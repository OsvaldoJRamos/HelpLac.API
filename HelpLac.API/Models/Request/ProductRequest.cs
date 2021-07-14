namespace HelpLac.API.Models.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public bool? ContainsLactose { get; set; }
    }
}
