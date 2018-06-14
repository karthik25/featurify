using System;

namespace Featurify.Exceptions
{
    public class FeatureNotEnabledException : Exception
    {
        public FeatureNotEnabledException(string message)
            : base(message)
        {
        }
    }
}
