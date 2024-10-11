using System.ComponentModel.DataAnnotations;

namespace ottplatform.Models
{
    public class SecondAdmin
    {
        [Key]

        public int id { get; set; }
        public string? photo { get; set; }
        public string? moviename {  get; set; }
        public string? info {  get; set; }
        public string? photo1 { get; set; }
        public string? photo2 { get; set; }
        public string? photo3 { get; set; }
        public string? photo4 { get; set; }

        
    }
}
