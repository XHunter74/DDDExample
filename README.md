# DDDExample

A .NET 8 Web API project demonstrating Domain-Driven Design (DDD) principles for a banking system. The API supports operations on bank accounts (open, close, deposit, withdraw) and is designed for extensibility and maintainability.

## Features
- Domain-Driven Design (DDD) architecture
- Aggregate root: `BankAccount`
- Value Objects: `Money`, `Currency`, `AccountId`
- Domain Events: raised and persisted to the database
- Repository pattern for data access
- Entity Framework Core with PostgreSQL
- Centralized error handling with custom exceptions
- Logging with Serilog
- Swagger/OpenAPI documentation

## Domain Events
Domain events are raised by aggregate roots and stored in the `Events` table. The dispatcher is generic and supports any domain entity inheriting from `BaseDomainEntity`.

## Project Structure
- `Domain/Entities` - Aggregate roots and entities
- `Domain/ValueObjects` - Value objects
- `Domain/Events` - Domain event contracts and implementations
- `Infrastructure` - Data access, event dispatcher, DbContext
- `Controllers` - API endpoints
- `Services` - Application services
- `Exceptions` - Custom exception types

## Getting Started
1. Configure your PostgreSQL connection string in `appsettings.json` under `DbConnection`.
2. Build and run the project:
   ```sh
   dotnet run
   ```
3. Access Swagger UI at `/swagger` for API documentation and testing.

## API Endpoints
- `POST /api/accounts` - Open a new account
- `POST /api/accounts/{id}/deposit` - Deposit money
- `POST /api/accounts/{id}/withdraw` - Withdraw money
- `POST /api/accounts/{id}/close` - Close account
- `GET /api/accounts/{id}` - Get account details

## License
Copyright (c) 2025 Serhiy Krasovskyy xhunter74@gmail.com  

This project is for demonstration and testing purposes only.

