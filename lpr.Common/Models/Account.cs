using System.ComponentModel.DataAnnotations;

namespace lpr.Common.Models
{
    public class Account
    {
        [Key]
        [Required]
        public Guid Id { get; set; } 
        public AccountIdentifiers? AccountIdentifiers { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Logo { get; set; } = string.Empty;
        public DateTime Created {  get; set; } 
        
        
    }
}
