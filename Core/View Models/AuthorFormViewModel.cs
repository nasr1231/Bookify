namespace Bookify.Core.View_Models
{
    public class AuthorFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        [Remote("UniqueItems", null!, ErrorMessage = "This Author is added before!!!")]
        public string Name { get; set; } = null!;
        [Required]
        public string Nationality { get; set; } = null!;
        [MaxLength(450)]
        public string brief { get; set; } = null!;

    }
}
