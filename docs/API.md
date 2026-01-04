# API Documentation

## Overview

The Task Manager API provides a comprehensive set of endpoints for managing tasks with full CRUD operations, filtering, and statistics.

## Base URL

```
http://localhost:5000/api
```

## Response Format

All responses are in JSON format.

### Success Response

```json
{
  "id": 1,
  "title": "Task Title",
  "description": "Task Description",
  "isCompleted": false,
  "priority": 1,
  "dueDate": "2026-01-15T00:00:00Z",
  "createdAt": "2026-01-04T10:00:00Z",
  "updatedAt": "2026-01-04T10:00:00Z"
}
```

### Error Response

```json
{
  "statusCode": 400,
  "message": "Validation failed",
  "errors": [
    {
      "property": "Title",
      "error": "Title is required"
    }
  ]
}
```

## Endpoints

### Health Check

#### GET /api/health

Check API health status.

**Response (200 OK)**

```json
{
  "status": "Healthy",
  "timestamp": "2026-01-04T10:00:00Z",
  "service": "Task Manager API",
  "version": "1.0.0"
}
```

---

### Tasks

#### GET /api/tasks

Get all tasks with optional filters.

**Query Parameters**

| Parameter | Type | Description |
|-----------|------|-------------|
| `isCompleted` | boolean | Filter by completion status |
| `priority` | integer | Filter by priority (0-3) |

**Example Request**

```
GET /api/tasks?isCompleted=false&priority=2
```

**Response (200 OK)**

```json
[
  {
    "id": 1,
    "title": "Complete documentation",
    "description": "Write API docs",
    "isCompleted": false,
    "priority": 2,
    "dueDate": "2026-01-15T00:00:00Z",
    "createdAt": "2026-01-04T10:00:00Z",
    "updatedAt": "2026-01-04T10:00:00Z"
  }
]
```

---

#### GET /api/tasks/{id}

Get a specific task by ID.

**Parameters**

| Parameter | Type | Description |
|-----------|------|-------------|
| `id` | integer | Task ID |

**Response (200 OK)**

```json
{
  "id": 1,
  "title": "Complete documentation",
  "description": "Write API docs",
  "isCompleted": false,
  "priority": 2,
  "dueDate": "2026-01-15T00:00:00Z",
  "createdAt": "2026-01-04T10:00:00Z",
  "updatedAt": "2026-01-04T10:00:00Z"
}
```

**Response (404 Not Found)**

```json
{
  "message": "Task with ID 1 not found"
}
```

---

#### POST /api/tasks

Create a new task.

**Request Body**

```json
{
  "title": "Complete documentation",
  "description": "Write comprehensive API documentation",
  "priority": 2,
  "dueDate": "2026-01-15T00:00:00Z"
}
```

**Field Validation**

| Field | Type | Required | Constraints |
|-------|------|----------|-------------|
| `title` | string | Yes | Max 200 characters |
| `description` | string | No | Max 1000 characters |
| `priority` | integer | No | 0-3 (Low, Medium, High, Urgent) |
| `dueDate` | datetime | No | Cannot be in the past |

**Response (201 Created)**

```json
{
  "id": 4,
  "title": "Complete documentation",
  "description": "Write comprehensive API documentation",
  "isCompleted": false,
  "priority": 2,
  "dueDate": "2026-01-15T00:00:00Z",
  "createdAt": "2026-01-04T10:30:00Z",
  "updatedAt": "2026-01-04T10:30:00Z"
}
```

**Response (400 Bad Request)**

```json
{
  "message": "Validation failed",
  "errors": [
    {
      "property": "Title",
      "error": "Title is required"
    }
  ]
}
```

---

#### PUT /api/tasks/{id}

Update an existing task.

**Parameters**

| Parameter | Type | Description |
|-----------|------|-------------|
| `id` | integer | Task ID |

**Request Body** (all fields optional)

```json
{
  "title": "Updated title",
  "description": "Updated description",
  "isCompleted": true,
  "priority": 3,
  "dueDate": "2026-01-20T00:00:00Z"
}
```

**Response (200 OK)**

```json
{
  "id": 1,
  "title": "Updated title",
  "description": "Updated description",
  "isCompleted": true,
  "priority": 3,
  "dueDate": "2026-01-20T00:00:00Z",
  "createdAt": "2026-01-04T10:00:00Z",
  "updatedAt": "2026-01-04T11:00:00Z"
}
```

**Response (404 Not Found)**

```json
{
  "message": "Task with ID 1 not found"
}
```

---

#### DELETE /api/tasks/{id}

Delete a task.

**Parameters**

| Parameter | Type | Description |
|-----------|------|-------------|
| `id` | integer | Task ID |

**Response (204 No Content)**

No response body.

**Response (404 Not Found)**

```json
{
  "message": "Task with ID 1 not found"
}
```

---

#### PATCH /api/tasks/{id}/complete

Mark a task as completed.

**Parameters**

| Parameter | Type | Description |
|-----------|------|-------------|
| `id` | integer | Task ID |

**Response (200 OK)**

```json
{
  "id": 1,
  "title": "Complete documentation",
  "description": "Write API docs",
  "isCompleted": true,
  "priority": 2,
  "dueDate": "2026-01-15T00:00:00Z",
  "createdAt": "2026-01-04T10:00:00Z",
  "updatedAt": "2026-01-04T12:00:00Z"
}
```

---

#### GET /api/tasks/statistics

Get task statistics and analytics.

**Response (200 OK)**

```json
{
  "totalTasks": 15,
  "completedTasks": 8,
  "pendingTasks": 7,
  "overdueTasks": 2,
  "tasksByPriority": {
    "Low": 3,
    "Medium": 6,
    "High": 4,
    "Urgent": 2
  }
}
```

---

## Priority Levels

| Value | Name | Description |
|-------|------|-------------|
| 0 | Low | Low priority tasks |
| 1 | Medium | Normal priority tasks |
| 2 | High | Important tasks |
| 3 | Urgent | Critical tasks |

---

## HTTP Status Codes

| Code | Description |
|------|-------------|
| 200 | OK - Request succeeded |
| 201 | Created - Resource created successfully |
| 204 | No Content - Request succeeded, no content returned |
| 400 | Bad Request - Invalid request data |
| 404 | Not Found - Resource not found |
| 500 | Internal Server Error - Server error occurred |

---

## Rate Limiting

Currently, there are no rate limits. This may be implemented in future versions.

---

## Versioning

Current API version: **v1**

The API version is not currently included in the URL. Future versions may use versioning like `/api/v2/tasks`.

---

## Support

For issues, questions, or contributions, please visit:
- GitHub Repository: [https://github.com/AnderssonProgramming/dotnet-task-manager-api](https://github.com/AnderssonProgramming/dotnet-task-manager-api)
- Open an Issue: [https://github.com/AnderssonProgramming/dotnet-task-manager-api/issues](https://github.com/AnderssonProgramming/dotnet-task-manager-api/issues)
