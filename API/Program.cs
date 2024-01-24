using Microsoft.EntityFrameworkCore;
using API.Data;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(connectionString));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
