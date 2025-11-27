# ASP.NET Core User Management

A user management application built with ASP.NET Core MVC, Entity Framework Core, and SQL Server.  
Demonstrates the Repository pattern, a Manager layer, external API integration, and Dapper for raw SQL queries.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (included with Visual Studio) or SQL Server
- Visual Studio or VS Code

## Getting Started

### 1. Clone the repository

```bash
git clone <repository-url>
cd aspnetcore-user-management
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

Open your browser and navigate to `https://localhost:7271` (or the URL shown in the console).

## Technologies Used

- **ASP.NET Core 8.0** - Web framework
- **Entity Framework Core 8.0** - ORM for migrations
- **Dapper 2.1** - Lightweight ORM for raw SQL queries
- **SQL Server** - Database
- **Bootstrap** - UI framework
- **jQuery** - JavaScript library
