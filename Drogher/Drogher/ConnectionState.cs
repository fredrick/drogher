using System;
using System.Collections.Concurrent;
using Drogher.Ensemble;
using Drogher.Utils;
using ZooKeeperNet;

namespace Drogher
{
    public class ConnectionState : IWatcher, IDisposable
    {
        private static readonly ConcurrentQueue<IWatcher> ParentWatchers = new ConcurrentQueue<IWatcher>();
        private IEnsembleProvider _ensembleProvider;
        private TimeSpan _sessionTimeout;
        private TimeSpan _connectionTimeout;
        private HandleHolder _zooKeeper;

        public ConnectionState(IZooKeeperFactory zooKeeperFactory, IEnsembleProvider ensembleProvider, TimeSpan sessionTimeout, TimeSpan connectionTimeout, IWatcher parentWatcher)
        {
            _ensembleProvider = ensembleProvider;
            _sessionTimeout = sessionTimeout;
            _connectionTimeout = connectionTimeout;
            if (parentWatcher != null)
            {
                ParentWatchers.Enqueue(parentWatcher);
            }

            _zooKeeper = new HandleHolder(zooKeeperFactory, this, ensembleProvider, sessionTimeout);
        }

        public void Process(WatchedEvent @event)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}