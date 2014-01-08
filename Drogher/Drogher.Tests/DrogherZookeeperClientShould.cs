using AutoMoq;
using NUnit.Framework;
using ZooKeeperNet;

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

		[TestCase]
		public void Dispose()
		{
			_mocker.GetMock<IZooKeeper>().Verify(x => x.Dispose());
		}
	}
}