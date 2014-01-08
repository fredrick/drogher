using System;

namespace Drogher.Ensemble
{
	public class FixedEnsembleProvider : IEnsembleProvider
	{
		private readonly string _connectionString;

		public FixedEnsembleProvider(string connectionString)
		{
			_connectionString = connectionString;
		}

		public string GetConnectionString()
		{
			return _connectionString;
		}

		public void Dispose()
		{
		}
	}
}

