using System.ComponentModel.DataAnnotations.Schema;

namespace PreSchool.Database.Models
{
    public class SlideBanner : BaseModel
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
