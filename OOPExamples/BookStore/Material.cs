namespace OOPExamples.BookStore
{
    public abstract class Material
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }
        public int Stock { get; set; }

        public bool IsAvailable() => Stock > 0;

        public abstract void ObtainDescription();
    }

    public class Book : Material, ILoanable
    {
        public string ISBN { get; set; }
        public int Pages { get; set; }

        public bool Loan(string borrower)
        {
            throw new NotImplementedException();
        }

        public override void ObtainDescription() =>
            Console.WriteLine($"Book: {Title} by {Author} ({PublishYear}) - ISBN: {ISBN} - It Has {Pages} pages");

        public void Return()
        {
            throw new NotImplementedException();
        }
    }

    public class Magazine : Material, ILoanable
    {
        public Frequency PublicationFrequency { get; set; }
        public int IssueNumber { get; set; }

        public bool Loan(string borrower)
        {
            throw new NotImplementedException();
        }

        public override void ObtainDescription() =>
            Console.WriteLine($"Magazine: {Title} by {Author} ({PublishYear}) - Issue: {IssueNumber}");

        public void Return()
        {
            throw new NotImplementedException();
        }
    }

    public enum Frequency { Weekly, Monthly, Quarterly }
}
