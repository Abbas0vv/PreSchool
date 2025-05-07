using System.ComponentModel.DataAnnotations.Schema;

namespace PreSchool.Database.Models
{
    public class SlideBanner : BaseModel
    {
        public SlideBanner(string name, string designation, IFormFile file)
        {
            Name = name;
            Designation = designation;
            File = file;
        }
        public SlideBanner() { }

        public string Name { get; set; }
        public string Designation { get; set; }
        public string ?Image { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
