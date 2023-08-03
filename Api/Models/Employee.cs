namespace Api.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    public int Age { 
        get 
        {
            {
                int age = DateTime.Now.Year - DateOfBirth.Year;

                // Maybe birthday didn't occured yet
                if (DateOfBirth.Date > DateTime.Now.AddYears(-age))
                    age--;

                return age;
            }
        } 
    }
}
