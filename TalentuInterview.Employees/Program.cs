using TalentuInterview.Employees.Contexts;
using TalentuInterview.Employees.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS configuration
builder.Services.AddCors(options => options.AddPolicy(name: "AngularUI",
    policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }
    ));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<SqlServerContext>(builder.Configuration.GetConnectionString("TalentuSqlServerConnection"));
builder.Services.AddScoped<IEmployeeService, EmployService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.UseCors("AngularUI");

app.Run();
