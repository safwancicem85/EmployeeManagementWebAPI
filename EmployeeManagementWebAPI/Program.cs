using EmployeeManagement.DAL.Context;
using EmployeeManagementWebAPI.Extensions;
using EmployeeManagementWebAPI.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<EmployeeManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"),
    migration => migration.MigrationsAssembly(typeof(EmployeeManagementContext).Assembly.FullName)));
builder.Services.AddMyDependencyGroup(); //register interfaces here
builder.Services.AddControllers();
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

app.UseMiddleware<GlobalErrorHandling>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
