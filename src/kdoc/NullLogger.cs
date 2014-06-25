using Microsoft.Framework.Logging;
using System;

namespace kdoc
{
    // TODO: Temp workaround until the runtime reliably provides logging.
    // If ILoggerFactory is never guaranteed, move this fallback into Microsoft.AspNet.Logging.
    public class NullLoggerFactory : ILoggerFactory
    {
        public ILogger Create(string name)
        {
            return new NullLogger();
        }
    }

    public class NullLogger : ILogger
    {
        public bool WriteCore(TraceType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            return false;
        }
    }
}