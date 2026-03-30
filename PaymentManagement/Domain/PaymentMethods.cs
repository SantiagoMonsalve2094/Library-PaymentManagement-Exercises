namespace PaymentManagement.Domain;

public class CreditCardPayment : IPaymentMethod
{
    public CreditCardPayment(string cardHolder, string cardNumber)
    {
        CardHolder = cardHolder;
        CardNumber = cardNumber;
    }

    public string Name => "Tarjeta de Crédito";
    public string CardHolder { get; }
    public string CardNumber { get; }

    public bool ProcessPayment(decimal amount)
    {
        if (CardNumber.Length != 16)
            throw new InvalidOperationException("Número de tarjeta inválido.");

        Console.WriteLine($"[Tarjeta] Cobro aprobado por {amount:C} para {CardHolder}.");
        return true;
    }
}

public class DigitalWalletPayment : IPaymentMethod
{
    public DigitalWalletPayment(string walletId, decimal availableBalance)
    {
        WalletId = walletId;
        AvailableBalance = availableBalance;
    }

    public string Name => "Billetera Digital";
    public string WalletId { get; }
    public decimal AvailableBalance { get; private set; }

    public bool ProcessPayment(decimal amount)
    {
        if (AvailableBalance < amount)
            throw new InvalidOperationException($"Saldo insuficiente en la billetera {WalletId}.");

        AvailableBalance -= amount;
        Console.WriteLine($"[Billetera] Cobro aprobado por {amount:C}. Saldo restante: {AvailableBalance:C}.");
        return true;
    }
}

public class BankTransferPayment : IPaymentMethod
{
    public BankTransferPayment(string bankAccountNumber)
        => BankAccountNumber = bankAccountNumber;

    public string Name => "Transferencia Bancaria";
    public string BankAccountNumber { get; }

    public bool ProcessPayment(decimal amount)
    {
        var reference = $"TRX-{DateTime.UtcNow:yyyyMMddHHmmss}";
        Console.WriteLine(
            $"[Transferencia] Cobro aprobado por {amount:C} desde cuenta {BankAccountNumber}. Referencia: {reference}.");
        return true;
    }
}
