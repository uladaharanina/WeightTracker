using Microsoft.EntityFrameworkCore;
using TrackerService.Data;
using TrackerService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
var localConnectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDbContext<WeightContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IWeightRepo, WeightRepo>();
builder.Services.AddScoped<IWeightService, WeightService>();


var app = builder.Build();


//Use Swagger to display API

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Test Me");
app.MapControllers();
app.Run();
