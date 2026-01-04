# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Planned Features
- Authentication and Authorization (JWT)
- Unit and Integration Tests
- Pagination for large datasets
- Caching with Redis
- Docker support
- CI/CD pipeline with GitHub Actions
- Database migrations support
- Rate limiting

## [1.0.0] - 2026-01-04

### Added
- Initial release of Task Manager API
- Complete CRUD operations for tasks
- RESTful API endpoints with proper HTTP methods
- Task priority management (Low, Medium, High, Urgent)
- Task filtering by completion status and priority
- Task statistics endpoint
- FluentValidation for input validation
- Entity Framework Core with SQLite database
- Dependency Injection architecture
- Swagger/OpenAPI documentation
- Serilog structured logging
- Global exception handling middleware
- Health check endpoint
- Comprehensive README with architecture diagrams
- API documentation
- Development guide
- Architecture documentation
- Contributing guidelines
- MIT License

### Technical Details
- ASP.NET Core 8.0
- C# 12
- Entity Framework Core 8.0
- SQLite database
- Clean Architecture implementation
- SOLID principles
- DTO pattern for API contracts
- Service layer for business logic
- Repository pattern through EF Core

### API Endpoints
- `GET /api/tasks` - Get all tasks with filtering
- `GET /api/tasks/{id}` - Get specific task
- `POST /api/tasks` - Create new task
- `PUT /api/tasks/{id}` - Update task
- `DELETE /api/tasks/{id}` - Delete task
- `PATCH /api/tasks/{id}/complete` - Mark task as completed
- `GET /api/tasks/statistics` - Get task statistics
- `GET /api/health` - Health check

### Documentation
- README.md with features and getting started guide
- API.md with complete API reference
- ARCHITECTURE.md with architectural overview
- DEVELOPMENT.md with development guide
- CONTRIBUTING.md with contribution guidelines
- CHANGELOG.md (this file)

---

## Version History

### Legend
- `Added` - New features
- `Changed` - Changes in existing functionality
- `Deprecated` - Soon-to-be removed features
- `Removed` - Removed features
- `Fixed` - Bug fixes
- `Security` - Security improvements

---

[Unreleased]: https://github.com/AnderssonProgramming/dotnet-task-manager-api/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/AnderssonProgramming/dotnet-task-manager-api/releases/tag/v1.0.0
