namespace PaymentManagement.Domain;

public class PaymentProcessor
{
    public void Process(Order order, IPaymentMethod paymentMethod)
    {
        ArgumentNullException.ThrowIfNull(order);
        ArgumentNullException.ThrowIfNull(paymentMethod);

        if (order.IsPaid())
            throw new InvalidOperationException($"La orden {order.Id} ya fue pagada.");

        var wasSuccessful = paymentMethod.ProcessPayment(order.TotalAmount);
        if (!wasSuccessful)
            throw new InvalidOperationException($"No fue posible procesar el pago de la orden {order.Id}.");

        order.MarkAsPaid(paymentMethod.Name);
        Console.WriteLine($"Orden {order.Id} pagada correctamente con {paymentMethod.Name}.");
    }
}
