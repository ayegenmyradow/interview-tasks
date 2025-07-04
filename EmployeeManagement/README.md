# Employee Management System

A .NET Core-based Employee Management System with CQRS architecture, built using:
- .NET 8.0
- PostgreSQL
- Entity Framework Core
- MediatR for CQRS implementation
- Swagger for API documentation

## Features

### Position Management
- Create new positions
- Update existing positions
- Delete positions (if no employees are assigned)
- List positions with pagination, sorting, and filtering
- Get position by ID

### Employee Management
- Create new employees
- Update employee details
- Delete employees
- List employees with pagination, sorting, and filtering
- Get employee by ID

## Prerequisites

- .NET 8.0 SDK
- PostgreSQL 15 or later
- Visual Studio 2022 or VS Code

## Getting Started

1. Clone the repository
2. Configure your environment:
   - Copy `appsettings.Example.json` to a new file named `appsettings.Production.json`
   - Update the configuration values in your new file with your actual settings
   - The development environment uses `appsettings.Development.json` with safe defaults

## Environment Configuration

The application uses different settings files for different environments:

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development environment settings
- `appsettings.Production.json` - Production environment settings (not in source control)

### Required Configuration

- **Database Connection**: Update the connection string in your environment-specific settings file
- **JWT Settings**: Configure your JWT secret key and token expiration
- **Email Settings**: Configure SMTP settings for email notifications
- **CORS Settings**: Configure allowed origins for your frontend application

### Development Defaults

The development environment comes with safe defaults:
- Local PostgreSQL database
- Local SMTP server settings
- Development JWT secret
- CORS allowed for localhost:3000

## Project Structure

```
EmployeeManagement/
├── EmployeeManagement.API/         # REST API endpoints
├── EmployeeManagement.Core/        # Domain models and CQRS patterns
├── EmployeeManagement.Infrastructure/  # EF Core and handlers
└── EmployeeManagement.Tests/       # Unit tests
```

## Setup Instructions

1. Clone the repository:
```bash
git clone <repository-url>
cd EmployeeManagement
```

2. Update database connection string in `EmployeeManagement.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=EmployeeManagement;Username=your_username;Password=your_password"
  }
}
```

3. Apply database migrations:
```bash
cd EmployeeManagement.API
dotnet ef database update
```

4. Build the solution:
```bash
dotnet build
```

5. Run the API:
```bash
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: http://localhost:5000/swagger

## API Endpoints

### Positions

#### GET /api/positions
Get a list of positions with pagination, sorting, and filtering.
- Query Parameters:
  - `pageNumber` (default: 1)
  - `pageSize` (default: 10)
  - `searchTerm` (optional)
  - `sortBy` (optional)
  - `sortDescending` (default: false)

#### GET /api/positions/{id}
Get a position by ID.

#### POST /api/positions
Create a new position.
```json
{
    "title": "Software Developer",
    "baseSalary": 75000.00,
    "description": "Full-stack developer position"
}
```

#### PUT /api/positions/{id}
Update an existing position.
```json
{
    "id": 1,
    "title": "Senior Developer",
    "baseSalary": 95000.00,
    "description": "Senior full-stack developer position"
}
```

#### DELETE /api/positions/{id}
Delete a position (only if no employees are assigned).

### Employees

#### GET /api/employees
Get a list of employees with pagination, sorting, and filtering.
- Query Parameters:
  - `pageNumber` (default: 1)
  - `pageSize` (default: 10)
  - `searchTerm` (optional)
  - `sortBy` (optional)
  - `sortDescending` (default: false)

#### GET /api/employees/{id}
Get an employee by ID.

#### POST /api/employees
Create a new employee.
```json
{
    "firstName": "John",
    "lastName": "Doe",
    "middleName": "Robert",
    "dateOfBirth": "1990-01-01T00:00:00Z",
    "hireDate": "2023-01-01T00:00:00Z",
    "positionId": 1
}
```

#### PUT /api/employees/{id}
Update an existing employee.
```json
{
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "middleName": "Robert",
    "dateOfBirth": "1990-01-01T00:00:00Z",
    "hireDate": "2023-01-01T00:00:00Z",
    "positionId": 1
}
```

#### DELETE /api/employees/{id}
Delete an employee.

## Development

### Adding New Migrations

When you make changes to the models, create a new migration:

```bash
cd EmployeeManagement.API
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### Running Tests

```bash
cd EmployeeManagement.Tests
dotnet test
```

### Building for Production

```bash
dotnet publish -c Release
```

## Error Handling

The API uses standard HTTP status codes:
- 200: Success
- 201: Created
- 204: No Content (successful deletion)
- 400: Bad Request
- 404: Not Found
- 500: Internal Server Error

## Data Validation

- All required fields are enforced through model validation
- Dates are stored in UTC format
- Position must exist when creating/updating an employee
- Cannot delete a position that has employees assigned

## Security Considerations

- All string inputs are properly validated
- SQL injection protection through Entity Framework Core
- Dates are properly handled for UTC storage

## Logging

The application uses Serilog for logging:
- Console logging for development
- File logging with daily rotation
- Logs are stored in the `logs` directory 