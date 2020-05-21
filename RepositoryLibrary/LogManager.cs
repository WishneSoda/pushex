using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary
{
    public class LogManager
    {
        private ILogger _logger;
        public LogManager(ILogger logger)
        {
            _logger = logger;
        } 
        public void LogMe()
        {
            _logger.LogMe();
        }
    }
}
