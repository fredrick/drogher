using System;
using Drogher.Ensemble;
using Drogher.Utils;
using log4net;
using ZooKeeperNet;

namespace Drogher
{
    public class DrogherZooKeeperClient : IDisposable
    {
        private TimeSpan _connectionTimeout;
        private bool _started;

        public DrogherZooKeeperClient(IZooKeeperFactory zooKeeperFactory, IEnsembleProvider ensembleProvider,
            TimeSpan sessionTimeout, TimeSpan connectionTimeout, IWatcher watcher, IRetryPolicy retryPolicy, ILog log)
        {
            if (sessionTimeout < connectionTimeout)
            {
                log.WarnFormat("session timeout {0} is less than connection timeout {1}", sessionTimeout,
                    connectionTimeout);
            }

            if (retryPolicy == null) throw new ArgumentNullException("retryPolicy");
            if (ensembleProvider == null) throw new ArgumentNullException("ensembleProvider");

            _connectionTimeout = connectionTimeout;
        }

        public void Dispose()
        {
            _started = false;
        }
    }
}