namespace TalentuInterview.Employees.Models
{
    public class Employ
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime BirthdayDate { get; set; }
        public required string HashPassword { get; set; }

    }
}
