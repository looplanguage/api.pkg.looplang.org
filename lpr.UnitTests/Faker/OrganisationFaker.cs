using System;
using lpr.Common.Models;

namespace lpr.Tests.Faker
{
public class OrganisationFaker
{
    public static Organisation Faker()
    {
        var Faker = new Bogus.Faker<Organisation>();
        Faker
        .RuleFor(o => o.Id, f => Guid.NewGuid())
        .RuleFor(o => o.Name, f => f.Company.CompanyName())
        .RuleFor(o => o.Documentation, f => f.Commerce.ProductDescription())
        .RuleFor(o => o.Created, f => f.Date.Past())
        .RuleFor(o => o.Logo, f => f.Image.LoremFlickrUrl())
        ;

        return Faker.Generate();
    }
}
}