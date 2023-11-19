using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Bookify.Core.View_Models
{
    public class BookFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(150 , ErrorMessage = UserErrors.MaxLength), ]
        [Remote("UniqueItem", null!, AdditionalFields = "Id,AuthorId", ErrorMessage = UserErrors.DuplicatedBook)]
        [RequiredIf("IsAvaliableForRental == true")]
        public string? Title { get; set; } = null!;

        [Display(Name = "Author")]
        [Remote("UniqueItem", null!, AdditionalFields = "Id,Title", ErrorMessage = UserErrors.Duplicated)]
        public int AuthorId { get; set; }
        public IEnumerable<SelectListItem>? Authors { get; set; }
        [MaxLength(80, ErrorMessage = UserErrors.MaxLength)]
        public string Publisher { get; set; } = null!;
        [Display(Name = "Publishing Date")]
        [AssertThat("PublishingDate <= Today()", ErrorMessage=UserErrors.NotFutureAllowed)]
        public DateTime PublishingDate { get; set; } = DateTime.Now;
        [Display(Name = "Image")]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

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
