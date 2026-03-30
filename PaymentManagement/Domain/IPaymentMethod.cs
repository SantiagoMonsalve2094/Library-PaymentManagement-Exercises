namespace PaymentManagement.Domain;

public interface IPaymentMethod
{
    string Name { get; }
    bool ProcessPayment(decimal amount);
}
