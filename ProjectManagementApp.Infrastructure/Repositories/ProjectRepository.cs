using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Application.Interfaces.Repository;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Infrastructure.EF;
namespace ProjectManagementApp.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetByIdAsync(int projectId)
        {
            return await _context.Projects.FindAsync(projectId);
        }

        public async Task<IEnumerable<Project>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Projects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);
        }

        public async Task Remove(int projectId)
        {
            var project = await GetByIdAsync(projectId);
            if (project != null)
            {
                // Soft delete: Mark as deleted instead of removing
                project.IsDeleted = true;
                _context.Projects.Update(project);
            }
        }
    }
}

