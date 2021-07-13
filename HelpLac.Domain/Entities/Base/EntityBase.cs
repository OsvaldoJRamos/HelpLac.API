using System;

namespace HelpLac.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public bool IsActive { get; private set; } = true;
        public abstract void Validate();
    }
}
