using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs;
using TaskManagerApi.Models;
using TaskManagerApi.Services;

namespace TaskManagerApi.Controllers;

/// <summary>
/// Controller for managing tasks
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IValidator<CreateTaskDto> _createValidator;
    private readonly IValidator<UpdateTaskDto> _updateValidator;
    private readonly ILogger<TasksController> _logger;

    public TasksController(
        ITaskService taskService,
        IValidator<CreateTaskDto> createValidator,
        IValidator<UpdateTaskDto> updateValidator,
        ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _logger = logger;
    }

    /// <summary>
    /// Get all tasks with optional filters
    /// </summary>
    /// <param name="isCompleted">Filter by completion status</param>
    /// <param name="priority">Filter by priority</param>
    /// <returns>List of tasks</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks(
        [FromQuery] bool? isCompleted = null,
        [FromQuery] TaskPriority? priority = null)
    {
        _logger.LogInformation("GET /api/tasks - Fetching all tasks");
        var tasks = await _taskService.GetAllTasksAsync(isCompleted, priority);
        return Ok(tasks);
    }

    /// <summary>
    /// Get a specific task by ID
    /// </summary>
    /// <param name="id">Task ID</param>
    /// <returns>Task details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> GetTask(int id)
    {
        _logger.LogInformation("GET /api/tasks/{Id} - Fetching task", id);
        var task = await _taskService.GetTaskByIdAsync(id);

        if (task == null)
        {
            _logger.LogWarning("Task with ID {Id} not found", id);
            return NotFound(new { message = $"Task with ID {id} not found" });
        }

        return Ok(task);
    }

    /// <summary>
    /// Create a new task
    /// </summary>
    /// <param name="createTaskDto">Task creation data</param>
    /// <returns>Created task</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        _logger.LogInformation("POST /api/tasks - Creating new task");

        var validationResult = await _createValidator.ValidateAsync(createTaskDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                message = "Validation failed",
                errors = validationResult.Errors.Select(e => new
                {
                    property = e.PropertyName,
                    error = e.ErrorMessage
                })
            });
        }

        var createdTask = await _taskService.CreateTaskAsync(createTaskDto);
        return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
    }

    /// <summary>
    /// Update an existing task
    /// </summary>
    /// <param name="id">Task ID</param>
    /// <param name="updateTaskDto">Task update data</param>
    /// <returns>Updated task</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        _logger.LogInformation("PUT /api/tasks/{Id} - Updating task", id);

        var validationResult = await _updateValidator.ValidateAsync(updateTaskDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                message = "Validation failed",
                errors = validationResult.Errors.Select(e => new
                {
                    property = e.PropertyName,
                    error = e.ErrorMessage
                })
            });
        }

        var updatedTask = await _taskService.UpdateTaskAsync(id, updateTaskDto);

        if (updatedTask == null)
        {
            _logger.LogWarning("Task with ID {Id} not found for update", id);
            return NotFound(new { message = $"Task with ID {id} not found" });
        }

        return Ok(updatedTask);
    }

    /// <summary>
    /// Delete a task
    /// </summary>
    /// <param name="id">Task ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        _logger.LogInformation("DELETE /api/tasks/{Id} - Deleting task", id);

        var result = await _taskService.DeleteTaskAsync(id);

        if (!result)
        {
            _logger.LogWarning("Task with ID {Id} not found for deletion", id);
            return NotFound(new { message = $"Task with ID {id} not found" });
        }

        return NoContent();
    }

    /// <summary>
    /// Mark a task as completed
    /// </summary>
    /// <param name="id">Task ID</param>
    /// <returns>Updated task</returns>
    [HttpPatch("{id}/complete")]
    [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> CompleteTask(int id)
    {
        _logger.LogInformation("PATCH /api/tasks/{Id}/complete - Marking task as completed", id);

        var task = await _taskService.CompleteTaskAsync(id);

        if (task == null)
        {
            _logger.LogWarning("Task with ID {Id} not found", id);
            return NotFound(new { message = $"Task with ID {id} not found" });
        }

        return Ok(task);
    }

    /// <summary>
    /// Get task statistics
    /// </summary>
    /// <returns>Task statistics</returns>
    [HttpGet("statistics")]
    [ProducesResponseType(typeof(TaskStatisticsDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<TaskStatisticsDto>> GetStatistics()
    {
        _logger.LogInformation("GET /api/tasks/statistics - Fetching statistics");
        var statistics = await _taskService.GetTaskStatisticsAsync();
        return Ok(statistics);
    }
}
