using System;
using Drogher.Ensemble;
using Drogher.Utils;
using log4net;
using ZooKeeperNet;

namespace Drogher
{
    public class DrogherZookeeperClient : IDisposable
    {
        private readonly ILog _log = LogManager.GetLogger(typeof (DrogherZookeeperClient));
        private bool Started = false;

        public DrogherZookeeperClient(IZooKeeperFactory zooKeeperFactory, IEnsembleProvider ensembleProvider,
            TimeSpan sessionTimeout, TimeSpan connectionTimeout, IWatcher watcher, IRetryPolicy retryPolicy)
        {
            if (sessionTimeout < connectionTimeout)
            {
                _log.WarnFormat("session timeout {0} is less than connection timeout {1}", sessionTimeout,
                    connectionTimeout);
            }

            if (retryPolicy == null) throw new ArgumentNullException("retryPolicy");
            if (ensembleProvider == null) throw new ArgumentNullException("ensembleProvider");
        }

        public void Dispose()
        {
            Started = false;
        }
    }
}