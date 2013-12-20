using NUnit.Framework;

namespace Drogher.Tests
{
    [TestFixture]
    public class DrogherZookeeperClientShould
    {
        private DrogherZookeeperClient _drogherZookeeperClient;

        [SetUp]
        public void Given()
        {
            _drogherZookeeperClient = new DrogherZookeeperClient();
        }
    }
}
