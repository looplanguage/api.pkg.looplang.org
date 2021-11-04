using AutoFixture;
using AutoFixture.AutoMoq;
using lpr.Common.Interfaces.Contexts;
using NUnit.Framework;

namespace lpr.Tests
{
    public class MoqTest
    {
        protected ILprDbContext _db;
        [SetUp]
        public void Setup()
        {
            Fixture fix = new Fixture();
            
            fix.Customize(new AutoMoqCustomization { ConfigureMembers = true });
            
            this._db = fix.Create<ILprDbContext>();
        }

        [Test]
        public void Moq_Created()
        {
            Assert.IsInstanceOf<ILprDbContext>(this._db);
        }
    }
}