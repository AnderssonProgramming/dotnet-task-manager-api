using TaskManagerApi.Models;

namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for creating a new task
/// </summary>
public class CreateTaskDto
{
    /// <summary>
    /// Title of the task
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description of the task
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Priority level of the task
    /// </summary>
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    /// <summary>
    /// Due date for the task
    /// </summary>
    public DateTime? DueDate { get; set; }
}

/// <summary>
/// DTO for updating an existing task
/// </summary>
public class UpdateTaskDto
{
    /// <summary>
    /// Title of the task
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Description of the task
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the task is completed
    /// </summary>
    public bool? IsCompleted { get; set; }

    /// <summary>
    /// Priority level of the task
    /// </summary>
    public TaskPriority? Priority { get; set; }

    /// <summary>
    /// Due date for the task
    /// </summary>
    public DateTime? DueDate { get; set; }
}

/// <summary>
/// DTO for task responses
/// </summary>
public class TaskDto
{
    /// <summary>
    /// Unique identifier for the task
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of the task
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description of the task
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the task is completed
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Priority level of the task
    /// </summary>
    public TaskPriority Priority { get; set; }

    /// <summary>
    /// Due date for the task
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Last update timestamp
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}
