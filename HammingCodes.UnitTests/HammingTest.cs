using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HammingCodes.UnitTests
{
    [TestClass]
    public class HammingTest
    {
        [TestMethod]
        public void Hamming3X1Check()
        {
            Assert.AreEqual(true, Hamming.Create3X1().Check());
        }

        [TestMethod]
        public void Hamming7X4Check()
        {
            Assert.AreEqual(true, Hamming.Create7X4().Check());
        }

        [TestMethod]
        public void Hamming15X11Check()
        {
            Assert.AreEqual(true, Hamming.Create15X11().Check());
        }

        [TestMethod]
        public void Hamming24X16Check()
        {
            Assert.AreEqual(true, Hamming.Create24X16().Check());
        }

        [TestMethod]
        public void Hamming31X26Check()
        {
            Assert.AreEqual(true, Hamming.Create31X26().Check());
        }

        [TestMethod]
        public void Hamming63X57Check()
        {
            Assert.AreEqual(true, Hamming.Create63X57().Check());
        }

        [TestMethod]
        public void HammingCustomCheck()
        {
            Assert.AreEqual(true, Hamming.Create(7, 1).Check());
        }
    }
}
