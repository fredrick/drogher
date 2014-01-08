using System;
using ZooKeeperNet;

namespace Drogher.Utils
{
	public interface IZooKeeperFactory
	{
		IZooKeeper NewZooKeeper(string connectionString, TimeSpan sessionTimeout, IWatcher watcher);
	}
}
