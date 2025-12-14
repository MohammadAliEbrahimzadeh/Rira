# Rira User Management gRPC Service

A user management service using **gRPC** and **Protocol Buffers** instead of REST.

## API

All operations use **gRPC + Protocol Buffers**:

- **GetUserList** - Read with filtering & pagination
- **CreateUser** - Add new user (validates: 10-digit national code, letters only for names)
- **UpdateUser** - Update user (FirstName, LastName, BirthDate only)
- **DeleteUser** - Delete user by ID

## Error Handling

| Error | Response |
|-------|----------|
| Invalid input | `InvalidArgument` |
| Duplicate national code | `Internal` |
| Not found | `NotFound` |
| System error | `Internal` (logged with TraceId) |

## Key Features

✅ gRPC + Protocol Buffers (binary serialization)  
✅ Clean architecture (Domain, Application, Persistence, Web layers)  
✅ Unit of Work & Repository patterns  
✅ Input validation (regex, birth date checks)  
✅ Pagination support  
✅ Persian calendar dates  
✅ Global exception handling  
✅ Dependency injection  

## Tech Stack

.NET 9 | gRPC | Protocol Buffers | Entity Framework Core | SQL Server | Mapster | Ardalis Guard Clauses

## Why gRPC?

- Smaller payloads (binary vs JSON)
- Faster (HTTP/2 multiplexing)
- Type-safe contracts
- Better for high-throughput

## Testing

Use **Postman** or [BloomRPC](https://github.com/bloomrpc/bloomrpc) to test the gRPC service.

## Sources

- [gRPC Documentation](https://grpc.io/docs/languages/csharp/)
- [Protocol Buffers](https://developers.google.com/protocol-buffers)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Mapster](https://mapperly.github.io/)
- [Ardalis Guard Clauses](https://github.com/ardalis/GuardClauses)
