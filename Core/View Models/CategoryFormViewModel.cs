namespace Bookify.Core.View_Models
{
    public class CategoryFormViewModel : DataModel
    {
        public int CategoryId { get; set; }
        [MaxLength(60, ErrorMessage = UserErrors.MaxLength), Required, Display(Name = "Category")]
        [Remote("UniqueItems", null, AdditionalFields = "CateogryId", ErrorMessage = UserErrors.Duplicated)]
        public string CategoryName { get; set; } = null!;
    }
}
