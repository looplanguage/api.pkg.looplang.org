using System;
using System.Collections.Generic;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Interfaces.Services;
using lpr.Common.Models;
using lpr.Data;
using lpr.Logic.Services;
using Moq;
using Xunit;

namespace lpr.Tests {
public class OrganizationTest {
    public Mock<OrganisationData> organisationData;
    public IOrganisationService organisationService;

    public OrganizationTest() {
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

    [Theory]
    public async void Get_Multiple_Organizations(int amount, Guid orgId) {
        List<Organisation> list = await
                                  organisationService.GetOrganisationsPaginatedAsync(amount, orgId);

        Assert.Same(list.Count, 15);
    }

    [Theory]
    public void Get_Organization(Guid orgId) {
        var org = organisationService.GetOrganisation(orgId);

        Assert.IsType<Organisation>(org);
    }
}
}
