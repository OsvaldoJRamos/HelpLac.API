using HelpLac.Domain.Entities.Base;
using HelpLac.Domain.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace HelpLac.Domain.Entities
{
    public class Comment : EntityBase
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public Guid? ReplyCommentId { get; set; }

        public void Update(Guid productId, int userId, string description, Guid? replyCommentId)
        {
            ProductId = productId;
            UserId = userId;
            Description = description;
            ReplyCommentId = replyCommentId;
        }

        public Comment(Guid productId, int userId, string description, Guid? replyCommentId)
        {
            ProductId = productId;
            UserId = userId;
            Description = description;
            ReplyCommentId = replyCommentId;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Description))
                throw new ValidationEntityException("Description cannot be null.", nameof(Description));
        }
    }
}
