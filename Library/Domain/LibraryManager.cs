namespace Library.Domain;

public class LibraryManager
{
    private readonly List<Material> _materials = [];

    public void AddMaterial(Material material)
    {
        ArgumentNullException.ThrowIfNull(material);

        var alreadyExists = _materials.Any(m =>
            string.Equals(m.Title, material.Title, StringComparison.OrdinalIgnoreCase));

        if (alreadyExists)
            throw new InvalidOperationException($"Ya existe un material con el título \"{material.Title}\".");

        _materials.Add(material);
    }

    public void ShowAvailableMaterials()
    {
        Console.WriteLine("=== Materiales disponibles para préstamo ===");

        var availableMaterials = _materials.Where(m => m.IsAvailable()).ToList();
        if (availableMaterials.Count == 0)
        {
            Console.WriteLine("No hay materiales disponibles.");
            return;
        }

        foreach (var material in availableMaterials)
            Console.WriteLine(material.ObtainDescription());
    }

    public void LoanMaterial(string title, string borrower)
    {
        var material = FindByTitle(title);
        if (material is not ILoanable loanableMaterial)
            throw new InvalidOperationException($"El material \"{material.Title}\" no permite préstamo.");

        loanableMaterial.Loan(borrower);
    }

    public void ReturnMaterial(string title)
    {
        var material = FindByTitle(title);
        if (material is not ILoanable loanableMaterial)
            throw new InvalidOperationException($"El material \"{material.Title}\" no permite devolución.");

        loanableMaterial.Return();
    }

    private Material FindByTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        var material = _materials.FirstOrDefault(m =>
            string.Equals(m.Title, title, StringComparison.OrdinalIgnoreCase));

        return material
            ?? throw new KeyNotFoundException($"No existe un material con el título \"{title}\".");
    }
}
