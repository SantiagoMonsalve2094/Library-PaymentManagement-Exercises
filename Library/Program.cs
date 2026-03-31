using Library.Domain;

var library = new LibraryManager();

library.AddMaterial(new Book("Clean Code", "Robert C. Martin", 2008, 1, "9780132350884", 464));
library.AddMaterial(new Book("The Pragmatic Programmer", "Andrew Hunt", 1999, 1, "9780201616224", 352));
library.AddMaterial(new Magazine("National Geographic", "National Geographic Society", 2025, 1, Frequency.Monthly, 120));

library.ShowAvailableMaterials();

Console.WriteLine();
Console.WriteLine("=== Préstamo de un libro ===");
try
{
    library.LoanMaterial("Clean Code", "Santiago Monsalve");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR CONTROLADO] {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== Intento de préstamo duplicado ===");
try
{
    library.LoanMaterial("Clean Code", "Ana Torres");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR CONTROLADO] {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== Devolución de libro ===");
try
{
    library.ReturnMaterial("Clean Code");
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR CONTROLADO] {ex.Message}");
}

Console.WriteLine();
library.ShowAvailableMaterials();
