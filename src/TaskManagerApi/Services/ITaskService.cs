using TaskManagerApi.DTOs;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

/// <summary>
/// Interface for task service operations
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Retrieves all tasks with optional filtering
    /// </summary>
    Task<IEnumerable<TaskDto>> GetAllTasksAsync(bool? isCompleted = null, TaskPriority? priority = null);

    /// <summary>
    /// Retrieves a specific task by ID
    /// </summary>
    Task<TaskDto?> GetTaskByIdAsync(int id);

    /// <summary>
    /// Creates a new task
    /// </summary>
    Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);

    /// <summary>
    /// Updates an existing task
    /// </summary>
    Task<TaskDto?> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto);

    /// <summary>
    /// Deletes a task
    /// </summary>
    Task<bool> DeleteTaskAsync(int id);

    /// <summary>
    /// Marks a task as completed
    /// </summary>
    Task<TaskDto?> CompleteTaskAsync(int id);

    /// <summary>
    /// Gets statistics about tasks
    /// </summary>
    Task<TaskStatisticsDto> GetTaskStatisticsAsync();
}
