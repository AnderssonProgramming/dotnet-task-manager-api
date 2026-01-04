# Contributing to Task Manager API

Thank you for your interest in contributing to the Task Manager API! This document provides guidelines and instructions for contributing.

## 🤝 How to Contribute

### Reporting Bugs

If you find a bug, please create an issue with:
- Clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Environment details (OS, .NET version, etc.)

### Suggesting Features

Feature suggestions are welcome! Please:
- Check if the feature has already been requested
- Clearly describe the feature and its benefits
- Provide examples of how it would be used

### Pull Requests

1. **Fork the repository**
   ```bash
   git clone https://github.com/AnderssonProgramming/dotnet-task-manager-api.git
   ```

2. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Make your changes**
   - Write clean, maintainable code
   - Follow existing code style
   - Add/update tests as needed
   - Update documentation

4. **Commit your changes**
   Use conventional commits:
   ```bash
   git commit -m "feat: add new feature"
   git commit -m "fix: resolve bug in service"
   git commit -m "docs: update API documentation"
   ```

   **Commit Types:**
   - `feat`: New feature
   - `fix`: Bug fix
   - `docs`: Documentation changes
   - `style`: Code style changes (formatting, etc.)
   - `refactor`: Code refactoring
   - `test`: Adding/updating tests
   - `chore`: Maintenance tasks

5. **Push to your fork**
   ```bash
   git push origin feature/your-feature-name
   ```

6. **Create a Pull Request**
   - Provide a clear description
   - Reference any related issues
   - Ensure all tests pass

## 📋 Code Style Guidelines

### C# Conventions
- Use PascalCase for class names and public members
- Use camelCase for private fields and local variables
- Use meaningful variable names
- Add XML documentation comments for public APIs
- Keep methods small and focused

### Example
```csharp
/// <summary>
/// Creates a new task in the system
/// </summary>
/// <param name="createTaskDto">Task creation data</param>
/// <returns>Created task</returns>
public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
{
    // Implementation
}
```

## 🧪 Testing

- Write unit tests for new features
- Ensure all existing tests pass
- Aim for high code coverage

## 📝 Documentation

- Update README.md if needed
- Update API.md for API changes
- Add code comments for complex logic

## ✅ Checklist

Before submitting a PR, ensure:
- [ ] Code follows project conventions
- [ ] All tests pass
- [ ] Documentation is updated
- [ ] Commit messages follow conventional format
- [ ] No merge conflicts
- [ ] Code is reviewed and self-tested

## 🌟 Recognition

Contributors will be recognized in the project documentation.

## 📞 Questions?

Feel free to open an issue for any questions or clarifications.

Thank you for contributing! 🎉
