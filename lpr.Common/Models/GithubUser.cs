using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class GithubUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }

        //TODO: this model can be extended:
        //https://docs.github.com/en/rest/reference/users
        
    }
}
