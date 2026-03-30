namespace Library.Domain;

public interface ILoanable
{
    bool Loan(string borrower);
    void Return();
}
