using HelpLac.Domain.Entities;
using HelpLac.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Service.Interfaces
{
    public interface ICommentService : IServiceBase<Comment, Guid, ICommentRepository>
    {
        Task<Comment> CreateAsync(Guid productId, int userId, string description, Guid? replyCommentId, CancellationToken cancellationToken);
    }
}
