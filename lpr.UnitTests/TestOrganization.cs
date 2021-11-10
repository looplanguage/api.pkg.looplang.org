using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data;
using lpr.Logic.Services;
using Moq;
using Xunit;

namespace lpr.Tests {
  public class TestOrganization {
    protected ILprDbContext _db;
    public Mock<OrganisationData> organisationData;
    public IOrganisationService organisationService;

    public TestOrganization() {
      this.organisationData =
          new Mock<OrganisationData>(DatabaseMoq.GetDatabaseContext());
      this.organisationService =
          new OrganisationService(this.organisationData.Object);
    }

    [Theory]
    [InlineData("Project Delta", "dgdgwt-hxfsgeb-636-hdbwd")]
    public void Create_A_Organization(string name, string userId) {
      var organisation = organisationService.AddOrganisation(name, userId);
      Assert.IsType<Organisation>(organisation);
    }
  }
}
