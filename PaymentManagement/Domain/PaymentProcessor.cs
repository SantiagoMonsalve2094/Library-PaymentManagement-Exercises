namespace PaymentManagement.Domain;

public class PaymentProcessor
{
    public void Process(Order order, IPaymentMethod paymentMethod)
    {
        if (order.IsPaid())
            throw new InvalidOperationException($"La orden {order.Id} ya fue pagada.");

        paymentMethod.ProcessPayment(order.TotalAmount);

        order.MarkAsPaid(paymentMethod.Name);
        Console.WriteLine($"Orden {order.Id} pagada correctamente con {paymentMethod.Name}.");
    }
}
