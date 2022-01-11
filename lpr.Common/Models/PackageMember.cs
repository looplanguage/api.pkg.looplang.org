namespace lpr.Common.Models
{
    public class PackageMember
    {
        public Guid Id {  get; set; }
        public Package? Package { get; set; }
        public Account? Account { get; set; }
        public DateTime Created {  get; set; }
    }
}
