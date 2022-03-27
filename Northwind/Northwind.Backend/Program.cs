using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Northwind.Backend.DataContext;
using Northwind.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<NorthwindContext>(options =>
{
    var conStr = builder.Configuration.GetConnectionString("NorthwindDatabase");
    options.UseSqlServer(conStr);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
