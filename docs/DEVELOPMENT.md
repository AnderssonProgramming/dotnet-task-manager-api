# Development Guide

## 🛠️ Development Setup

### Prerequisites

1. **.NET 8.0 SDK** or later
   - Download: https://dotnet.microsoft.com/download

2. **IDE** (choose one)
   - Visual Studio 2022 (recommended)
   - Visual Studio Code with C# extension
   - JetBrains Rider

3. **Git** for version control
   - Download: https://git-scm.com/

4. **Optional Tools**
   - Postman or Insomnia (API testing)
   - DB Browser for SQLite (database inspection)

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/AnderssonProgramming/dotnet-task-manager-api.git
cd dotnet-task-manager-api
```

### 2. Restore Dependencies

```bash
dotnet restore
```

This downloads all NuGet packages specified in the project file.

### 3. Build the Project

```bash
dotnet build
```

Compiles the project and checks for errors.

### 4. Run the Application

```bash
cd src/TaskManagerApi
dotnet run
```

Or press `F5` in Visual Studio to run with debugger.

### 5. Access the API

- Swagger UI: http://localhost:5000
- API Base: http://localhost:5000/api
- Health Check: http://localhost:5000/api/health

---

## 📁 Project Structure

```
TaskManagerApi/
├── Controllers/          # API endpoints
├── Services/            # Business logic
├── Data/               # Database context
├── Models/             # Domain entities
├── DTOs/               # Data transfer objects
├── Validators/         # Input validation
├── Middleware/         # Custom middleware
├── Properties/         # Launch settings
├── Program.cs          # Application entry point
├── appsettings.json    # Configuration
└── TaskManagerApi.csproj
```

---

## 🔧 Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=taskmanager.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information"
    }
  }
}
```

### Environment Variables

For production, consider using environment variables:

```bash
# Windows
set ASPNETCORE_ENVIRONMENT=Production

# Linux/Mac
export ASPNETCORE_ENVIRONMENT=Production
```

---

## 🗄️ Database Management

### Using SQLite

The project uses SQLite by default. The database file is created automatically on first run.

**Location:** `src/TaskManagerApi/taskmanager.db`

### Database Schema

The database schema is created by Entity Framework Core based on the models.

```csharp
// In TaskDbContext.cs
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Entity configuration and seed data
}
```

### Seed Data

Initial data is seeded automatically when the database is created:

```csharp
modelBuilder.Entity<TaskItem>().HasData(
    new TaskItem { Id = 1, Title = "Setup Development Environment", ... },
    new TaskItem { Id = 2, Title = "Design API Architecture", ... }
);
```

### Inspecting the Database

Use **DB Browser for SQLite** to inspect the database:

1. Download from https://sqlitebrowser.org/
2. Open `taskmanager.db`
3. Browse data and schema

---

## 🧪 Testing

### Manual Testing with Swagger

1. Run the application
2. Navigate to http://localhost:5000
3. Explore and test endpoints interactively

### Testing with cURL

```bash
# Create a task
curl -X POST http://localhost:5000/api/tasks \
  -H "Content-Type: application/json" \
  -d '{"title":"Test Task","priority":1}'

# Get all tasks
curl http://localhost:5000/api/tasks

# Get specific task
curl http://localhost:5000/api/tasks/1

# Update task
curl -X PUT http://localhost:5000/api/tasks/1 \
  -H "Content-Type: application/json" \
  -d '{"isCompleted":true}'

# Delete task
curl -X DELETE http://localhost:5000/api/tasks/1
```

### Testing with Postman

1. Import the API collection (create one from Swagger)
2. Set base URL: `http://localhost:5000`
3. Test each endpoint with different scenarios

---

## 🐛 Debugging

### Visual Studio

1. Set breakpoints by clicking the left margin
2. Press `F5` to start debugging
3. Use debugging tools (Watch, Locals, Call Stack)

### VS Code

1. Install C# extension
2. Set breakpoints
3. Press `F5` and select `.NET Core Launch`
4. Use Debug Console for output

### Logging

The application uses Serilog for logging:

```csharp
_logger.LogInformation("Creating new task");
_logger.LogWarning("Task not found");
_logger.LogError(ex, "Error occurred");
```

**Log Locations:**
- Console output
- `logs/taskmanager-{Date}.txt`

---

## 📝 Adding New Features

### Example: Adding a "Tags" Feature

#### 1. Create the Model

```csharp
// Models/Tag.cs
public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TaskItem> Tasks { get; set; } = new();
}

// Update TaskItem.cs
public class TaskItem
{
    // ... existing properties
    public List<Tag> Tags { get; set; } = new();
}
```

#### 2. Update DbContext

```csharp
public DbSet<Tag> Tags { get; set; } = null!;

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configure many-to-many relationship
    modelBuilder.Entity<TaskItem>()
        .HasMany(t => t.Tags)
        .WithMany(t => t.Tasks);
}
```

#### 3. Create DTOs

```csharp
// DTOs/TagDto.cs
public class TagDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
```

#### 4. Add Service Methods

```csharp
// Services/ITaskService.cs
Task<TaskDto> AddTagToTaskAsync(int taskId, string tagName);

// Services/TaskService.cs
public async Task<TaskDto> AddTagToTaskAsync(int taskId, string tagName)
{
    // Implementation
}
```

#### 5. Create Controller Endpoints

```csharp
[HttpPost("{id}/tags")]
public async Task<ActionResult<TaskDto>> AddTag(int id, [FromBody] string tagName)
{
    // Implementation
}
```

---

## 🔄 Development Workflow

### 1. **Create Feature Branch**
```bash
git checkout -b feature/add-tags
```

### 2. **Make Changes**
- Write code
- Test locally
- Add documentation

### 3. **Commit Changes**
```bash
git add .
git commit -m "feat: add tags functionality"
```

### 4. **Push and Create PR**
```bash
git push origin feature/add-tags
```

---

## 🎨 Code Style

### Naming Conventions

```csharp
// PascalCase for classes, methods, properties
public class TaskService { }
public void CreateTask() { }
public string Title { get; set; }

// camelCase for parameters and local variables
public void ProcessTask(int taskId)
{
    var result = DoSomething();
}

// _camelCase for private fields
private readonly ITaskService _taskService;
```

### Documentation

Add XML comments for public APIs:

```csharp
/// <summary>
/// Creates a new task in the system
/// </summary>
/// <param name="createTaskDto">Task creation data</param>
/// <returns>The created task</returns>
public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
{
    // Implementation
}
```

---

## 📊 Performance Tips

1. **Use async/await** for all I/O operations
2. **Use `IQueryable`** for deferred execution
3. **Project DTOs directly** when possible
4. **Add indexes** for frequently queried fields
5. **Use pagination** for large result sets

---

## 🔒 Security Best Practices

1. **Validate all inputs** using FluentValidation
2. **Use parameterized queries** (EF Core does this automatically)
3. **Sanitize error messages** before sending to client
4. **Implement authentication/authorization** for production
5. **Use HTTPS** in production

---

## 📚 Useful Commands

```bash
# Build
dotnet build

# Run
dotnet run

# Run with watch (auto-reload)
dotnet watch run

# Clean build artifacts
dotnet clean

# Run tests (when added)
dotnet test

# Publish for deployment
dotnet publish -c Release
```

---

## 🆘 Troubleshooting

### Port Already in Use

Change port in `launchSettings.json`:

```json
"applicationUrl": "http://localhost:5001"
```

### Database Locked

Close any applications accessing `taskmanager.db`.

### Dependency Issues

```bash
dotnet clean
dotnet restore
dotnet build
```

### Missing NuGet Packages

```bash
dotnet restore
```

---

## 📞 Getting Help

- Check the [README](../README.md)
- Review [API Documentation](API.md)
- Read [Architecture Guide](ARCHITECTURE.md)
- Open an issue on GitHub
- Check ASP.NET Core docs: https://docs.microsoft.com/aspnet/core

---

## 🎯 Next Steps

1. Explore the codebase
2. Run the application
3. Test endpoints with Swagger
4. Try adding a new feature
5. Contribute to the project!

Happy coding! 🚀
