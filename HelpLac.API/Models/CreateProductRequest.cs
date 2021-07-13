using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpLac.API.Models
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public bool ContainsLactose { get; set; }
        public IFormFile Image { get; set; }
    }
}
