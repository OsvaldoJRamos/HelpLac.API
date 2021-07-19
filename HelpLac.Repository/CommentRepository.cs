using HelpLac.Domain.Entities;
using HelpLac.Repository.Interfaces;
using System;

namespace HelpLac.Repository
{
    public class CommentRepository : RepositoryBase<Comment, Guid>, ICommentRepository
    {
        private readonly Context _context;
        public CommentRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
