using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Application.Interfaces.Repository;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Domain.Enums;
using ProjectManagementApp.Infrastructure.EF;
using TaskStatus = ProjectManagementApp.Domain.Enums.TaskStatus;

namespace ProjectManagementApp.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Tasks task)
        {
            string validationMessage;
            if (!task.IsValid(out validationMessage))
            {
                throw new ArgumentException(validationMessage); // Handle validation failure
            }

            await _context.Tasks.AddAsync(task);
        }
        public async Task<IEnumerable<Tasks>> GetAllAsync(int projectId, int pageNumber, int pageSize)
        {
            return await _context.Tasks
                .Where(t => t.ProjectId == projectId && !t.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<Tasks>> GetAllByProjectIdAsync(int projectId)
        {
            return await _context.Tasks
                                 .Where(t => t.ProjectId == projectId && !t.IsDeleted)
                                 .ToListAsync(); // Ensure you have Microsoft.EntityFrameworkCore namespace
        }

        public async Task<Tasks> GetByIdAsync(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        public async Task UpdateAsync(Tasks task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync(); // Save changes after update
        }

        public async Task DeleteAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.IsDeleted = true;
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync(); // Save changes after deletion
            }
        }

        public async Task<List<Tasks>> GetOverdueTasksAsync()
        {
            return await _context.Tasks
                                 .Where(t => t.EndDate < DateTime.Now && t.Status != TaskStatus.Completed)
                                 .ToListAsync();
        }
        public async Task<List<Tasks>> GetAllSortedByPriorityAsync()
        {
            return await _context.Tasks
                                 .Where(t => !t.IsDeleted) // Only non-deleted tasks
                                 .OrderBy(t => t.Priority) // Sort by priority using the enum
                                 .ToListAsync();
        }
        public async Task<List<Tasks>> GetHighPriorityTasksAsync()
        {
            return await _context.Tasks
                                 .Where(t => t.Priority == TaskPriority.High && !t.IsDeleted) // Filter for high priority
                                 .ToListAsync();
        }
    }
}
