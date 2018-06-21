using System;

namespace Featurify.Exceptions
{
    public class InvalidFeatureToggleMetadaException : Exception
    {
        public InvalidFeatureToggleMetadaException(string message)
            : base (message)
        {
        }
    }
}
