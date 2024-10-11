using System.ComponentModel.DataAnnotations;

namespace ottplatform.Models
{
    public class Information2
    {
        [Key]

        public int id { get; set; }
        public string name { get; set; }
        public string email {  get; set; }
        public string message { get; set; }
    }
}
