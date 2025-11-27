# ASP.NET Core User Management

A user management application built with ASP.NET Core MVC, Entity Framework Core, and SQL Server.  
Demonstrates the Repository pattern, a Manager layer, external API integration, and Dapper for raw SQL queries.

## Features

- User CRUD operations
- Server-side validation for emails, latitude/longitude, passwords
- EF Core with SQL Server migrations
- Dapper for raw SQL queries
- Repository and Manager pattern
- Integration with external APIs (JSONPlaceholder)
- MVC architecture with Razor views

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (included with Visual Studio) or SQL Server
- Visual Studio or VS Code

## Getting Started

### 1. Clone the repository

```bash
git clone <repository-url>
cd AspnetcoreUserManagement
```

### 2. Configure the database connection

The default connection string in `appsettings.json` uses SQL Server LocalDB:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=UserManagementDb;Trusted_Connection=True;"
}
```

If you're using a different SQL Server instance, update the connection string accordingly.

### 3. Restore dependencies

```bash
cd AspnetcoreUserManagement
dotnet restore
```

### 4. Apply database migrations

```bash
dotnet ef database update
```

This will create the `UserManagementDb` database and apply all migrations.

### 5. Run the application

```bash
dotnet build
dotnet run
```

The application will start and be available at:
- HTTPS: `https://localhost:7271`

### 6. Access the application

Open your browser and navigate to `https://localhost:5001` (or the URL shown in the console).

## Project Structure

```
AspnetcoreUserManagement/
├── Controllers/          # MVC controllers
├── Data/                 # DbContext and repositories (using Dapper)
├── Manageres/            # Business logic layer
├── Models/
│   ├── DTOs/            # Data transfer objects
│   ├── Entities/        # Database entities
│   └── ViewModels/      # View models for MVC
├── Services/             # External API services
├── Helpers/              # Mapping utilities
├── Views/                # Razor views
├── wwwroot/              # Static files
└── Migrations/           # EF Core migrations
```

## Database Migrations

### Make sure EF Core tools are installed:

```bash
dotnet tool install --global dotnet-ef
```
### Create a new migration

```bash
dotnet ef migrations add MigrationName
```

### Apply migrations

```bash
dotnet ef database update
```

### Remove last migration

```bash
dotnet ef migrations remove
```

## Technologies Used

- **ASP.NET Core 8.0** - Web framework
- **Entity Framework Core 8.0** - ORM for migrations
- **Dapper 2.1** - Lightweight ORM for raw SQL queries
- **SQL Server** - Database
- **Bootstrap** - UI framework
- **jQuery** - JavaScript library

## Troubleshooting

### Database connection issues

If you encounter connection issues:
1. Verify SQL Server LocalDB is installed: `sqllocaldb info`
2. Start LocalDB if needed: `sqllocaldb start MSSQLLocalDB`
3. Check the connection string in `appsettings.json`

### Migration issues

If migrations fail:
1. Ensure the connection string is correct
2. Delete the database and reapply migrations: `dotnet ef database drop` then `dotnet ef database update`
3. Check that EF Core tools are installed: `dotnet tool install --global dotnet-ef`

## License

This project is for educational purposes.
