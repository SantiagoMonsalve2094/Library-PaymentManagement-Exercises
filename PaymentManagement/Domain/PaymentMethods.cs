using System.Text.RegularExpressions;

namespace PaymentManagement.Domain;

public class CreditCardPayment : IPaymentMethod
{
    public CreditCardPayment(string cardHolder, string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardHolder))
            throw new ArgumentException("Card holder cannot be empty.", nameof(cardHolder));
        if (string.IsNullOrWhiteSpace(cardNumber))
            throw new ArgumentException("Card number cannot be empty.", nameof(cardNumber));

        CardHolder = cardHolder;
        CardNumber = cardNumber.Replace(" ", string.Empty);
    }

    public string Name => "Tarjeta de Crédito";
    public string CardHolder { get; }
    public string CardNumber { get; }

    public bool ProcessPayment(decimal amount)
    {
        if (!Regex.IsMatch(CardNumber, @"^\d{16}$"))
            throw new InvalidOperationException("Número de tarjeta inválido.");

        Console.WriteLine($"[Tarjeta] Cobro aprobado por {amount:C} para {CardHolder}.");
        return true;
    }
}

public class DigitalWalletPayment : IPaymentMethod
{
    public DigitalWalletPayment(string walletId, decimal availableBalance)
    {
        if (string.IsNullOrWhiteSpace(walletId))
            throw new ArgumentException("Wallet id cannot be empty.", nameof(walletId));
        if (availableBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(availableBalance), "Balance cannot be negative.");

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
    {
        if (string.IsNullOrWhiteSpace(bankAccountNumber))
            throw new ArgumentException("Bank account number cannot be empty.", nameof(bankAccountNumber));

        BankAccountNumber = bankAccountNumber;
    }

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
