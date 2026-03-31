namespace Library.Domain;

public abstract class Material
{
    protected Material(string title, string author, int publishYear, int stock)
    {
        Title = title;
        Author = author;
        PublishYear = publishYear;
        Stock = stock;
    }

    public string Title { get; }
    public string Author { get; }
    public int PublishYear { get; }
    public int Stock { get; private set; }

    public bool IsAvailable() => Stock > 0;

    protected void DecreaseStock()
    {
        if (!IsAvailable())
            throw new InvalidOperationException($"No hay stock disponible para \"{Title}\".");

        Stock--;
    }

    protected void IncreaseStock() => Stock++;

    public abstract string ObtainDescription();
}

public class Book : Material, ILoanable
{
    public Book(string title, string author, int publishYear, int stock, string isbn, int pages)
        : base(title, author, publishYear, stock)
    {
        ISBN = isbn;
        Pages = pages;
    }

    public string ISBN { get; }
    public int Pages { get; }

    public bool Loan(string borrower)
    {
        DecreaseStock();
        Console.WriteLine($"Se prestó \"{Title}\" a {borrower}.");
        return true;
    }

    public void Return()
    {
        IncreaseStock();
        Console.WriteLine($"Se devolvió \"{Title}\".");
    }

    public override string ObtainDescription() =>
        $"Book: {Title} by {Author} ({PublishYear}) - ISBN: {ISBN} - Pages: {Pages} - Stock: {Stock}";
}

public class Magazine : Material, ILoanable
{
    public Magazine(
        string title,
        string author,
        int publishYear,
        int stock,
        Frequency publicationFrequency,
        int issueNumber)
        : base(title, author, publishYear, stock)
    {
        PublicationFrequency = publicationFrequency;
        IssueNumber = issueNumber;
    }

    public Frequency PublicationFrequency { get; }
    public int IssueNumber { get; }

    public bool Loan(string borrower)
    {
        DecreaseStock();
        Console.WriteLine($"Se prestó la revista \"{Title}\" a {borrower}.");
        return true;
    }

    public void Return()
    {
        IncreaseStock();
        Console.WriteLine($"Se devolvió la revista \"{Title}\".");
    }

    public override string ObtainDescription() =>
        $"Magazine: {Title} by {Author} ({PublishYear}) - Issue: {IssueNumber} - Frequency: {PublicationFrequency} - Stock: {Stock}";
}

public enum Frequency
{
    Weekly,
    Monthly,
    Quarterly
}
