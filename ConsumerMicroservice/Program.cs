using ConsumerMicroservice.Models;
using ConsumerMicroservice.RepositoryLayer;
using ConsumerMicroservice.RepositoryLayer.IRepositoryLayer;
using ConsumerMicroservice.ServiceLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Logging.AddLog4Net();
builder.Services.AddScoped<IConsumerRepository, ConsumerRepository>();
builder.Services.AddScoped<IConsumerBusinessRepository, ConsumerBusinessRepository>();
builder.Services.AddScoped<IBusinessPropertyRepository, BusinessPropertyRepository>();
builder.Services.AddScoped<IConsumerService, ConsumerService>();
builder.Services.AddDbContext<InsureityPortalDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
