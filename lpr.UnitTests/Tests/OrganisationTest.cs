using System.Threading.Tasks;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Logic.Services;
using lpr.Tests.Faker;
using Moq;
using Xunit;

namespace lpr.Tests {
  public class OrganizationTest {
    public Mock<IOrganisationData> organisationDataMock;

    public OrganizationTest() {
      this.organisationDataMock = new Mock<IOrganisationData>();
    }

    [Fact]
    public void Create_A_Organisation()
    {
      Organisation TestOrganisation = OrganisationFaker.Faker();
      this.organisationDataMock.Setup(d => d.AddOrganisation(TestOrganisation)).Returns(TestOrganisation);
      OrganisationService service = new OrganisationService(this.organisationDataMock.Object);

      var result = service.AddOrganisation(TestOrganisation);
      
      Assert.Equal(result, TestOrganisation);
      Assert.IsType<Organisation>(result);
    }
    
    [Fact]
    public void Get_Organization() {
      Organisation TestOrganisation = OrganisationFaker.Faker();
      this.organisationDataMock.Setup(d => d.GetOrganisationById(TestOrganisation.Id)).Returns(TestOrganisation);
      OrganisationService service = new OrganisationService(this.organisationDataMock.Object);

      var result = service.GetOrganisation(TestOrganisation.Id);
      
      Assert.Equal(result, TestOrganisation);
      Assert.IsType<Organisation>(result);
    }
  }
}
