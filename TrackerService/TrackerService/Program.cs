using Microsoft.EntityFrameworkCore;
using TrackerService.Data;
using TrackerService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
builder.Services.AddSingleton<RabbitMQConsumerService>();
builder.Services.AddHostedService<ConsumerBackgroundService>();

//builder.Services.AddScoped<IMessageTrackerProducerConsumer, TrackerProducerConsumer>();
builder.Services.AddScoped<IWeightService, WeightService>();
builder.Services.AddScoped<IWeightRepo, WeightRepo>();

var connectionString = Environment.GetEnvironmentVariable("CONNECTIONSTRING");
var localConnectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDbContext<WeightContext>(options =>
{
    options.UseSqlServer(localConnectionString);
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Test Me");
app.MapControllers();
app.Run();
