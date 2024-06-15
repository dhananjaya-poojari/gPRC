using System.ComponentModel.DataAnnotations;

namespace gRPC.Server.Models
{
    public class CustomerVM
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Range(0, 100)]
        public int Age { get; set; }
    }
}
