using ProjectManagementApp.Domain.Entities;
using System;

namespace ProjectManagementApp.Application.Interfaces.Repository
{
    public interface ITaskRepository
    {
        Task AddAsync(Tasks task);
        Task<List<Tasks>> GetAllByProjectIdAsync(int projectId);
        Task<IEnumerable<Tasks>> GetAllAsync(int projectId, int pageNumber, int pageSize);
        Task<Tasks> GetByIdAsync(int taskId);
        Task UpdateAsync(Tasks task);
        Task DeleteAsync(int taskId);
        Task<List<Tasks>> GetOverdueTasksAsync();
        Task<List<Tasks>> GetAllSortedByPriorityAsync();
        Task<List<Tasks>> GetHighPriorityTasksAsync();
    }

}
