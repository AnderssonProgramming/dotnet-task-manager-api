# Task Manager API - Example Requests

This file contains example HTTP requests for testing the API using various tools.

## 🌐 Base URL

```
http://localhost:5000/api
```

---

## 📋 Health Check

### Check API Health

```http
GET /api/health HTTP/1.1
Host: localhost:5000
```

**Expected Response:**
```json
{
  "status": "Healthy",
  "timestamp": "2026-01-04T10:00:00Z",
  "service": "Task Manager API",
  "version": "1.0.0"
}
```

---

## ✅ Task Operations

### 1. Get All Tasks

```http
GET /api/tasks HTTP/1.1
Host: localhost:5000
```

### 2. Get Tasks with Filters

```http
GET /api/tasks?isCompleted=false&priority=2 HTTP/1.1
Host: localhost:5000
```

### 3. Get Specific Task

```http
GET /api/tasks/1 HTTP/1.1
Host: localhost:5000
```

### 4. Create Task

```http
POST /api/tasks HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
  "title": "Complete project documentation",
  "description": "Write comprehensive README and API docs",
  "priority": 2,
  "dueDate": "2026-01-15T00:00:00Z"
}
```

### 5. Create Simple Task

```http
POST /api/tasks HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
  "title": "Buy groceries",
  "priority": 0
}
```

### 6. Update Task

```http
PUT /api/tasks/1 HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
  "title": "Updated task title",
  "description": "Updated description",
  "priority": 3,
  "isCompleted": true
}
```

### 7. Partial Update

```http
PUT /api/tasks/1 HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
  "isCompleted": true
}
```

### 8. Complete Task

```http
PATCH /api/tasks/1/complete HTTP/1.1
Host: localhost:5000
```

### 9. Delete Task

```http
DELETE /api/tasks/1 HTTP/1.1
Host: localhost:5000
```

### 10. Get Statistics

```http
GET /api/tasks/statistics HTTP/1.1
Host: localhost:5000
```

---

## 🧪 Test Scenarios

### Scenario 1: Create Multiple Tasks

```http
# Task 1: High Priority
POST /api/tasks HTTP/1.1
Content-Type: application/json

{
  "title": "Fix critical bug",
  "description": "Production issue needs immediate attention",
  "priority": 3,
  "dueDate": "2026-01-05T00:00:00Z"
}

# Task 2: Medium Priority
POST /api/tasks HTTP/1.1
Content-Type: application/json

{
  "title": "Review pull requests",
  "description": "Review pending PRs from team members",
  "priority": 1
}

# Task 3: Low Priority
POST /api/tasks HTTP/1.1
Content-Type: application/json

{
  "title": "Update dependencies",
  "description": "Update NuGet packages to latest versions",
  "priority": 0,
  "dueDate": "2026-01-20T00:00:00Z"
}
```

### Scenario 2: Complete a Workflow

```http
# 1. Create task
POST /api/tasks HTTP/1.1
Content-Type: application/json

{
  "title": "Write unit tests",
  "priority": 2
}

# 2. Get the created task (assume ID = 4)
GET /api/tasks/4 HTTP/1.1

# 3. Update the task
PUT /api/tasks/4 HTTP/1.1
Content-Type: application/json

{
  "description": "Write tests for TaskService"
}

# 4. Mark as complete
PATCH /api/tasks/4/complete HTTP/1.1

# 5. Verify completion
GET /api/tasks/4 HTTP/1.1
```

### Scenario 3: Filter and Statistics

```http
# 1. Get all pending tasks
GET /api/tasks?isCompleted=false HTTP/1.1

# 2. Get all high priority tasks
GET /api/tasks?priority=2 HTTP/1.1

# 3. Get completed high priority tasks
GET /api/tasks?isCompleted=true&priority=2 HTTP/1.1

# 4. Get overall statistics
GET /api/tasks/statistics HTTP/1.1
```

---

## 🔍 Validation Tests

### Test 1: Empty Title (Should Fail)

```http
POST /api/tasks HTTP/1.1
Content-Type: application/json

{
  "title": "",
  "description": "This should fail validation"
}
```

**Expected Response: 400 Bad Request**

### Test 2: Title Too Long (Should Fail)

```http
POST /api/tasks HTTP/1.1
Content-Type: application/json

{
  "title": "This is a very long title that exceeds the maximum allowed length of 200 characters. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco.",
  "description": "Should fail"
}
```

**Expected Response: 400 Bad Request**

### Test 3: Invalid Priority (Should Fail)

```http
POST /api/tasks HTTP/1.1
Content-Type: application/json

{
  "title": "Test task",
  "priority": 99
}
```

**Expected Response: 400 Bad Request**

### Test 4: Past Due Date (Should Fail)

```http
POST /api/tasks HTTP/1.1
Content-Type: application/json

{
  "title": "Test task",
  "dueDate": "2020-01-01T00:00:00Z"
}
```

**Expected Response: 400 Bad Request**

---

## 🐚 cURL Examples

### Create Task

```bash
curl -X POST "http://localhost:5000/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "New Task from cURL",
    "description": "Testing API with cURL",
    "priority": 1
  }'
```

### Get All Tasks

```bash
curl -X GET "http://localhost:5000/api/tasks"
```

### Get Task by ID

```bash
curl -X GET "http://localhost:5000/api/tasks/1"
```

### Update Task

```bash
curl -X PUT "http://localhost:5000/api/tasks/1" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Updated from cURL",
    "isCompleted": true
  }'
```

### Delete Task

```bash
curl -X DELETE "http://localhost:5000/api/tasks/1"
```

### Get Statistics

```bash
curl -X GET "http://localhost:5000/api/tasks/statistics"
```

---

## 🔧 PowerShell Examples

### Create Task

```powershell
$body = @{
    title = "New Task from PowerShell"
    description = "Testing API with PowerShell"
    priority = 1
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/tasks" `
    -Method Post `
    -ContentType "application/json" `
    -Body $body
```

### Get All Tasks

```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/tasks" `
    -Method Get
```

### Update Task

```powershell
$body = @{
    isCompleted = $true
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5000/api/tasks/1" `
    -Method Put `
    -ContentType "application/json" `
    -Body $body
```

---

## 📝 Notes

- Replace `localhost:5000` with your actual server address in production
- All timestamps are in UTC format (ISO 8601)
- Priority values: 0=Low, 1=Medium, 2=High, 3=Urgent
- The API returns detailed error messages for validation failures
- Use Swagger UI at http://localhost:5000 for interactive testing

---

## 🎯 Quick Test Script

Save this as `test-api.sh` (Linux/Mac) or `test-api.ps1` (Windows):

### Bash Script

```bash
#!/bin/bash
BASE_URL="http://localhost:5000/api"

echo "1. Health Check"
curl -X GET "$BASE_URL/health"

echo -e "\n\n2. Create Task"
curl -X POST "$BASE_URL/tasks" \
  -H "Content-Type: application/json" \
  -d '{"title":"Test Task","priority":1}'

echo -e "\n\n3. Get All Tasks"
curl -X GET "$BASE_URL/tasks"

echo -e "\n\n4. Get Statistics"
curl -X GET "$BASE_URL/tasks/statistics"
```

### PowerShell Script

```powershell
$BASE_URL = "http://localhost:5000/api"

Write-Host "1. Health Check"
Invoke-RestMethod -Uri "$BASE_URL/health"

Write-Host "`n2. Create Task"
$body = @{title="Test Task"; priority=1} | ConvertTo-Json
Invoke-RestMethod -Uri "$BASE_URL/tasks" -Method Post -ContentType "application/json" -Body $body

Write-Host "`n3. Get All Tasks"
Invoke-RestMethod -Uri "$BASE_URL/tasks"

Write-Host "`n4. Get Statistics"
Invoke-RestMethod -Uri "$BASE_URL/tasks/statistics"
```

Run with:
```bash
# Linux/Mac
chmod +x test-api.sh
./test-api.sh

# Windows PowerShell
.\test-api.ps1
```
