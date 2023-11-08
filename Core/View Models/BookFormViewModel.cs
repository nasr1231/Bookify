using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.Core.View_Models
{
    public class BookFormViewModel : DataModel
    {
        public int Id { get; set; }
        [MaxLength(150 , ErrorMessage = UserErrors.MaxLength), ]
        public string Title { get; set; } = null!;

        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public IEnumerable<SelectListItem>? Authors { get; set; }
        [MaxLength(80, ErrorMessage = UserErrors.MaxLength)]
        public string Publisher { get; set; } = null!;
        [Display(Name = "Publishing Date")]
        public DateTime PublishingDate { get; set; } = DateTime.Now;
        [Display(Name = "Image")]
        public IFormFile? ImageUrl { get; set; }
        [MaxLength(50, ErrorMessage = UserErrors.MaxLength)]
        public string Hall { get; set; } = null!;
        [Display(Name = "Is Avaliable For Rental?")]
        public bool IsAvaliableForRental { get; set; }
        public string Description { get; set; } = null!;
        [Display(Name = "Categories")]
        public IList<int> SelectedCategories { get; set; } = new List<int>();
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
