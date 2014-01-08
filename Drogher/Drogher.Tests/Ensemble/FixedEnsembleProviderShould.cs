using System;
using Drogher.Ensemble;
using NUnit.Framework;

namespace Drogher.Tests.Ensemble
{
	[TestFixture]
	public class FixedEnsembleProviderShould
	{
		[Test]
		public void GetConnectionString()
		{
			var connectionString = "127.0.0.1:2181";
			var fixedEnsembleProvider = new FixedEnsembleProvider(connectionString);
			Assert.AreEqual(connectionString, fixedEnsembleProvider.GetConnectionString());
		}
	}
}
