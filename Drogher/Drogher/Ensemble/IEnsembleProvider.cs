using System;

namespace Drogher.Ensemble
{
    public interface IEnsembleProvider : IDisposable
    {
        string GetConnectionString();
    }
}