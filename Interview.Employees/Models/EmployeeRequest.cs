namespace Interview.Employees.Models;

public class EmployeeRequest
{
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required DateTime BirthdayDate { get; set; }
}
