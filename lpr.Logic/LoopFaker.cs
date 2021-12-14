using System;
using Bogus;
using lpr.Common.Models;
using lpr.Logic;
using Version = lpr.Common.Models.Version;

namespace lpr.Logic
{
    public class LoopFaker
    {
        public static Organisation Organisation()
        {
            Faker<Organisation> faker = new Faker<Organisation>();
            
            faker.RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Name, f => Utilities.GenerateRandomValidString(5, 30))
                .RuleFor(o => o.Documentation, f => f.Commerce.ProductDescription())
                .RuleFor(o => o.Created, f => f.Date.Past())
                .RuleFor(o => o.Logo, f => f.Image.LoremFlickrUrl());
            

            return faker.Generate();
        }

        public static Package Package(Organisation? organisation = null)
        {
            Faker<Package> faker = new Faker<Package>();
            
            faker.RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => Utilities.GenerateRandomValidString(5, 30))
                .RuleFor(p => p.Documentation, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Created, f => f.Date.Past())
                .RuleFor(p => p.Archived, false);

            Package package = faker.Generate();

            if (organisation != null)
            {
                package.Organisation = organisation;
                organisation.Packages.Add(package);
            }

            return package;
        }

        public static Version Version(Package? package = null)
        {
            Faker<Version> faker = new Faker<Version>();

            faker.RuleFor(v => v.Created, f => f.Date.Past());
            faker.RuleFor(v => v.Archived, f => f.Random.Bool());
            faker.RuleFor(v => v.Major, f => f.Random.Int());
            faker.RuleFor(v => v.Minor, f => f.Random.Int());
            faker.RuleFor(v => v.Patch, f => f.Random.Int());
            faker.RuleFor(v => v.Documentation, f => f.Lorem.Paragraphs(4));

            Version version = faker.Generate();

            if (package != null)
            {
                version.Package = package;
                package.Versions.Add(version);
            }

            return version;
        }

        public static OrganisationMember OrganisationMember(Account? account = null, Organisation? organisation = null)
        {
            Faker<OrganisationMember> faker = new Faker<OrganisationMember>();

            faker.RuleFor(m => m.Id, Guid.NewGuid());
            faker.RuleFor(m => m.Created, f => f.Date.Past());

            OrganisationMember organisationMember = faker.Generate();
            
            if (account != null)
            {
                organisationMember.Account = account;
            }

            if (organisation != null)
            {
                organisationMember.Organisation = organisation;
            }

            return organisationMember;
        }

        public static Account Account()
        {
            Faker<Account> faker = new Faker<Account>();

            faker.RuleFor(a => a.Id, f => Guid.NewGuid());
            faker.RuleFor(a => a.Created, f => f.Date.Past());
            faker.RuleFor(a => a.Logo, f => f.Image.LoremFlickrUrl());
            faker.RuleFor(a => a.Name, f => f.Person.UserName);

            return faker.Generate();
        }

        public static Account StaticAccount()
        {
            Faker<Account> faker = new Faker<Account>();

            faker.RuleFor(a => a.Id, f => Guid.Parse("183858b9-6aff-4d3b-aeef-3c4dcff295c0"));
            faker.RuleFor(a => a.Created, f => f.Date.Past());
            faker.RuleFor(a => a.Logo, f => f.Image.LoremFlickrUrl());
            faker.RuleFor(a => a.Name, f => f.Person.UserName);

            return faker.Generate();
        }
    }
}