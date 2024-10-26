using ProjectManagementApp.Domain.Entities;

namespace ProjectManagementApp.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task AddAsync(Tasks task);
        Task<IEnumerable<Tasks>> GetAllAsync(int projectId, int pageNumber, int pageSize);
        Task<Tasks> GetByIdAsync(int taskId);
        Task UpdateAsync(Tasks task);
        Task DeleteAsync(int taskId); // Soft delete
        Task<List<Tasks>> GetOverdueTasksAsync();
        Task<List<Tasks>> GetAllSortedByPriorityAsync();
        Task<List<Tasks>> GetHighPriorityTasksAsync();
    }
}
