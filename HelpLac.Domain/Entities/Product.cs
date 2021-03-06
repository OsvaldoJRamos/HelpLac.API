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
        public string ImageUrl { get; private set; }

        public void Update(string name, bool containsLactose, string ingredients, byte[] image, string imageUrl)
        {
            Name = name;
            ContainsLactose = containsLactose;
            Ingredients = ingredients;
            Image = image;
            ImageUrl = imageUrl;
        }

        public Product(string name, bool containsLactose, string ingredients, byte[] image, string imageUrl)
        {
            Name = name;
            ContainsLactose = containsLactose;
            Ingredients = ingredients;
            Image = image;
            ImageUrl = imageUrl;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ValidationEntityException("Name cannot be null.", nameof(Name));
        }
    }
}
