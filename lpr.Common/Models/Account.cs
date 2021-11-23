using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class Account
    {
        public int GithubId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public List<Participant> ParticipantAt {get; set;}
        public DateTime Created {  get; set; }
        
    }
}
