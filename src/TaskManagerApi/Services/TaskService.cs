using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

/// <summary>
/// Service implementation for task operations
/// </summary>
public class TaskService : ITaskService
{
    private readonly TaskDbContext _context;
    private readonly ILogger<TaskService> _logger;

    public TaskService(TaskDbContext context, ILogger<TaskService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(bool? isCompleted = null, TaskPriority? priority = null)
    {
        _logger.LogInformation("Fetching all tasks with filters - IsCompleted: {IsCompleted}, Priority: {Priority}", 
            isCompleted, priority);

        var query = _context.Tasks.AsQueryable();

        if (isCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == isCompleted.Value);
        }

        if (priority.HasValue)
        {
            query = query.Where(t => t.Priority == priority.Value);
        }

        var tasks = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return tasks.Select(MapToDto);
    }

    public async Task<TaskDto?> GetTaskByIdAsync(int id)
    {
        _logger.LogInformation("Fetching task with ID: {TaskId}", id);

        var task = await _context.Tasks.FindAsync(id);
        return task == null ? null : MapToDto(task);
    }

    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
    {
        _logger.LogInformation("Creating new task with title: {Title}", createTaskDto.Title);

        var taskItem = new TaskItem
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            Priority = createTaskDto.Priority,
            DueDate = createTaskDto.DueDate,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Tasks.Add(taskItem);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Task created successfully with ID: {TaskId}", taskItem.Id);

        return MapToDto(taskItem);
    }

    public async Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto)
    {
        _logger.LogInformation("Updating task with ID: {TaskId}", id);

        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            _logger.LogWarning("Task with ID: {TaskId} not found for update", id);
            return null;
        }

        // Update only provided fields
        if (updateTaskDto.Title != null)
            task.Title = updateTaskDto.Title;

        if (updateTaskDto.Description != null)
            task.Description = updateTaskDto.Description;

        if (updateTaskDto.IsCompleted.HasValue)
            task.IsCompleted = updateTaskDto.IsCompleted.Value;

        if (updateTaskDto.Priority.HasValue)
            task.Priority = updateTaskDto.Priority.Value;

        if (updateTaskDto.DueDate != null)
            task.DueDate = updateTaskDto.DueDate;

        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Task with ID: {TaskId} updated successfully", id);

        return MapToDto(task);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        _logger.LogInformation("Deleting task with ID: {TaskId}", id);

        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            _logger.LogWarning("Task with ID: {TaskId} not found for deletion", id);
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Task with ID: {TaskId} deleted successfully", id);

        return true;
    }

    public async Task<TaskDto?> CompleteTaskAsync(int id)
    {
        _logger.LogInformation("Marking task with ID: {TaskId} as completed", id);

        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            _logger.LogWarning("Task with ID: {TaskId} not found", id);
            return null;
        }

        task.IsCompleted = true;
        task.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Task with ID: {TaskId} marked as completed", id);

        return MapToDto(task);
    }

    public async Task<TaskStatisticsDto> GetTaskStatisticsAsync()
    {
        _logger.LogInformation("Calculating task statistics");

        var totalTasks = await _context.Tasks.CountAsync();
        var completedTasks = await _context.Tasks.CountAsync(t => t.IsCompleted);
        var pendingTasks = totalTasks - completedTasks;
        var overdueTasks = await _context.Tasks.CountAsync(t => 
            !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value < DateTime.UtcNow);

        var tasksByPriority = await _context.Tasks
            .GroupBy(t => t.Priority)
            .Select(g => new { Priority = g.Key, Count = g.Count() })
            .ToListAsync();

        return new TaskStatisticsDto
        {
            TotalTasks = totalTasks,
            CompletedTasks = completedTasks,
            PendingTasks = pendingTasks,
            OverdueTasks = overdueTasks,
            TasksByPriority = tasksByPriority.ToDictionary(
                x => x.Priority.ToString(),
                x => x.Count
            )
        };
    }

    private static TaskDto MapToDto(TaskItem task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            Priority = task.Priority,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }
}
