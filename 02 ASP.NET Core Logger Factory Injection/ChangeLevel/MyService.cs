using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeLevel
{
    public class MyService
    {
        private readonly IConfigurationRoot _configuration;

        public string MyValue => _configuration["MyServiceValue"];

        public MyService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }


        
    }
}
