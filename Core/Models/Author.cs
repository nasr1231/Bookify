namespace Bookify.Core.Models
{
    public class Author
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public string brief { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }
    }
}
