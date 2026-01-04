using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models;

/// <summary>
/// Represents a task entity in the system
/// </summary>
public class TaskItem
{
    /// <summary>
    /// Unique identifier for the task
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of the task
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the task
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Indicates whether the task has been completed
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Priority level of the task
    /// </summary>
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    /// <summary>
    /// Due date for the task
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Date and time when the task was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when the task was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Priority levels for tasks
/// </summary>
public enum TaskPriority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Urgent = 3
}
