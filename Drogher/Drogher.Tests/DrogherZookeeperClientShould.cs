using System;
using AutoMoq;
using Drogher.Ensemble;
using Drogher.Utils;
using log4net;
using Moq;
using NUnit.Framework;
using ZooKeeperNet;

namespace Drogher.Tests
{
    [TestFixture]
    public class DrogherZooKeeperClientShould
    {
        private AutoMoqer _mocker;
        private Mock<IZooKeeperFactory> _zooKeeperFactory;
        private Mock<IEnsembleProvider> _ensembleProvider;
        private Mock<IWatcher> _watcher;
        private Mock<IRetryPolicy> _retryPolicy;
        private Mock<ILog> _log;
        private TimeSpan _sessionTimeout;
        private TimeSpan _connectionTimeout;
        private DrogherZooKeeperClient _drogherZooKeeperClient;

        [SetUp]
        public void Given()
        {
            _mocker = new AutoMoqer();
            _zooKeeperFactory = _mocker.GetMock<IZooKeeperFactory>();
            _ensembleProvider = _mocker.GetMock<IEnsembleProvider>();
            _watcher = _mocker.GetMock<IWatcher>();
            _retryPolicy = _mocker.GetMock<IRetryPolicy>();
            _log = _mocker.GetMock<ILog>();
            _sessionTimeout = TimeSpan.FromSeconds(30);
            _connectionTimeout = TimeSpan.FromSeconds(30);
            _drogherZooKeeperClient = new DrogherZooKeeperClient(
                _zooKeeperFactory.Object,
                _ensembleProvider.Object,
                _sessionTimeout,
                _connectionTimeout,
                _watcher.Object,
                _retryPolicy.Object,
                _log.Object);
        }

        [TestCase]
        public void CreateZookeeperClient()
        {
            Assert.NotNull(_drogherZooKeeperClient);
        }

        [TestCase]
        public void Log_WhenSessionTimeoutLessThanConnectionTimeout()
        {
            var sessionTimeout = TimeSpan.FromSeconds(10);
            var connectionTimeout = TimeSpan.FromSeconds(30);
            new DrogherZooKeeperClient(
                _zooKeeperFactory.Object,
                _ensembleProvider.Object,
                sessionTimeout,
                connectionTimeout,
                _watcher.Object,
                _retryPolicy.Object,
                _log.Object);
            _log.Verify(x => x.WarnFormat("session timeout {0} is less than connection timeout {1}", sessionTimeout,
                connectionTimeout));
        }

        [TestCase]
        public void ThrowArgumentNullException_WhenRetryPolicyIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    new DrogherZooKeeperClient(_zooKeeperFactory.Object, _ensembleProvider.Object, _sessionTimeout,
                        _connectionTimeout,
                        _watcher.Object,
                        null,
                        _log.Object));
        }

        [TestCase]
        public void ThrowArgumentNullException_WhenEnsembleProviderIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    new DrogherZooKeeperClient(_zooKeeperFactory.Object, null, _sessionTimeout, _connectionTimeout,
                        _watcher.Object,
                        _retryPolicy.Object,
                        _log.Object));
        }

        [TestCase]
        public void Dispose()
        {
            _mocker.GetMock<IZooKeeper>().Verify(x => x.Dispose());
        }
    }
}