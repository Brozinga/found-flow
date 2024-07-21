using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FoundFlow.Application.Models;
using FoundFlow.Domain.Interfaces;
using FoundFlow.Shared.Extensions;
using FoundFlow.Shared.Messages;
using MediatR;

namespace FoundFlow.Application.Common.Feature.Users.Login;

/// <summary>
/// Manipulador (Handler) para a solicitação de login do usuário (`LoginRequest`).
/// </summary>
public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly IManagerService _cacheDbService;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Cria uma nova instância de `LoginHandler`.
    /// </summary>
    /// <param name="tokenService">O serviço para gerar tokens JWT.</param>
    /// <param name="unitOfWork">A unidade de trabalho para gerenciar o acesso aos dados.</param>
    /// <param name="cacheDbService">O serviço para gerenciar dados em cache.</param>
    public LoginHandler(
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        IManagerService cacheDbService)
    {
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _cacheDbService = cacheDbService;
    }

    /// <summary>
    /// Método privado para verificar e atualizar o bloqueio temporário de tentativas de login.
    /// </summary>
    /// <param name="blockData">Os dados de bloqueio existentes (ou nulos se não houver).</param>
    /// <param name="loginAttemptsKey">A chave para identificar as tentativas de login do usuário.</param>
    /// <param name="collectionName">O nome da coleção no cache onde os dados de bloqueio são armazenados.</param>
    /// <param name="cleanData">Indica se os dados de bloqueio devem ser limpos (redefinidos).</param>
    private async Task BlockCheck(BlockInfo blockData, string loginAttemptsKey, string collectionName, bool cleanData = false)
    {
        if (blockData is null)
        {
            var newAttemptsData = new BlockInfo()
            {
                EmailKey = loginAttemptsKey,
                Attempts = cleanData ? 0 : 1,
                BlockedSince = cleanData ? null : DateTime.UtcNow
            };
            await _cacheDbService.InsertValueAsync(collectionName, newAttemptsData);
        }
        else
        {
            var updateAttemptsData = new BlockInfo()
            {
                Id = blockData.Id,
                EmailKey = loginAttemptsKey,
                Attempts = cleanData ? 0 : blockData.Attempts + 1,
                BlockedSince = cleanData ? null : blockData.BlockedSince ?? DateTime.UtcNow
            };
            await _cacheDbService.UpdateValueAsync(collectionName, "EmailKey", loginAttemptsKey, updateAttemptsData);
        }
    }

    /// <summary>
    /// Manipula a solicitação de login, autenticando o usuário e gerando um token JWT se o login for bem-sucedido.
    /// </summary>
    /// <param name="request">A solicitação de login contendo o e-mail e a senha do usuário.</param>
    /// <param name="cancellationToken">O token de cancelamento.</param>
    /// <returns>
    /// Um resultado (`Result`) contendo a resposta `LoginResponse` com o token JWT e sua data de expiração, se o login for bem-sucedido,
    /// ou uma mensagem de erro em caso de falha (por exemplo, usuário não encontrado, senha incorreta, conta bloqueada, etc.).
    /// </returns>
    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        string loginAttemptsKey = $"loginAttempts:{request.Email}";
        string collectionName = "BlockInfo";
        bool blockToTimeIsEnable = false;

        var blockData = await _cacheDbService.GetValueAsync<BlockInfo>(collectionName, "EmailKey", loginAttemptsKey);

        if (blockData?.Attempts >= 3)
        {
            if (blockData.BlockedSince.HasValue &&
                DateTime.UtcNow - blockData.BlockedSince.Value > TimeSpan.FromHours(1))
            {
                blockToTimeIsEnable = true;
            }
            else
            {
                Result<LoginResponse>.Failure(HttpStatusCode.Forbidden, ErrorMessages.UserLoginAccountTemporaryBlockedMessage);
            }
        }

        var user = await _unitOfWork.UsersRepository
            .FindOneAsync(
                user =>
                    user.Email == request.Email.ToLower(CultureInfo.CurrentCulture),
                cancellationToken);

        if (user == null)
            Result<LoginResponse>.Failure(HttpStatusCode.BadRequest, ErrorMessages.UsersLoginIncorrectMessage);

        if (user!.Blocked.HasValue && user.Blocked.Value)
            Result<LoginResponse>.Failure(HttpStatusCode.Forbidden, ErrorMessages.UsersLoginAccountIsBlockedMessage);

        bool isPasswordValid = Crypto.Verify(request.Password, user!.Password);

        if (!isPasswordValid)
        {
            await BlockCheck(blockData, loginAttemptsKey, collectionName, blockToTimeIsEnable);

            Result<LoginResponse>.Failure(HttpStatusCode.BadRequest, ErrorMessages.UsersLoginIncorrectMessage);
        }

        await BlockCheck(blockData, loginAttemptsKey, collectionName, true);

        (string token, var expires) = _tokenService.Generate(user);
        LoginResponse response = new(token, expires);
        return Result<LoginResponse>.Success(response);
    }
}