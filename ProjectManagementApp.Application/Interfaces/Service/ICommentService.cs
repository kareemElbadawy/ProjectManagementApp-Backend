using ProjectManagementApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementApp.Application.Interfaces.Service
{
    public interface ICommentService
    {
        Task AddAsync(Comment comment);
        Task<List<Comment>> GetAllByTaskIdAsync(int taskId);
        Task<Comment> GetByIdAsync(int commentId);
        Task UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(int commentId);
    }
}
