# Architecture Overview

## 📐 Project Architecture

The Task Manager API follows a **Clean Architecture** approach with clear separation of concerns across multiple layers.

## 🏗️ Layered Architecture

### 1. **Presentation Layer (API)**
- **Controllers**: Handle HTTP requests and responses
- **Middleware**: Cross-cutting concerns (exception handling, logging)
- **Validation**: Input validation using FluentValidation

**Responsibilities:**
- Receive HTTP requests
- Validate input data
- Route requests to appropriate services
- Format responses
- Handle errors gracefully

### 2. **Business Logic Layer (Services)**
- **Services**: Implement core business logic
- **Interfaces**: Define contracts for service implementations

**Responsibilities:**
- Execute business rules
- Coordinate data operations
- Transform domain entities to DTOs
- Implement domain-specific logic

### 3. **Data Access Layer**
- **DbContext**: Entity Framework Core database context
- **Entities**: Domain models that map to database tables

**Responsibilities:**
- Database operations (CRUD)
- Data persistence
- Query optimization
- Transaction management

### 4. **Cross-Cutting Concerns**
- **DTOs**: Data Transfer Objects for API communication
- **Validators**: Input validation rules
- **Logging**: Structured logging with Serilog
- **Exception Handling**: Global error handling

---

## 🔄 Data Flow

```
Client Request
    ↓
[Middleware] → Exception Handling
    ↓
[Controller] → Request Routing & Initial Validation
    ↓
[Validator] → FluentValidation Rules
    ↓
[Service] → Business Logic Execution
    ↓
[DbContext] → Database Operations
    ↓
[Database] → SQLite Persistence
    ↓
[Service] → Data Transformation (Entity → DTO)
    ↓
[Controller] → Response Formatting
    ↓
Client Response
```

---

## 🎯 Design Patterns Used

### 1. **Dependency Injection (DI)**
Promotes loose coupling and testability.

```csharp
// Registration
builder.Services.AddScoped<ITaskService, TaskService>();

// Usage
public TasksController(ITaskService taskService)
{
    _taskService = taskService;
}
```

### 2. **Repository Pattern** (through EF Core)
Abstracts data access logic.

```csharp
public class TaskService : ITaskService
{
    private readonly TaskDbContext _context;
    
    public async Task<TaskDto> GetTaskByIdAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        return MapToDto(task);
    }
}
```

### 3. **DTO Pattern**
Separates internal domain models from API contracts.

```csharp
// Internal Model
public class TaskItem { /* Domain properties */ }

// API Contract
public class TaskDto { /* Exposed properties */ }
```

### 4. **Middleware Pattern**
Handles cross-cutting concerns in the request pipeline.

```csharp
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

### 5. **Strategy Pattern** (FluentValidation)
Defines validation strategies for different DTOs.

```csharp
public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
{
    // Validation rules
}
```

---

## 📦 Project Dependencies

### Core Dependencies
- **Microsoft.AspNetCore.App** - ASP.NET Core framework
- **Microsoft.EntityFrameworkCore.Sqlite** - SQLite provider for EF Core
- **FluentValidation.AspNetCore** - Model validation
- **Swashbuckle.AspNetCore** - API documentation (Swagger)
- **Serilog.AspNetCore** - Structured logging

### Dependency Graph

```
TaskManagerApi
├── Controllers
│   ├── → Services (Interface)
│   ├── → DTOs
│   └── → Validators
├── Services
│   ├── → Data (DbContext)
│   ├── → Models
│   └── → DTOs
├── Data
│   └── → Models
└── Middleware
    └── → Logging
```

---

## 🔐 SOLID Principles

### **S - Single Responsibility Principle**
Each class has one reason to change:
- Controllers handle HTTP concerns
- Services handle business logic
- DbContext handles data access

### **O - Open/Closed Principle**
Services are open for extension through interfaces:
```csharp
public interface ITaskService { }
public class TaskService : ITaskService { }
```

### **L - Liskov Substitution Principle**
Implementations can replace their interfaces without breaking functionality.

### **I - Interface Segregation Principle**
Interfaces are focused and specific:
```csharp
public interface ITaskService
{
    Task<TaskDto> GetTaskByIdAsync(int id);
    // Other task-related methods only
}
```

### **D - Dependency Inversion Principle**
High-level modules depend on abstractions:
```csharp
// Controller depends on ITaskService interface, not concrete implementation
public TasksController(ITaskService taskService)
```

---

## 🧪 Testing Strategy

### Unit Tests
- **Services**: Test business logic in isolation
- **Validators**: Test validation rules
- **Controllers**: Test request/response handling

### Integration Tests
- Test full request pipeline
- Test database operations
- Test middleware behavior

### Test Pyramid

```
       /\
      /UI\           ← Few UI/E2E tests
     /────\
    /  API \         ← More API/Integration tests
   /────────\
  /   Unit   \       ← Many Unit tests
 /────────────\
```

---

## 📊 Database Design

### Entity: TaskItem

| Column | Type | Constraints |
|--------|------|-------------|
| Id | INTEGER | PRIMARY KEY, AUTOINCREMENT |
| Title | TEXT(200) | NOT NULL |
| Description | TEXT(1000) | NULL |
| IsCompleted | BOOLEAN | NOT NULL, DEFAULT 0 |
| Priority | INTEGER | NOT NULL, DEFAULT 1 |
| DueDate | DATETIME | NULL |
| CreatedAt | DATETIME | NOT NULL |
| UpdatedAt | DATETIME | NOT NULL |

### Indexes
- `idx_IsCompleted` on `IsCompleted`
- `idx_Priority` on `Priority`
- `idx_DueDate` on `DueDate`

---

## 🚀 Performance Considerations

### 1. **Async/Await**
All I/O operations use async patterns for better scalability.

### 2. **Database Indexing**
Strategic indexes on frequently queried columns.

### 3. **DTOs for Data Shaping**
Reduce payload size by returning only necessary data.

### 4. **Query Optimization**
- Use `AsQueryable()` for deferred execution
- Filter data at database level
- Use `Select()` projections when possible

### 5. **Connection Pooling**
EF Core manages connection pooling automatically.

---

## 🔮 Extensibility Points

### Adding New Features
1. Create new DTOs in `DTOs/`
2. Add validators in `Validators/`
3. Implement service methods in `Services/`
4. Add controller endpoints in `Controllers/`

### Adding New Entities
1. Create entity class in `Models/`
2. Add DbSet to `TaskDbContext`
3. Configure entity in `OnModelCreating`
4. Create migration (when using migrations)

### Adding Middleware
1. Create middleware class in `Middleware/`
2. Register in `Program.cs` using `app.UseMiddleware<>()`

---

## 📚 Further Reading

- [ASP.NET Core Architecture](https://docs.microsoft.com/aspnet/core/fundamentals)
- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
