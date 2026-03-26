namespace OOPExamples.Encapsulation;

public class BankAccount
{
    private decimal _balance;

    public AccountHolder accountHolder { get; set; }

    public decimal Balance
    {
        get => _balance;
        private set
        {
            if (value < 0) throw new InvalidOperationException("Balance cannot be negative.");
            _balance = value;
        }
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("The amount must be positive.");
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
    }
}

public class AccountHolder
{
    public required string Name { get; set; }
    public required string Email { get; set; }
}
