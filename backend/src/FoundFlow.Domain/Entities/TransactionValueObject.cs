using FoundFlow.Domain.Enums;

namespace FoundFlow.Domain.Entities;

public record TransactionValueObject(string TransactionName, decimal Amount, TransactionType TransactionType, PaymentType PaymentStatus);