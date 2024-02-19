using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookify.Core.View_Models
{
    public class AuthorFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Author Name")]
        [MaxLength(50, ErrorMessage = UserErrors.MaxLength)]        
        [Required(ErrorMessage = UserErrors.isRequried)]
        [Remote("UniqueAuthors", null, AdditionalFields = "Id", ErrorMessage = UserErrors.Duplicated)]
        public string Name { get; set; } = null!;

        public int NationalityId { get; set; } 
        public IEnumerable<SelectListItem>? Nationalities { get; set; }

        [Required(ErrorMessage =UserErrors.isRequried)]        
        [MaxLength(450)]
        public string Brief { get; set; } = null!;

    }
}
