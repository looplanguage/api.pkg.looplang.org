using lpr.Common.Models;

namespace lpr.Common.Dtos.Out
{
    //With this altered DTO for accounts we can filter out certain values we rather not return to the requester. 
    public class ForeignAccountDtoOut
    {
        public int GithubId { get; set; }//TODO: Remove or keep?
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public DateTime Created {  get; set; }

        public ForeignAccountDtoOut(Account account)
        {
            GithubId = account.AccountIdentifiers.GithubId;
            Id = account.Id;
            Name = account.Name ?? string.Empty;
            Logo = account.Logo;
            Created = account.Created;
        }
    }
}
