# dotnet-angular-project

## Tutorial: Create an ASP.NET Core app with Angular in Visual Studio
https://docs.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-angular?view=vs-2022

## Tutorial: Create a web API with ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio


## Generate Models using EF Core
https://www.learnentityframeworkcore.com/walkthroughs/existing-database

`dotnet tool install --global dotnet-ef`

`dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --force`


## Angular commands

### Generate a service (ex. Employee)

`cd ClientApp`

`ng g s employee --path=src/app/services/employee --module=app`

### Generate a component (ex. Employee)

`cd ClientApp`

`ng g c employee-list --path=src/app/components --module=app`


## Get the Northwind sample database for SQL Server

https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/downloading-sample-databases#get-the-northwind-sample-database-for-sql-server