using HelpLac.Domain.Entities;
using HelpLac.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HelpLac.Service.Interfaces
{
    public interface ICommentService : IServiceBase<Comment, Guid, ICommentRepository>
    {
        Task<Comment> CreateAsync(Guid productId, int userId, string description, Guid? replyCommentId, CancellationToken cancellationToken);
        Task<Comment> DeleteByIdAsync(Guid id, int userId, CancellationToken cancellationToken);
        Task<List<Comment>> GetAsync(Guid productId, CancellationToken cancellationToken);
    }
}
