using System;
using Drogher.Ensemble;
using Drogher.Utils;
using ZooKeeperNet;

namespace Drogher
{
    public class HandleHolder
    {
        private static readonly object Locker = new object();
        private readonly IZooKeeperFactory _zooKeeperFactory;
        private readonly IWatcher _watcher;
        private readonly IEnsembleProvider _ensembleProvider;
        private readonly TimeSpan _sessionTimeout;

        private static volatile IZooKeeper _zooKeeper;
        private static volatile string _connectionString;

        public HandleHolder(IZooKeeperFactory zooKeeperFactory, IWatcher watcher, IEnsembleProvider ensembleProvider, TimeSpan sessionTimeout)
        {
            _zooKeeperFactory = zooKeeperFactory;
            _watcher = watcher;
            _ensembleProvider = ensembleProvider;
            _sessionTimeout = sessionTimeout;
        }

        public IZooKeeper GetZooKeeper()
        {
            lock (Locker)
            {
                return _zooKeeper;
            }
        }

        public string GetConnectionString()
        {
            lock (Locker)
            {
                return _connectionString;
            }
        }

        public bool HasNewConnectionString()
        {
            lock (Locker)
            {
                return (_connectionString != null) && _ensembleProvider.GetConnectionString() != _connectionString;
            }
        }

        public void CloseAndClear()
        {
            InternalClose();
            lock (Locker)
            {
                _zooKeeper = null;
                _connectionString = null;
            }
        }

        public void CloseAndReset()
        {
            InternalClose();
            lock (Locker)
            {
                if (_zooKeeper != null) return;
                _connectionString = _ensembleProvider.GetConnectionString();
                _zooKeeper = _zooKeeperFactory.NewZooKeeper(_connectionString, _sessionTimeout, _watcher);
            }
        }

        private static void InternalClose()
        {
            if (_zooKeeper == null) return;
            _zooKeeper.Register(new DummyWatcher());
            _zooKeeper.Dispose();
        }

        private class DummyWatcher : IWatcher
        {
            public void Process(WatchedEvent @event)
            {
            }
        }
    }
}