using AutoFixture;
using AutoFixture.AutoMoq;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Models;
using lpr.Data.Contexts;
using lpr.Logic.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace lpr.Tests
{
    public class TestOrganization
    {
        protected OrganisationService _organisationService;
        
        public TestOrganization()
        {
            Fixture fix = new Fixture();
            
            fix.Customize(new AutoMoqCustomization { ConfigureMembers = true });
            
            this._organisationService = fix.Create<OrganisationService>();
        }

        [Theory]
        [InlineData("Project Delta", "dgdgwt-hxfsgeb-636-hdbwd")]
        public void Create_A_Organization(string name, string userId)
        {
            var result = this._organisationService.AddOrganisation(name, userId);
                
            Assert.IsInstanceOf<Organisation>(result);
        }
    }
}