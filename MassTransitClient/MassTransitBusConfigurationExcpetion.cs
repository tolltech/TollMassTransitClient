using System;

namespace Tolltech.MassTransitClient
{
    public class MassTransitBusConfigurationExcpetion : Exception
    {
        public MassTransitBusConfigurationExcpetion(string msg) : base(msg)
        {
            
        }
    }
}