namespace Bookify.Core.View_Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; } 
        public string brief { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
