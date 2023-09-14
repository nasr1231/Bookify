namespace Bookify.Core.View_Models
{
    public class CreateCategoryViewModel
    {
        [MaxLength(60), Required]

        public string CategoryName { get; set; } = null!;
    }
}
