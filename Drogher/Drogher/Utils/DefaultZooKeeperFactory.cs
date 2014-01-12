using System;
using ZooKeeperNet;

namespace Drogher.Utils
{
    public class DefaultZooKeeperFactory : IZooKeeperFactory
    {
        public IZooKeeper NewZooKeeper(string connectionString, TimeSpan sessionTimeout, IWatcher watcher)
        {
            return new ZooKeeper(connectionString, sessionTimeout, watcher);
        }
    }
}