namespace Bookify.Core.Models
{
    public class Author : DataModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public string Brief { get; set; }
    }
}
