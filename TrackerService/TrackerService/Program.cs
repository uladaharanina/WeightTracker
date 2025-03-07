using Microsoft.EntityFrameworkCore;
using TrackerService.Data;
using TrackerService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<WeightContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
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
