namespace EmpytProject.Models;

public class Employee
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set;  }
    public DateOnly DateOfBirth { get; set; }
}