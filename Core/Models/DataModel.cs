namespace Bookify.Core.Models
{
    public class DataModel
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedOn { get; set; }

    }
}
