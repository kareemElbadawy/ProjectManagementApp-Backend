

using ProjectManagementApp.Application.Interfaces.Repository;
using ProjectManagementApp.Application.Interfaces.Service;
using ProjectManagementApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Infrastructure.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task AddAsync(Comment comment)
        {
            // Validate comment data
            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                throw new ValidationException("Comment content cannot be empty.");
            }

            await _commentRepository.AddAsync(comment);
            // You might want to call a _unitOfWork.CompleteAsync() method if you have one
        }

        public async Task<List<Comment>> GetAllByTaskIdAsync(int taskId)
        {
            return await _commentRepository.GetAllByTaskIdAsync(taskId);
        }

        public async Task<Comment> GetByIdAsync(int commentId)
        {
            return await _commentRepository.GetByIdAsync(commentId);
        }

        public async Task UpdateAsync(Comment comment)
        {
            // Validate comment data
            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                throw new ValidationException("Comment content cannot be empty.");
            }

            await _commentRepository.UpdateAsync(comment);
            // You might want to call a _unitOfWork.CompleteAsync() method if you have one
        }

        public async Task<bool> DeleteAsync(int commentId)
        {
            await _commentRepository.DeleteAsync(commentId);
            // You might want to call a _unitOfWork.CompleteAsync() method if you have one
            return true;
        }
    }
}
