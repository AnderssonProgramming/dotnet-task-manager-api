using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Data;

/// <summary>
/// Database context for the Task Manager application
/// </summary>
public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// DbSet for TaskItem entities
    /// </summary>
    public DbSet<TaskItem> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure TaskItem entity
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.IsCompleted)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(e => e.Priority)
                .IsRequired()
                .HasDefaultValue(TaskPriority.Medium);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("datetime('now')");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("datetime('now')");

            // Index for performance
            entity.HasIndex(e => e.IsCompleted);
            entity.HasIndex(e => e.Priority);
            entity.HasIndex(e => e.DueDate);
        });

        // Seed initial data
        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem
            {
                Id = 1,
                Title = "Setup Development Environment",
                Description = "Install necessary tools and configure the development environment",
                Priority = TaskPriority.High,
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Id = 2,
                Title = "Design API Architecture",
                Description = "Plan and design the REST API structure and endpoints",
                Priority = TaskPriority.High,
                IsCompleted = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new TaskItem
            {
                Id = 3,
                Title = "Implement CRUD Operations",
                Description = "Create endpoints for Create, Read, Update, and Delete operations",
                Priority = TaskPriority.Medium,
                IsCompleted = false,
                DueDate = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
