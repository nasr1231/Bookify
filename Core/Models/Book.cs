namespace Bookify.Core.Models
{
    [Index(nameof(Title), nameof(AuthorId), IsUnique =true)]
    public class Book : DataModel
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        [MaxLength(80)]
        public string Publisher { get; set; } = null!;
        public DateTime PublishingDate { get; set; }
        public string? ImageUrl { get; set; }
        public string Hall { get; set; } = null!;
        public bool IsAvaliableForRental { get; set; }   
        public string Description { get; set; } = null!;
        public int? Pages{ get; set; }
        public ICollection<BookCategory> Categories { get; set; } = new List<BookCategory>();
    }
}
