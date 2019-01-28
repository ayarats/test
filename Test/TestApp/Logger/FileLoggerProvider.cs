using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TestApp.Logger
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _path;
        public FileLoggerProvider(string path)
        {
            _path = path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_path);
        }

        public void Dispose()
        {
        }
    }
}
