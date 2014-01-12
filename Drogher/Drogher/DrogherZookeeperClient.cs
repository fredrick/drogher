using System;
using Drogher.Ensemble;
using Drogher.Utils;
using log4net;
using ZooKeeperNet;

namespace Drogher
{
    public class DrogherZooKeeperClient : IDisposable
    {
        private readonly ILog _log = LogManager.GetLogger(typeof (DrogherZooKeeperClient));
        private bool _started = false;
        private TimeSpan _connectionTimeout;

        public DrogherZooKeeperClient(IZooKeeperFactory zooKeeperFactory, IEnsembleProvider ensembleProvider,
            TimeSpan sessionTimeout, TimeSpan connectionTimeout, IWatcher watcher, IRetryPolicy retryPolicy)
        {
            if (sessionTimeout < connectionTimeout)
            {
                _log.WarnFormat("session timeout {0} is less than connection timeout {1}", sessionTimeout,
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