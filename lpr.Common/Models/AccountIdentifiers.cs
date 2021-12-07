using System.ComponentModel.DataAnnotations;

namespace lpr.Common.Models
{
    public class AccountIdentifiers
    {
        [Key]
        public int GithubId { get; set; }
    }
}