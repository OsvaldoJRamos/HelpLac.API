using HelpLac.Domain.Entities.Base;
using HelpLac.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpLac.Domain.Entities
{
    public class Product : EntityBase
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }

        public Product(string name)
        {
            Name = name;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ValidationEntityException("Name cannot be null.", nameof(Name));
        }
    }
}
