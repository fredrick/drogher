using ZooKeeperNet;

namespace Drogher.Tests
{
    public class StubWatcher : IWatcher
    {
        public void Process(WatchedEvent @event)
        {
        }
    }
}