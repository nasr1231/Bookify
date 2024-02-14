namespace Bookify.Core.View_Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public DateTime PublishingDate { get; set; }
        public string? ImageUrl { get; set; }
        public string Hall { get; set; } = null!;
        public bool IsAvaliableForRental { get; set; }
        public string Description { get; set; } = null!;
        public int? Pages { get; set; }
        public IEnumerable<string> Categories { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
