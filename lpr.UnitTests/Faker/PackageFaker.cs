using System;
using lpr.Common.Models;
using lpr.Logic;

namespace lpr.Tests.Faker
{
    public class PackageFaker
    {
        public static Package Faker()
        {
            var Faker = new Bogus.Faker<Package>();
            Faker.RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.Name, f => Utilities.GenerateRandomValidString(5, 30))
            .RuleFor(p => p.Documentation, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Created, f => f.Date.Past())
            .RuleFor(p => p.Archived, false);

            return Faker.Generate();
        }
    }
}
