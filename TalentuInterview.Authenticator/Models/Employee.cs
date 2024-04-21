using System.Text.Json.Serialization;

namespace TalentuInterview.Authenticator.Models;

public class Employee
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string HashPassword { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? BirthdayDate { get; set; }
    public DateTime? HireDate { get; set; }
    public Guid? RoleId { get; set; }
}
