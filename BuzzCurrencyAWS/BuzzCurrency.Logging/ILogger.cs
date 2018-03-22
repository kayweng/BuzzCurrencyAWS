using System;
using System.Collections.Generic;
using System.Text;

namespace BuzzCurrency.Logging
{
    interface ILogger
    {
        void LogInfo(string message);

        void LogException(Exception ex);
    }
}
