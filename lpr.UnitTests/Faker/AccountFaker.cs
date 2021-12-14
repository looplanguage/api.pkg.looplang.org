using System;
using lpr.Common.Models;
using lpr.Logic;

namespace lpr.Tests.Faker
{
    public class AccountFaker
    {
        public static Account Faker()
        {
            var Faker = new Bogus.Faker<Account>();
            Faker.RuleFor(p => p.Id, f => Guid.Parse("183858b9-6aff-4d3b-aeef-3c4dcff295c0"))
            .RuleFor(p => p.Name, f => Utilities.GenerateRandomValidString(5, 30))
            .RuleFor(p => p.Created, f => f.Date.Past());

            return Faker.Generate();
        }
    }
}
