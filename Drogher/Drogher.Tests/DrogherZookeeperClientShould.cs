using AutoMoq;
using NUnit.Framework;

namespace Drogher.Tests
{
    [TestFixture]
    public class DrogherZookeeperClientShould
    {
		private AutoMoqer _mocker;
        private DrogherZookeeperClient _drogherZookeeperClient;

        [SetUp]
        public void Given()
        {
			_mocker = new AutoMoqer();
			_drogherZookeeperClient = _mocker.Resolve<DrogherZookeeperClient>();
        }

		[TestCase]
		public void CreateZookeeperClient()
		{
			Assert.NotNull(_drogherZookeeperClient);
		}
    }
}
