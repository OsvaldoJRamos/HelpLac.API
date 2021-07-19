using HelpLac.Domain.Entities;
using HelpLac.Repository.Interfaces;
using HelpLac.Service.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Service
{
    public class CommentService : ServiceBase<Comment, Guid, ICommentRepository>, ICommentService
    {
        public CommentService(ICommentRepository commentRepository) : base(commentRepository)
        {
        }

        public async Task<Comment> CreateAsync(Guid productId, int userId, string description, Guid? replyCommentId, CancellationToken cancellationToken)
        {
            var comment = new Comment(productId, userId, description, replyCommentId);
            comment.Validate();

            await _repository.AddAsync(comment, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return comment;
        }
    }
}
