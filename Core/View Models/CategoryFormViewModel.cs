namespace Bookify.Core.View_Models
{
    public class CategoryFormViewModel
    {
        public int CategoryId { get; set; }
        [MaxLength(60, ErrorMessage = UserErrors.MaxLength), Required, Display(Name = "Category")]
        [Remote("AllowItems", null!, AdditionalFields = "CategoryId", ErrorMessage = UserErrors.Duplicated)]
        public string CategoryName { get; set; } = null!;
    }
}
