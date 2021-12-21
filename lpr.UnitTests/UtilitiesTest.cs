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

        [Fact]
        public void GetSemVerFromString_True()
        {
            string semVer = "0.1.0";

            (Common.Models.Version version,bool res ) = Utilities.GetSemVerFromString(semVer);

            Assert.True(res);

            Assert.Equal(0, version.Major);
            Assert.Equal(1, version.Minor);
            Assert.Equal(0, version.Patch);
        }

        [Fact]
        public void GetSemVerFromString_False_TooLong()
        {
            string semVer = "0.1.0.0";

            (Common.Models.Version _, bool res) = Utilities.GetSemVerFromString(semVer);

            Assert.False(res);
        }

        [Fact]
        public void GetSemVerFromString_False_TooShort()
        {
            string semVer = "0.1";

            (Common.Models.Version _, bool res) = Utilities.GetSemVerFromString(semVer);

            Assert.False(res);
        }

        [Fact]
        public void GetSemVerFromString_False_NoNumber()
        {
            string semVer = "0.1.n";

            (Common.Models.Version _, bool res) = Utilities.GetSemVerFromString(semVer);

            Assert.False(res);
        }

        [Fact]
        public void GetSemVerFromString_False_StringEmpty()
        {
            string semVer = string.Empty;

            (Common.Models.Version _, bool res) = Utilities.GetSemVerFromString(semVer);

            Assert.False(res);
        }
    }
}
