using Microsoft.EntityFrameworkCore;
using Northwind.Backend.DataContext;
using Northwind.Backend.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

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

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
