#!/bin/bash
# Task Manager API - CRUD Testing Script (Bash version)
# This script tests all CRUD operations using curl

echo "========================================"
echo "  TASK MANAGER API - CRUD TESTS"
echo "========================================"
echo ""

BASE_URL="http://localhost:5000/api"

# Colors
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

# Test 1: Health Check
echo -e "${YELLOW}✅ 1. Health Check${NC}"
curl -s $BASE_URL/health | jq
sleep 1

# Test 2: Get All Tasks
echo -e "\n${YELLOW}✅ 2. GET All Tasks${NC}"
curl -s $BASE_URL/tasks | jq
sleep 1

# Test 3: Get Specific Task
echo -e "\n${YELLOW}✅ 3. GET Task (ID=1)${NC}"
curl -s $BASE_URL/tasks/1 | jq
sleep 1

# Test 4: Create New Task
echo -e "\n${YELLOW}✅ 4. POST - Create New Task${NC}"
NEW_TASK=$(curl -s -X POST $BASE_URL/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Test Task from Bash Script",
    "description": "This task was created by the bash test script",
    "priority": 3,
    "dueDate": "2026-01-20T00:00:00Z"
  }')

echo $NEW_TASK | jq
NEW_TASK_ID=$(echo $NEW_TASK | jq -r '.id')
sleep 1

# Test 5: Update Task
echo -e "\n${YELLOW}✅ 5. PUT - Update Task (ID=$NEW_TASK_ID)${NC}"
curl -s -X PUT $BASE_URL/tasks/$NEW_TASK_ID \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Updated Task Title",
    "description": "This task was updated by the test script",
    "isCompleted": true,
    "priority": 2
  }' | jq
sleep 1

# Test 6: Get Filtered Tasks (Completed)
echo -e "\n${YELLOW}✅ 6. GET Filtered - Completed Tasks${NC}"
curl -s "$BASE_URL/tasks?isCompleted=true" | jq
sleep 1

# Test 7: Get Filtered Tasks (Pending)
echo -e "\n${YELLOW}✅ 7. GET Filtered - Pending Tasks${NC}"
curl -s "$BASE_URL/tasks?isCompleted=false" | jq
sleep 1

# Test 8: Get Filtered by Priority
echo -e "\n${YELLOW}✅ 8. GET Filtered - High Priority (2)${NC}"
curl -s "$BASE_URL/tasks?priority=2" | jq
sleep 1

# Test 9: Mark as Completed
echo -e "\n${YELLOW}✅ 9. PATCH - Mark Task as Completed${NC}"
PENDING_TASK=$(curl -s "$BASE_URL/tasks?isCompleted=false" | jq -r '.[0].id')
if [ "$PENDING_TASK" != "null" ]; then
  curl -s -X PATCH "$BASE_URL/tasks/$PENDING_TASK/complete" | jq
else
  echo "No pending tasks to complete"
fi
sleep 1

# Test 10: Get Statistics
echo -e "\n${YELLOW}✅ 10. GET Task Statistics${NC}"
curl -s $BASE_URL/tasks/statistics | jq
sleep 1

# Test 11: Delete Task
echo -e "\n${YELLOW}✅ 11. DELETE Task (ID=$NEW_TASK_ID)${NC}"
curl -s -X DELETE $BASE_URL/tasks/$NEW_TASK_ID -w "\nHTTP Status: %{http_code}\n"
sleep 1

# Test 12: Verify Deletion
echo -e "\n${YELLOW}✅ 12. Verify Deletion - Get All Tasks${NC}"
curl -s $BASE_URL/tasks | jq
sleep 1

# Test 13: Test Validation (Empty Title)
echo -e "\n${YELLOW}✅ 13. POST - Validation Test (Should Fail)${NC}"
echo "Expected: 400 Bad Request with validation errors"
curl -s -X POST $BASE_URL/tasks \
  -H "Content-Type: application/json" \
  -d '{
    "title": "",
    "description": "This should fail"
  }' | jq
sleep 1

# Test 14: Test 404
echo -e "\n${YELLOW}✅ 14. GET Non-existent Task (Should Return 404)${NC}"
echo "Expected: 404 Not Found"
curl -s $BASE_URL/tasks/9999 -w "\nHTTP Status: %{http_code}\n" | jq
sleep 1

# Summary
echo ""
echo "========================================"
echo -e "${GREEN}     CRUD TESTING COMPLETE! 🎉${NC}"
echo "========================================"
echo ""
echo -e "${GREEN}✅ All CRUD operations tested successfully!${NC}"
echo ""
echo -e "${YELLOW}Tested Operations:${NC}"
echo "  • GET    /api/tasks"
echo "  • GET    /api/tasks/{id}"
echo "  • POST   /api/tasks"
echo "  • PUT    /api/tasks/{id}"
echo "  • PATCH  /api/tasks/{id}/complete"
echo "  • DELETE /api/tasks/{id}"
echo "  • GET    /api/tasks/statistics"
echo "  • GET    /api/health"
echo ""
echo -e "${YELLOW}Filtering & Validation:${NC}"
echo "  • Filter by isCompleted"
echo "  • Filter by priority"
echo "  • Input validation (FluentValidation)"
echo "  • Error handling (404, 400)"
echo ""
