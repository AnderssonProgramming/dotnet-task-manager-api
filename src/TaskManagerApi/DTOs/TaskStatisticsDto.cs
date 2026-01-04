namespace TaskManagerApi.DTOs;

/// <summary>
/// DTO for task statistics
/// </summary>
public class TaskStatisticsDto
{
    /// <summary>
    /// Total number of tasks
    /// </summary>
    public int TotalTasks { get; set; }

    /// <summary>
    /// Number of completed tasks
    /// </summary>
    public int CompletedTasks { get; set; }

    /// <summary>
    /// Number of pending tasks
    /// </summary>
    public int PendingTasks { get; set; }

    /// <summary>
    /// Number of overdue tasks
    /// </summary>
    public int OverdueTasks { get; set; }

    /// <summary>
    /// Count of tasks by priority level
    /// </summary>
    public Dictionary<string, int> TasksByPriority { get; set; } = new();
}
