using Interview.Authenticator.Utilites;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// CORS configuration
builder.Services.AddCors(options => options.AddPolicy(name: "AngularUI",
    policy =>
    {
        policy
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    }
    ));

// JWT configuration
builder.Services.AddJwtAuthentication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.UseCors("AngularUI");

app.UseAuthentication();
app.UseAuthorization();

app.Run();


