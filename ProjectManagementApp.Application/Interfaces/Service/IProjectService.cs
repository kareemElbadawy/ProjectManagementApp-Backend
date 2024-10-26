using ProjectManagementApp.Domain.Entities;

namespace ProjectManagementApp.Application.Interfaces.Service
{
    public interface IProjectService
    {
        Task<Project> GetByIdAsync(int projectId);
        Task<IEnumerable<Project>> GetAllAsync(int pageNumber, int pageSize);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task<bool> DeleteAsync(int projectId);
    }
}
