using HelpLac.Domain.Entities;
using HelpLac.Domain.Validation;
using HelpLac.Repository.Interfaces;
using HelpLac.Service.Interfaces;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<Comment> DeleteByIdAsync(Guid id, int userId, CancellationToken cancellationToken)
        {
            var comment = await _repository.GetByIdAsync(id, cancellationToken);

            if (comment == null) throw new ValidationEntityException("Comentário não existe!");
            if (comment.UserId != userId) throw new ValidationEntityException("Somente o dono do comentário pode apagá-lo.");

            await _repository.DeleteByIdAsync(id, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return comment;
        }

        public async Task<List<Comment>> GetAsync(Guid productId, CancellationToken cancellationToken)
        {
            Expression<Func<Comment, bool>> filterExpression = PredicateBuilder.New<Comment>(true);
            filterExpression = filterExpression.And(x => x.ProductId == productId && !x.ReplyCommentId.HasValue);

            var comments = await _repository.SearchAsync(filterExpression, cancellationToken);
            return comments.ToList();
        }
    }
}
