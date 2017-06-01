﻿using Microsoft.Extensions.Logging;

namespace Enjoying.Logging.Abstractions
{
    public class Defaults
    {
        public const string APPLICATION_NAME_KEY = "ApplicationName";
        internal const LogLevel MINIMUM_LOG_LEVEL = LogLevel.Information;
        

        #region Rolling File 

        internal const bool INCLUDE_FILE = true;
        internal const string FILE_NAME = @"Logs\log-{Date}.txt";
        internal const int MAX_NUMBER_OF_FILES_TO_KEEP = 31;
        internal const long ONE_MEGABYTE = 1024 * 1024;

        #endregion

        #region Console

        internal const bool INCLUDE_CONSOLE = false;
        internal const string SERVER_URL = "http://localhost:5341/";

        #endregion

        
    }
}
