namespace PaymentManagement.Domain;

public class Order
{
    public Order(int id, string customerName, decimal totalAmount)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be positive.");
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name cannot be empty.", nameof(customerName));
        if (totalAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalAmount), "Total amount must be positive.");

        Id = id;
        CustomerName = customerName;
        TotalAmount = totalAmount;
        Status = OrderStatus.Pending;
    }

    public int Id { get; }
    public string CustomerName { get; }
    public decimal TotalAmount { get; }
    public OrderStatus Status { get; private set; }
    public string? PaidWith { get; private set; }

    public bool IsPaid() => Status == OrderStatus.Paid;

    public void MarkAsPaid(string paymentMethodName)
    {
        if (IsPaid())
            throw new InvalidOperationException($"La orden {Id} ya fue pagada.");
        if (string.IsNullOrWhiteSpace(paymentMethodName))
            throw new ArgumentException("Payment method name cannot be empty.", nameof(paymentMethodName));

        Status = OrderStatus.Paid;
        PaidWith = paymentMethodName;
    }

    public string ObtainDescription()
    {
        var method = PaidWith ?? "N/A";
        return $"Order #{Id} | Cliente: {CustomerName} | Monto: {TotalAmount:C} | Estado: {Status} | Método: {method}";
    }
}

public enum OrderStatus
{
    Pending,
    Paid
}
