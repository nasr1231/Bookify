namespace Bookify.Core.View_Models
{
    public class CategoryFormViewModel
    {
        public int CategoryId { get; set; }
        [MaxLength(60), Required]
        [Remote("UniqueItems", null, ErrorMessage = "This Category is added before!!!")]
        public string CategoryName { get; set; } = null!;
    }
}
