using System;
using AutoMoq;
using Drogher.Ensemble;
using Drogher.Utils;
using Moq;
using NUnit.Framework;
using ZooKeeperNet;

namespace Drogher.Tests
{
    [TestFixture]
    public class DrogherZookeeperClientShould
    {
        private AutoMoqer _mocker;
        private Mock<IZooKeeperFactory> _zooKeeperFactory;
        private Mock<IEnsembleProvider> _ensembleProvider;
        private Mock<IWatcher> _watcher;
        private Mock<IRetryPolicy> _retryPolicy;
        private TimeSpan _sessionTimeout;
        private TimeSpan _connectionTimeout;
        private DrogherZookeeperClient _drogherZookeeperClient;

        [SetUp]
        public void Given()
        {
            _mocker = new AutoMoqer();
            _zooKeeperFactory = _mocker.GetMock<IZooKeeperFactory>();
            _ensembleProvider = _mocker.GetMock<IEnsembleProvider>();
            _watcher = _mocker.GetMock<IWatcher>();
            _retryPolicy = _mocker.GetMock<IRetryPolicy>();
            _sessionTimeout = TimeSpan.FromSeconds(30);
            _connectionTimeout = TimeSpan.FromSeconds(30);
            _drogherZookeeperClient = new DrogherZookeeperClient(
                _zooKeeperFactory.Object,
                _ensembleProvider.Object,
                _sessionTimeout,
                _connectionTimeout,
                _watcher.Object,
                _retryPolicy.Object);
        }

        [TestCase]
        public void CreateZookeeperClient()
        {
            Assert.NotNull(_drogherZookeeperClient);
        }

        [TestCase]
        public void ThrowArgumentNullException_WhenRetryPolicyIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    new DrogherZookeeperClient(_zooKeeperFactory.Object, _ensembleProvider.Object, _sessionTimeout,
                        _connectionTimeout,
                        _watcher.Object,
                        null));
        }

        [TestCase]
        public void ThrowArgumentNullException_WhenEnsembleProviderIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    new DrogherZookeeperClient(_zooKeeperFactory.Object, null, _sessionTimeout, _connectionTimeout,
                        _watcher.Object,
                        _retryPolicy.Object));
        }

        [TestCase]
        public void Dispose()
        {
            _mocker.GetMock<IZooKeeper>().Verify(x => x.Dispose());
        }
    }
}