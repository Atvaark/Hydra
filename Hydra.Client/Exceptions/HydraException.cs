using System;

namespace Hydra.Client.Exceptions
{
    [Serializable]
    public class HydraException : ApplicationException
    {
        public HydraException(string message) : base(message)
        {
        }
    }
}