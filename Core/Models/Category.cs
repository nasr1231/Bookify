
namespace Bookify.Core.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(60)]
        [Required]
        public string CategoryName { get; set; } = null!;
        public bool IsDeleted{ get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime LastUpdatedOn { get; set; }
    }
}
