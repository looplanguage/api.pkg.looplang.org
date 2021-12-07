namespace lpr.Common.Models
{
    public class Account
    {
        public AccountIdentifiers AccountIdentifiers { get; set; } = new AccountIdentifiers();
        public Guid Id { get; set; } 
        public string? Name { get; set; } = string.Empty;
        public string? Logo { get; set; } = string.Empty;
        public DateTime Created {  get; set; } 
        
        
    }
}
