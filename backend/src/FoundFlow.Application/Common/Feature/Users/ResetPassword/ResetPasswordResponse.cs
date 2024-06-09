namespace FoundFlow.Application.Common.Feature.Users.ResetPassword;
public class ResetPasswordResponse
{
    public ResetPasswordResponse(string newPassword)
    {
        NewPassword = newPassword;
    }

    public string NewPassword { get; private set; }
}
