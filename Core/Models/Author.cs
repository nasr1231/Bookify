namespace Bookify.Core.Models
{
    public class Author : DataModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = null!;
        public string Brief { get; set; } = null!;
        public Nationality? Nationality { get; set; } 
        public int NationalityId{ get; set; } 
    }
}
