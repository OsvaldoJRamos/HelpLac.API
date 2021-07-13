using HelpLac.Domain.Entities.Base;
using HelpLac.Domain.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace HelpLac.Domain.Entities
{
    public class Product : EntityBase
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public bool ContainsLactose { get; private set; }
        public string Ingredients { get; private set; }
        public byte[] Image { get; private set; }

        public Product(string name, bool containsLactose, string ingredients, byte[] image)
        {
            Name = name;
            ContainsLactose = containsLactose;
            Ingredients = ingredients;
            Image = image;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ValidationEntityException("Name cannot be null.", nameof(Name));
        }
    }
}
