using Microsoft.EntityFrameworkCore;
using TrackerService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WeightContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionSring"));
});

var app = builder.Build();


//Use Swagger to display API

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
