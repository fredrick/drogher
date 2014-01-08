namespace Drogher.Ensemble
{
	public class FixedEnsembleProvider : IEnsembleProvider
	{
		readonly string _connectionString;

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
