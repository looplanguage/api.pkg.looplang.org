using System.Collections.Generic;
using System.Threading.Tasks;
using lpr.Common.Interfaces.Data;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Logic;
using lpr.Logic.Services;
using Moq;
using Xunit;

namespace lpr.Tests {
    public class OrganizationTest {
        public Mock<IOrganisationData> organisationDataMock;

        public OrganizationTest() {
          this.organisationDataMock = new Mock<IOrganisationData>();
        }

        [Fact]
        public void AddOrganisation_True() {
            Organisation TestOrganisation = LoopFaker.Organisation();
            this.organisationDataMock.Setup(d => d.AddOrganisation(TestOrganisation))
                .Returns(TestOrganisation);
            OrganisationService service =
                new OrganisationService(this.organisationDataMock.Object);

            var result = service.AddOrganisation(TestOrganisation);

            Assert.Equal(result, TestOrganisation);
        }

        [Fact]
        public void AddOrganisation_Throws_ApiException_Name_Empty()
        {
            Organisation TestOrganisation = LoopFaker.Organisation();
            TestOrganisation.Name = "";
            this.organisationDataMock.Setup(d => d.AddOrganisation(TestOrganisation))
                .Returns(TestOrganisation);
            OrganisationService service =
                new OrganisationService(this.organisationDataMock.Object);

            Assert.Throws<ApiException>(() => service.AddOrganisation(TestOrganisation));
        }

        [Fact]
        public void AddOrganisation_Throws_ApiException_Name_Too_Short()
        {
            Organisation TestOrganisation = LoopFaker.Organisation();
            TestOrganisation.Name = "test";

            this.organisationDataMock.Setup(d => d.AddOrganisation(TestOrganisation))
                .Returns(TestOrganisation);
            OrganisationService service =
                new OrganisationService(this.organisationDataMock.Object);

            Assert.Throws<ApiException>(() => service.AddOrganisation(TestOrganisation));
        }


        [Fact]
        public void GetOrganisation_True() {
            Organisation TestOrganisation = LoopFaker.Organisation();
            this.organisationDataMock
                .Setup(d => d.GetOrganisationById(TestOrganisation.Id))
                .Returns(TestOrganisation);
            OrganisationService service =
                new OrganisationService(this.organisationDataMock.Object);

            var result = service.GetOrganisation(TestOrganisation.Id);

            Assert.Equal(result, TestOrganisation);
        }

        [Fact]
        public void GetOrganisation_False()
        {
            Organisation TestOrganisation = LoopFaker.Organisation();
            this.organisationDataMock
                .Setup(d => d.GetOrganisationById(TestOrganisation.Id))
                .Returns(TestOrganisation);
            OrganisationService service =
                new OrganisationService(this.organisationDataMock.Object);

            var result = service.GetOrganisation(System.Guid.Empty);

            Assert.Null(result);
        }

        [Fact]
        public async void GetOrganisationsFromAccountAsync_True()
        {
            Account Account = LoopFaker.Account();
            Organisation Organisation = LoopFaker.Organisation();
            Organisation.Name = "TestOrganisation";
            List<Organisation> TestOrganisations = new List<Organisation>(){Organisation};
            this.organisationDataMock.Setup(d => d.GetOrganisationsFromAccountAsync(Account.Id))
                .Returns(Task.FromResult(TestOrganisations));

            IOrganisationService organisationService =
                new OrganisationService(this.organisationDataMock.Object);
            List<Organisation> org = await organisationService.GetOrganisationsFromAccountAsync(Account.Id);

            Assert.Equal(org[0].Name, "TestOrganisation");
        }
    }
}
