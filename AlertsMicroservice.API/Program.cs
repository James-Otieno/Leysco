using AlertsMicroservice.Application.Command;
using AlertsMicroservice.Application.Services;
using AlertsMicroservice.Application.Settings;
using AlertsMicroservice.Domain.Entities;
using AlertsMicroservice.Domain.Repositories;
using AlertsMicroservice.Infastracture.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder
    .Configuration
    .GetConnectionString("ALERTS");
builder.Services.AddDbContext<AlertsContext>(opt =>
opt.UseSqlServer(connectionString));


builder.Services.AddScoped<IAlertsService, AlertsService>();
builder.Services.AddScoped<IAlertsRepository, AlertsRepository>();
builder.Services
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(SendEmailCommandHandler)));

var mySettings = builder.Configuration.GetSection("MailSettings").Get<MailSettings>();
builder.Services.AddSingleton(mySettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
