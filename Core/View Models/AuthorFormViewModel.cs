namespace Bookify.Core.View_Models
{
    public class AuthorFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = UserErrors.MaxLength), Required, Display(Name = "Author")]
        [Remote("UniqueAuthors", null, AdditionalFields = "Id", ErrorMessage = UserErrors.Duplicated)]
        public string Name { get; set; } = null!;
        [Required]
        public string Nationality { get; set; } = null!;
        [MaxLength(450)]
        public string Brief { get; set; } = null!;

    }
}
