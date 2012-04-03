using NUnit.Framework;

namespace JsonConfig.Test
{
    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        public void ToUpper_ShouldDoNothing_BecauseCharIsAlreadyInUpperCase()
        {
            char chr = 'A';

            var result = chr.ToUpper();

            Assert.AreEqual(chr, result);
        }

        [Test]
        public void ToUpper_ShouldReturnUpperCasedChar()
        {
            char chr = 'a';

            var result = chr.ToUpper();

            Assert.AreEqual('A', result);
        }

        [Test]
        public void UpperCaseFirstChar_ShouldDoNothing_BecauseFirstCharIsAlreadyInUpperCase()
        {
            string str = "HostName";

            var result = str.UpperCaseFirstChar();

            Assert.AreEqual(str, result);
        }

        [Test]
        public void UpperCaseFirstChar_ShouldReturnStringWithUpperCasedFirstChar()
        {
            string str = "hostName";

            var result = str.UpperCaseFirstChar();

            Assert.AreEqual("HostName", result);
        }

        [Test]
        public void UpperCaseFirstChar_OnEmptyString_ShouldDoNothing()
        {
            string str = "";

            var result = str.UpperCaseFirstChar();

            Assert.AreEqual(str, result);
        }

        [Test]
        public void UpperCaseFirstChar_OnOneCharString_ShouldNotThrowException()
        {
            string str = "a";

            var result = str.UpperCaseFirstChar();

            Assert.AreEqual("A", result);
        }
    }
}
