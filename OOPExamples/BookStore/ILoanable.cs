namespace OOPExamples.BookStore
{
    public interface ILoanable
    {
        bool Loan(string borrower);
        void Return();
    }
}
