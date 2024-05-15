using Interview.Employees.Contexts;
using Interview.Employees.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS configuration
SetCORS(builder);
// SQL Server Configuration
SetSqlConfiguration(builder);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseCors("AngularUI");

app.Run();

void SetSqlConfiguration(WebApplicationBuilder builder)
{
    builder.Services.AddSqlServer<SqlServerContext>(builder.Configuration.GetConnectionString("InterviewSqlServerConnection"));
    builder.Services.AddScoped<IEmployeeService, EmployService>();
}

void SetCORS(WebApplicationBuilder builder)
{
    builder.Services.AddCors(options => options.AddPolicy(name: "AngularUI",
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }
        ));
}