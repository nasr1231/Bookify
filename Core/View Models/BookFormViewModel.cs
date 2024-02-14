using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Bookify.Core.View_Models
{
    public class BookFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = UserErrors.isRequried)]
        [MaxLength(150 , ErrorMessage = UserErrors.MaxLength), ]
        [Remote("UniqueItem", null!, AdditionalFields = "Id,AuthorId", ErrorMessage = UserErrors.DuplicatedBook)]
        [RequiredIf("IsAvaliableForRental == true", ErrorMessage = "You Must Enter A Title Of The Book To Make It Available")]
        public string? Title { get; set; } = null!;

        [Display(Name = "Author")]
        [Required(ErrorMessage =UserErrors.isRequried)]
        [Remote("UniqueItem", null!, AdditionalFields = "Id,Title", ErrorMessage = UserErrors.Duplicated)]
        public int AuthorId { get; set; }
        public IEnumerable<SelectListItem>? Authors { get; set; }

        [Required(ErrorMessage = UserErrors.isRequried)]
        [MaxLength(80, ErrorMessage = UserErrors.MaxLength)]
        public string Publisher { get; set; } = null!;

        [Required(ErrorMessage = UserErrors.isRequried)]
        [Display(Name = "Publishing Date")]
        [AssertThat("PublishingDate <= Today()", ErrorMessage=UserErrors.NotFutureAllowed)]
        public DateTime PublishingDate { get; set; } = DateTime.Now;

        [Display(Name = "Image")]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = UserErrors.MaxLength)]
        [Required(ErrorMessage = UserErrors.isRequried)]
        public string Hall { get; set; } = null!;
        [Display(Name = "Is Avaliable For Rental?")]
        public bool IsAvaliableForRental { get; set; }
        [Display(Name ="Pages Number")]
        public int? Pages { get; set; }

        [Required(ErrorMessage = UserErrors.isRequried)]
        public string Description { get; set; } = null!;
        
        [Display(Name = "Categories")]
        public IList<int> SelectedCategories { get; set; } = new List<int>();
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
    
}
