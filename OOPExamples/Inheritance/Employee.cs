namespace OOPExamples.Inheritance
{
    public class Employee
    {
        public required string Name { get; set; }
        public decimal BaseSalary { get; set; }

        public virtual decimal CalculatePay() => BaseSalary;

        public void DisplayInfo() =>
            Console.WriteLine($"{Name} — Pay: {CalculatePay():C}");
    }

    public class HourlyEmployee : Employee
    {
        public int HoursWorked { get; set; }

        public override decimal CalculatePay() => BaseSalary * HoursWorked;
    }

    public class BonusEmployee : Employee
    {
        public decimal Bonus { get; set; }

        public override decimal CalculatePay() => BaseSalary + Bonus;
    }
}
