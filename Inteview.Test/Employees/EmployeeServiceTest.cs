using Interview.Employees.Contexts;
using Interview.Employees.Controllers;
using Interview.Employees.Models;
using Interview.Employees.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Xml;

namespace Inteview.Test
{
    public class EmployeeServiceTest
    {
        [Fact]
        public void GetNullEmail()
        {
            EmployService employeeService = SetEmployeeService();

            var result = employeeService.Get(null);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("Name", "")]
        [InlineData("", "LastName")]
        public void UpdateEmployee(string name, string lastName)
        {
            var employee = new  EmployeeRequest() 
            { 
                Email = string.Empty,
                BirthdayDate = DateTime.Now,
                LastName = lastName,
                Name = name,
                PhoneNumber = string.Empty
            };
            EmployService employeeService = SetEmployeeService();

            var result = employeeService.Update(employee);

            Assert.False(result);
        }


        private EmployService SetEmployeeService()
        {
            var mockDbContext = new Mock<SqlServerContext>();
            mockDbContext.Setup(c => c.Employees);
            var employeeService = new EmployService(mockDbContext.Object);
            return employeeService;
        }
    }
}
