using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Application.Interfaces.Repository;
using ProjectManagementApp.Domain.Entities;
using ProjectManagementApp.Infrastructure.EF;

namespace ProjectManagementApp.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task<List<Comment>> GetAllByTaskIdAsync(int taskId)
        {
            return await _context.Comments
                                 .Where(c => c.TaskId == taskId)
                                 .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int commentId)
        {
            return await _context.Comments.FindAsync(commentId);
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = await GetByIdAsync(commentId);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
        }
    }
}
