using ProjectManagementApp.Application.Interfaces.Repository;

namespace ProjectManagementApp.Application.Interfaces.IGenericRepository
{
    namespace ProjectManagementApp.Core.Interfaces
    {
        public interface IUnitOfWork : IDisposable
        {
            IProjectRepository Projects { get; }
            ITaskRepository Tasks { get; }
            ICommentRepository Comments { get; }
            Task<int> CompleteAsync();  // Commits all changes
        }
    }

}
