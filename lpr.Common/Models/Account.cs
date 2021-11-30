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
