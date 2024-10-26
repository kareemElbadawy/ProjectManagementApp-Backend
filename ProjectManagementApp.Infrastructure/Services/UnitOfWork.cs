

using ProjectManagementApp.Application.Interfaces.IGenericRepository.ProjectManagementApp.Core.Interfaces;
using ProjectManagementApp.Application.Interfaces.Repository;
using ProjectManagementApp.Infrastructure.EF;

namespace ProjectManagementApp.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProjectRepository Projects { get; private set; }
        public ITaskRepository Tasks { get; private set; }
        public ICommentRepository Comments { get; }

        public UnitOfWork(ApplicationDbContext context, IProjectRepository projectRepository, ITaskRepository taskRepository, ICommentRepository comments)
        {
            _context = context;
            Projects = projectRepository;
            Tasks = taskRepository;
            Comments = comments;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

