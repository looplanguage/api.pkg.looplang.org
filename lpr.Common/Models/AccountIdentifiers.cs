using System.ComponentModel.DataAnnotations;

namespace lpr.Common.Models
{
    public class AccountIdentifiers
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public int GithubId { get; set; }
    }
}