using lpr.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace lpr.Tests
{
    public class UtilitiesTest
    {

        [Fact]
        public void ValidateName_False_Empty()
        {
            string s = "";

            bool res = Utilities.ValidateName(s);

            Assert.False(res);
        }


        [Fact]
        public void ValidateName_True()
        {
            string s = "TestName";

            bool res = Utilities.ValidateName(s);

            Assert.True(res);
        }

        [Fact]
        public void ValidateName_False_Hypen_First()
        {
            string s = "-TestName";

            bool res = Utilities.ValidateName(s);

            Assert.False(res);
        }


        [Fact]
        public void ValidateName_False_SpecialCharacter()
        {
            string s = "TestName#";

            bool res = Utilities.ValidateName(s);

            Assert.False(res);
        }


        [Fact]
        public void ValidateName_True_hypen()
        {
            string s = "Test-Name";

            bool res = Utilities.ValidateName(s);

            Assert.True(res);
        }
    }
}
