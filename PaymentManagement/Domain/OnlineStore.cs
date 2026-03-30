namespace PaymentManagement.Domain;

public class OnlineStore
{
    private readonly List<Order> _orders = [];
    private readonly List<IPaymentMethod> _paymentMethods = [];
    private readonly PaymentProcessor _paymentProcessor = new();

    public void AddOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        var exists = _orders.Any(o => o.Id == order.Id);
        if (exists)
            throw new InvalidOperationException($"Ya existe una orden con id {order.Id}.");

        _orders.Add(order);
    }

    public void AddPaymentMethod(IPaymentMethod paymentMethod)
    {
        ArgumentNullException.ThrowIfNull(paymentMethod);

        var exists = _paymentMethods.Any(pm =>
            string.Equals(pm.Name, paymentMethod.Name, StringComparison.OrdinalIgnoreCase));

        if (exists)
            throw new InvalidOperationException($"Ya existe un método de pago llamado \"{paymentMethod.Name}\".");

        _paymentMethods.Add(paymentMethod);
    }

    public void ProcessOrderPayment(int orderId, string paymentMethodName)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId)
            ?? throw new KeyNotFoundException($"No existe una orden con id {orderId}.");

        var paymentMethod = _paymentMethods.FirstOrDefault(pm =>
            string.Equals(pm.Name, paymentMethodName, StringComparison.OrdinalIgnoreCase))
            ?? throw new KeyNotFoundException($"No existe el método de pago \"{paymentMethodName}\".");

        _paymentProcessor.Process(order, paymentMethod);
    }

    public void ShowSummary()
    {
        Console.WriteLine("=== Resumen final de la tienda ===");

        foreach (var order in _orders.OrderBy(o => o.Id))
            Console.WriteLine(order.ObtainDescription());

        var totalCollected = _orders.Where(o => o.IsPaid()).Sum(o => o.TotalAmount);
        var pendingOrders = _orders.Count(o => !o.IsPaid());

        Console.WriteLine();
        Console.WriteLine($"Total recaudado: {totalCollected:C}");
        Console.WriteLine($"Órdenes pendientes: {pendingOrders}");
    }
}
