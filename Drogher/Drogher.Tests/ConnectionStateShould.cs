using System;
using AutoMoq;
using Drogher.Ensemble;
using Drogher.Utils;
using NUnit.Framework;
using ZooKeeperNet;

namespace Drogher.Tests
{
    [TestFixture]
    public class ConnectionStateShould
    {
        private AutoMoqer _mocker;
        private ConnectionState _connectionState;

        [SetUp]
        public void Given()
        {
            _mocker = new AutoMoqer();
            _connectionState = new ConnectionState(
                _mocker.GetMock<IZooKeeperFactory>().Object,
                _mocker.GetMock<IEnsembleProvider>().Object,
                TimeSpan.FromSeconds(30),
                TimeSpan.FromSeconds(30),
                _mocker.GetMock<IWatcher>().Object);
        }

        [Test]
        public void Process()
        {
            _connectionState.Process(new WatchedEvent(KeeperState.SyncConnected, EventType.NodeCreated, ""));
        }

        [Test]
        public void Dispose()
        {
            _connectionState.Dispose();
        }
    }
}