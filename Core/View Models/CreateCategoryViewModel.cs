namespace Bookify.Core.View_Models
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; } = null!;
    }
}
