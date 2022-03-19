using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Customer
    {
        public long Id { get; set; }
        
        [Required]
        public string Firstname { get; init; }

        [Required]
        public string Lastname { get; init; }
    }
}