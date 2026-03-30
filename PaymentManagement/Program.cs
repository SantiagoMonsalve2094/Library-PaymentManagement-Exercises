using PaymentManagement.Domain;

var store = new OnlineStore();

store.AddOrder(new Order(1, "Carlos Pérez", 150_000m));
store.AddOrder(new Order(2, "Laura Gómez", 89_500m));
store.AddOrder(new Order(3, "Miguel Torres", 240_000m));

store.AddPaymentMethod(new CreditCardPayment("Carlos Pérez", "4111111111111111"));
store.AddPaymentMethod(new DigitalWalletPayment("wallet-laura-01", 120_000m));
store.AddPaymentMethod(new BankTransferPayment("010123456789"));

Console.WriteLine("=== Procesamiento inicial de pagos ===");
store.ProcessOrderPayment(1, "Tarjeta de Crédito");
store.ProcessOrderPayment(2, "Billetera Digital");
store.ProcessOrderPayment(3, "Transferencia Bancaria");

Console.WriteLine();
Console.WriteLine("=== Intento de pago duplicado ===");
try
{
    store.ProcessOrderPayment(1, "Tarjeta de Crédito");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR CONTROLADO] {ex.Message}");
}

Console.WriteLine();
store.ShowSummary();
