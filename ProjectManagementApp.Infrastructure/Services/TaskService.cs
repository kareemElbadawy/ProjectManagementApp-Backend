using ProjectManagementApp.Application.Interfaces.IGenericRepository.ProjectManagementApp.Core.Interfaces;
using ProjectManagementApp.Application.Interfaces.Services;
using ProjectManagementApp.Domain.Entities;
using TaskStatus = ProjectManagementApp.Domain.Enums.TaskStatus;

namespace ProjectManagementApp.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Tasks task)
        {
            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Tasks>> GetAllAsync(int projectId, int pageNumber, int pageSize)
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync(projectId, pageNumber, pageSize);
            return tasks;
        }

        public async Task<Tasks> GetByIdAsync(int taskId)
        {
            return await _unitOfWork.Tasks.GetByIdAsync(taskId);
        }

        public async Task UpdateAsync(Tasks task)
        {
            await _unitOfWork.Tasks.UpdateAsync(task);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int taskId)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(taskId);
            if (task != null)
            {
                task.IsDeleted = true; // Soft delete
                await _unitOfWork.Tasks.UpdateAsync(task);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<List<Tasks>> GetOverdueTasksAsync()
        {
            var currentDate = DateTime.UtcNow;
            var overdueTasks = await _unitOfWork.Tasks.GetOverdueTasksAsync();
            return overdueTasks.Where(t => t.EndDate < currentDate && !t.IsDeleted && t.Status != TaskStatus.Completed).ToList();
        }

        public async Task<List<Tasks>> GetAllSortedByPriorityAsync()
        {
            return await _unitOfWork.Tasks.GetAllSortedByPriorityAsync();
        }

        public async Task<List<Tasks>> GetHighPriorityTasksAsync()
        {
            return await _unitOfWork.Tasks.GetHighPriorityTasksAsync();
        }
    }
}
