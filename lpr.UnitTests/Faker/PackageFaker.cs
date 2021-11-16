using System;
using lpr.Common.Models;

namespace lpr.Tests.Faker {
  public class PackageFaker {
    public static Package Faker() {
      var Faker = new Bogus.Faker<Package>();
      Faker.RuleFor(p => p.Id, f => Guid.NewGuid())
          .RuleFor(p => p.Name, f => f.Company.CompanyName())
          .RuleFor(p => p.Documentation, f => f.Commerce.ProductDescription())
          .RuleFor(p => p.Created, f => f.Date.Past())
          .RuleFor(p => p.Archived, false);

      return Faker.Generate();
    }
  }
}
