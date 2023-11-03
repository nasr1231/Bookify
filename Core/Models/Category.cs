namespace Bookify.Core.Models
{
    [Index(nameof(CategoryName), IsUnique = true)]
    public class Category : DataModel
    {
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(60)]
        [Required]
        public string CategoryName { get; set; } = null!;
        public ICollection<BookCategory> Books { get; set; } = new List<BookCategory>();    
    }
}
