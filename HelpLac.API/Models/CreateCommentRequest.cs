using System;

namespace HelpLac.API.Models
{
    public class CreateCommentRequest
    {
        public Guid ProductId { get; set; }
        public string Description { get; set; }
        public Guid? ReplyCommentId { get; set; }
    }
}
