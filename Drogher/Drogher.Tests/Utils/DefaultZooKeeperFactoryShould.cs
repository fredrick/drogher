using System;
using Drogher.Utils;
using Moq;
using NUnit.Framework;
using ZooKeeperNet;

namespace Drogher.Tests.Utils
{
	[TestFixture]
	public class DefaultZooKeeperFactoryShould
	{
		[Test]
		public void NewZooKeeper()
		{
			var zooKeeperFactory = new DefaultZooKeeperFactory();
			Assert.IsNotNull(zooKeeperFactory.NewZooKeeper("127.0.0.1:2181", TimeSpan.FromSeconds(30), new Mock<IWatcher>().Object));
		}
	}
}
