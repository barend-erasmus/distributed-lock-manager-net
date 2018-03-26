using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace DistributedLockManagerNet.Tests
{
    [TestClass]
    public class DistributedLockManagerClientTest
    {
        [TestMethod]
        public void Acquire_Should_Return_True()
        {
            var id = Guid.NewGuid();

            var distributedLockManagerClient = new DistributedLockManagerClient("127.0.0.1", 5001);

            var result = distributedLockManagerClient.Acquire($"mylock-{id}");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Acquire_Should_Return_False_Given_Already_Acquired()
        {
            var id = Guid.NewGuid();

            var distributedLockManagerClient = new DistributedLockManagerClient("127.0.0.1", 5001);

            distributedLockManagerClient.Acquire($"mylock-{id}");

            var result = distributedLockManagerClient.Acquire($"mylock-{id}");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Acquire_Should_Return_True_Given_Released()
        {
            var id = Guid.NewGuid();

            var distributedLockManagerClient = new DistributedLockManagerClient("127.0.0.1", 5001);

            distributedLockManagerClient.Acquire($"mylock-{id}");
            distributedLockManagerClient.Release($"mylock-{id}");

            var result = distributedLockManagerClient.Acquire($"mylock-{id}");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WaitAcquire_Should_Return_True()
        {
            var id = Guid.NewGuid();

            var distributedLockManagerClient = new DistributedLockManagerClient("127.0.0.1", 5001);

            distributedLockManagerClient.Acquire($"mylock-{id}");

            Thread.Sleep(3500);

            var result = distributedLockManagerClient.WaitAcquire($"mylock-{id}");

            Assert.IsTrue(result);
        }
    }
}
