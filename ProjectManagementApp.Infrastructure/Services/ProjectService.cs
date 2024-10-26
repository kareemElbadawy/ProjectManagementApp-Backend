

using ProjectManagementApp.Application.Interfaces.IGenericRepository.ProjectManagementApp.Core.Interfaces;
using ProjectManagementApp.Application.Interfaces.Service;
using ProjectManagementApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Infrastructure.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Project> GetByIdAsync(int projectId)
        {
            return await _unitOfWork.Projects.GetByIdAsync(projectId);
        }

        public async Task<IEnumerable<Project>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _unitOfWork.Projects.GetAllAsync(pageNumber, pageSize);
        }

        public async Task AddAsync(Project project)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(project.ProjectName))
            {
                throw new ValidationException("Project name cannot be empty.");
            }

            if (project.StartDate >= project.EndDate)
            {
                throw new ValidationException("Start date must be before the end date.");
            }

            if (project.Budget <= 0)
            {
                throw new ValidationException("Budget must be a positive value.");
            }

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            // Validation
            var existingProject = await _unitOfWork.Projects.GetByIdAsync(project.ProjectId);
            if (existingProject == null)
            {
                throw new ValidationException("Project not found.");
            }

            if (string.IsNullOrWhiteSpace(project.ProjectName))
            {
                throw new ValidationException("Project name cannot be empty.");
            }

            if (project.StartDate >= project.EndDate)
            {
                throw new ValidationException("Start date must be before the end date.");
            }

            if (project.Budget <= 0)
            {
                throw new ValidationException("Budget must be a positive value.");
            }

            _unitOfWork.Projects.Update(project);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteAsync(int projectId)
        {
            await _unitOfWork.Projects.Remove(projectId);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
