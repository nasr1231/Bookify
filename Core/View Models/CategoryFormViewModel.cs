namespace Bookify.Core.View_Models
{
    public class CategoryFormViewModel
    {
        public int CategoryId { get; set; }
        [MaxLength(60), Required]
        public string CategoryName { get; set; } = null!;
    }
}
