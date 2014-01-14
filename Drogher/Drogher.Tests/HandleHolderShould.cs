using System;
using System.Runtime.CompilerServices;
using AutoMoq;
using Drogher.Ensemble;
using Drogher.Utils;
using Moq;
using NUnit.Framework;
using ZooKeeperNet;

namespace Drogher.Tests
{
    [TestFixture]
    public class HandleHolderShould
    {
        private AutoMoqer _mocker;
        private HandleHolder _handleHolder;

        [SetUp]
        public void Given()
        {
            _mocker = new AutoMoqer();
            _handleHolder = new HandleHolder(
                _mocker.GetMock<IZooKeeperFactory>().Object,
                _mocker.GetMock<IWatcher>().Object,
                _mocker.GetMock<IEnsembleProvider>().Object,
                TimeSpan.FromSeconds(30));
            Assert.IsNotNull(_handleHolder);
        }

        [Test]
        public void GetZooKeeperAsNull_WhenNewHandleHolder()
        {
            Assert.IsNull(_handleHolder.GetZooKeeper());
        }

        [Test]
        public void GetConnectionStringAsNull_WhenNewHandleHolder()
        {
            Assert.IsNull(_handleHolder.GetConnectionString());
        }

        [Test]
        public void NotHaveNewConnectionString_WhenNewHandlerHolder()
        {
            Assert.IsFalse(_handleHolder.HasNewConnectionString());
        }

        [Test]
        public void CloseAndClear()
        {
            _handleHolder.CloseAndClear();
            Assert.IsNull(_handleHolder.GetZooKeeper());
            Assert.IsNull(_handleHolder.GetConnectionString());
        }

        [Test]
        public void CloseAndReset()
        {
            const string connectionString = "127.0.0.1:2181";

            var watcher = new StubWatcher();
            _mocker.SetInstance(watcher);
            var sessionTimeout = TimeSpan.FromSeconds(30);
            var handleHolder = new HandleHolder(
                _mocker.GetMock<IZooKeeperFactory>().Object,
                watcher,
                _mocker.GetMock<IEnsembleProvider>().Object,
                sessionTimeout
                );
            var zooKeeper = new Mock<IZooKeeper>().Object;

            _mocker.GetMock<IZooKeeperFactory>()
                .Setup(x => x.NewZooKeeper(connectionString, sessionTimeout, watcher))
                .Returns(zooKeeper);
            _mocker.GetMock<IEnsembleProvider>().Setup(x => x.GetConnectionString()).Returns(connectionString);

            handleHolder.CloseAndReset();

            _mocker.GetMock<IEnsembleProvider>().Verify(x => x.GetConnectionString(), Times.Once());
            _mocker.GetMock<IZooKeeperFactory>()
                .Verify(x => x.NewZooKeeper(connectionString, sessionTimeout, watcher), Times.Once());
            Assert.AreEqual(zooKeeper, handleHolder.GetZooKeeper());
            Assert.AreEqual(connectionString, handleHolder.GetConnectionString());
        }

        [TearDown]
        public void Cleanup()
        {
            _handleHolder.CloseAndClear();
        }
    }
}