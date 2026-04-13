using CustomerService.Infrastructure.Repositories;
using CustomerService.Application.Services;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ✅ só aqui
builder.Services.AddScoped<CustomerAppService>();

var app = builder.Build();

// 🚀 pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();