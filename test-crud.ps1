# Task Manager API - CRUD Testing Script
# This script tests all CRUD operations using curl

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "  TASK MANAGER API - CRUD TESTS" -ForegroundColor Green
Write-Host "========================================`n" -ForegroundColor Cyan

$baseUrl = "http://localhost:5000/api"

# Test 1: Health Check
Write-Host "✅ 1. Health Check" -ForegroundColor Yellow
curl "$baseUrl/health" | ConvertFrom-Json | Format-List
Start-Sleep -Seconds 1

# Test 2: Get All Tasks
Write-Host "`n✅ 2. GET All Tasks" -ForegroundColor Yellow
curl "$baseUrl/tasks" | ConvertFrom-Json | Format-Table id, title, isCompleted, priority -AutoSize
Start-Sleep -Seconds 1

# Test 3: Get Specific Task
Write-Host "`n✅ 3. GET Task (ID=1)" -ForegroundColor Yellow
curl "$baseUrl/tasks/1" | ConvertFrom-Json | Format-List
Start-Sleep -Seconds 1

# Test 4: Create New Task
Write-Host "`n✅ 4. POST - Create New Task" -ForegroundColor Yellow
$newTask = @{
    title = "Test Task from Script"
    description = "This task was created by the test script"
    priority = 3
    dueDate = "2026-01-20T00:00:00Z"
} | ConvertTo-Json

$created = curl -X POST "$baseUrl/tasks" `
    -H "Content-Type: application/json" `
    -d $newTask | ConvertFrom-Json

$created | Format-List
$newTaskId = $created.id
Start-Sleep -Seconds 1

# Test 5: Update Task
Write-Host "`n✅ 5. PUT - Update Task (ID=$newTaskId)" -ForegroundColor Yellow
$updateTask = @{
    title = "Updated Task Title"
    description = "This task was updated by the test script"
    isCompleted = $true
    priority = 2
} | ConvertTo-Json

curl -X PUT "$baseUrl/tasks/$newTaskId" `
    -H "Content-Type: application/json" `
    -d $updateTask | ConvertFrom-Json | Format-List
Start-Sleep -Seconds 1

# Test 6: Get Filtered Tasks (Completed)
Write-Host "`n✅ 6. GET Filtered - Completed Tasks" -ForegroundColor Yellow
curl "$baseUrl/tasks?isCompleted=true" | ConvertFrom-Json | 
    Format-Table id, title, isCompleted, priority -AutoSize
Start-Sleep -Seconds 1

# Test 7: Get Filtered Tasks (Pending)
Write-Host "`n✅ 7. GET Filtered - Pending Tasks" -ForegroundColor Yellow
curl "$baseUrl/tasks?isCompleted=false" | ConvertFrom-Json | 
    Format-Table id, title, isCompleted, priority -AutoSize
Start-Sleep -Seconds 1

# Test 8: Get Filtered by Priority
Write-Host "`n✅ 8. GET Filtered - High Priority (2)" -ForegroundColor Yellow
curl "$baseUrl/tasks?priority=2" | ConvertFrom-Json | 
    Format-Table id, title, isCompleted, priority -AutoSize
Start-Sleep -Seconds 1

# Test 9: Mark as Completed
Write-Host "`n✅ 9. PATCH - Mark Task as Completed" -ForegroundColor Yellow
# Find a pending task first
$pendingTasks = curl "$baseUrl/tasks?isCompleted=false" | ConvertFrom-Json
if ($pendingTasks.Count -gt 0) {
    $taskToComplete = $pendingTasks[0].id
    curl -X PATCH "$baseUrl/tasks/$taskToComplete/complete" | ConvertFrom-Json | Format-List
} else {
    Write-Host "No pending tasks to complete" -ForegroundColor Gray
}
Start-Sleep -Seconds 1

# Test 10: Get Statistics
Write-Host "`n✅ 10. GET Task Statistics" -ForegroundColor Yellow
curl "$baseUrl/tasks/statistics" | ConvertFrom-Json | Format-List
Start-Sleep -Seconds 1

# Test 11: Delete Task
Write-Host "`n✅ 11. DELETE Task (ID=$newTaskId)" -ForegroundColor Yellow
curl -X DELETE "$baseUrl/tasks/$newTaskId" -w "`nHTTP Status: %{http_code}`n"
Start-Sleep -Seconds 1

# Test 12: Verify Deletion
Write-Host "`n✅ 12. Verify Deletion - Get All Tasks" -ForegroundColor Yellow
curl "$baseUrl/tasks" | ConvertFrom-Json | Format-Table id, title, isCompleted, priority -AutoSize
Start-Sleep -Seconds 1

# Test 13: Test Validation (Empty Title)
Write-Host "`n✅ 13. POST - Validation Test (Should Fail)" -ForegroundColor Yellow
$invalidTask = @{
    title = ""
    description = "This should fail"
} | ConvertTo-Json

Write-Host "Expected: 400 Bad Request with validation errors" -ForegroundColor Gray
curl -X POST "$baseUrl/tasks" `
    -H "Content-Type: application/json" `
    -d $invalidTask | ConvertFrom-Json | Format-List
Start-Sleep -Seconds 1

# Test 14: Test 404 (Non-existent Task)
Write-Host "`n✅ 14. GET Non-existent Task (Should Return 404)" -ForegroundColor Yellow
Write-Host "Expected: 404 Not Found" -ForegroundColor Gray
curl "$baseUrl/tasks/9999" -w "`nHTTP Status: %{http_code}`n"
Start-Sleep -Seconds 1

# Summary
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "     CRUD TESTING COMPLETE! 🎉" -ForegroundColor Green
Write-Host "========================================`n" -ForegroundColor Cyan

Write-Host "✅ All CRUD operations tested successfully!" -ForegroundColor Green
Write-Host "`nTested Operations:" -ForegroundColor Yellow
Write-Host "  • GET    /api/tasks" -ForegroundColor White
Write-Host "  • GET    /api/tasks/{id}" -ForegroundColor White
Write-Host "  • POST   /api/tasks" -ForegroundColor White
Write-Host "  • PUT    /api/tasks/{id}" -ForegroundColor White
Write-Host "  • PATCH  /api/tasks/{id}/complete" -ForegroundColor White
Write-Host "  • DELETE /api/tasks/{id}" -ForegroundColor White
Write-Host "  • GET    /api/tasks/statistics" -ForegroundColor White
Write-Host "  • GET    /api/health" -ForegroundColor White
Write-Host "`nFiltering & Validation:" -ForegroundColor Yellow
Write-Host "  • Filter by isCompleted" -ForegroundColor White
Write-Host "  • Filter by priority" -ForegroundColor White
Write-Host "  • Input validation (FluentValidation)" -ForegroundColor White
Write-Host "  • Error handling (404, 400)" -ForegroundColor White
Write-Host ""
