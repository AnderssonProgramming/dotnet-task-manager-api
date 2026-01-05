# 🧪 Test Scripts

This directory contains automated test scripts for the Task Manager API.

## 📁 Contents

- **test-crud.ps1** - PowerShell test script (Windows)
- **test-crud.sh** - Bash test script (Linux/Mac)

---

## 🚀 Quick Start

### Prerequisites

1. **API must be running**
   ```bash
   cd src/TaskManagerApi
   dotnet run
   ```

2. **curl must be installed** (usually pre-installed on most systems)

---

## 💻 Running Tests

### PowerShell (Windows)

```powershell
# Navigate to tests directory
cd tests

# Run the test script
.\test-crud.ps1
```

### Bash (Linux/Mac)

```bash
# Navigate to tests directory
cd tests

# Make script executable (first time only)
chmod +x test-crud.sh

# Run the test script
./test-crud.sh
```

---

## 📋 What Gets Tested

The automated scripts test all CRUD operations:

### ✅ **Create Operations**
- Creating new tasks with full data
- Creating tasks with minimal data
- Validation testing (empty title, invalid data)

### ✅ **Read Operations**
- Getting all tasks
- Getting specific task by ID
- Filtering by completion status
- Filtering by priority
- Getting task statistics
- Testing 404 errors for non-existent tasks

### ✅ **Update Operations**
- Full task updates (PUT)
- Partial task updates
- Marking tasks as completed (PATCH)

### ✅ **Delete Operations**
- Deleting existing tasks
- Verifying deletion
- Testing 404 for deleted tasks

### ✅ **Additional Tests**
- Health check endpoint
- Input validation (FluentValidation)
- Error handling (400, 404 status codes)
- Response format validation

---

## 📊 Expected Output

Both scripts provide:
- ✅ Color-coded test results (PASSED/FAILED)
- 📈 Test statistics
- 📝 Detailed response data for each test
- 🎯 Summary of all operations tested

### Success Indicators
- All tests return appropriate HTTP status codes
- Data validation works correctly
- CRUD operations function as expected
- Error handling responds properly

---

## 🎯 Test Coverage

| Category | Tests | Coverage |
|----------|-------|----------|
| **CRUD Operations** | 8 | Complete |
| **Filtering** | 3 | Complete |
| **Validation** | 2 | Complete |
| **Error Handling** | 2 | Complete |
| **Statistics** | 1 | Complete |
| **Health Check** | 1 | Complete |
| **Total** | **17** | **100%** |

---

## 🔧 Customization

### Changing Base URL

Edit the base URL in the scripts if your API runs on a different port:

**PowerShell:**
```powershell
$baseUrl = "http://localhost:YOUR_PORT/api"
```

**Bash:**
```bash
BASE_URL="http://localhost:YOUR_PORT/api"
```

### Adding New Tests

Both scripts are structured to make adding tests easy:

1. Add a new test section with clear description
2. Use curl to make the API call
3. Parse and display the response
4. Add a sleep delay between tests

Example:
```powershell
Write-Host "`n✅ XX. Your New Test" -ForegroundColor Yellow
curl "$baseUrl/your-endpoint" | ConvertFrom-Json | Format-List
Start-Sleep -Seconds 1
```

---

## 📚 Related Documentation

- [API Documentation](../docs/API.md) - Complete API reference
- [Test Results](../docs/TEST_RESULTS.md) - Detailed test results
- [Examples](../docs/EXAMPLES.md) - Manual testing examples
- [Development Guide](../docs/DEVELOPMENT.md) - Development setup

---

## 🐛 Troubleshooting

### API Not Running
```
Error: Failed to connect to localhost port 5000
Solution: Start the API first with `dotnet run`
```

### curl Not Found
```
Error: 'curl' is not recognized
Solution: Install curl or use built-in PowerShell Invoke-WebRequest
```

### Permission Denied (Bash)
```
Error: Permission denied: ./test-crud.sh
Solution: chmod +x test-crud.sh
```

### Script Execution Policy (PowerShell)
```
Error: Running scripts is disabled on this system
Solution: Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

---

## 💡 Tips

- **Run tests sequentially** - Don't run multiple test scripts simultaneously
- **Clean state** - Restart the API between test runs for consistent results
- **Check logs** - API logs provide additional debugging information
- **Modify data** - Feel free to modify test data in the scripts
- **CI/CD Integration** - These scripts can be integrated into CI/CD pipelines

---

## 🎓 Learning

These test scripts demonstrate:
- ✅ RESTful API testing patterns
- ✅ curl usage for API testing
- ✅ Automated test execution
- ✅ Response parsing and validation
- ✅ Test organization and structure

---

<div align="center">

**🧪 Happy Testing! 🧪**

[Back to Main README](../README.md) • [View Test Results](../docs/TEST_RESULTS.md)

</div>
