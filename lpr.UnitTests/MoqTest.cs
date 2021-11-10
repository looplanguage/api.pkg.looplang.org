using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using Moq;
using Xunit;

namespace lpr.Tests
{
public class MoqTest
{
    protected ILprDbContext _db;
    public Mock<IOrganisationData> organisationData;
    public IOrganisationService organisationService;

    public MoqTest()
    {
        this.organisationData = new Mock<IOrganisationData>();
        this.organisationService = new OrganisationService(_db);
    }
}
}