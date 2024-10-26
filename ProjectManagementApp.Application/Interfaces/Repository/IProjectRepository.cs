using ProjectManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.Interfaces.Repository
{
    public interface IProjectRepository
    {
        Task<Project> GetByIdAsync(int projectId);
        Task<IEnumerable<Project>> GetAllAsync(int pageNumber, int pageSize);
        Task AddAsync(Project project);
        void Update(Project project);
        Task Remove(int projectId);
    }

}
