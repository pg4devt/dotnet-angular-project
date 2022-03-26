# dotnet-angular-project

Tutorial: Create a web API with ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio


Generate Models using EF Core
https://www.learnentityframeworkcore.com/walkthroughs/existing-database

`dotnet tool install --global dotnet-ef`
`dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --force`