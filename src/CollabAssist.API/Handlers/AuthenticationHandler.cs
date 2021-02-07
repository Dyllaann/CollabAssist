using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollabAssist.API.Handlers
{
    public class AuthenticationHandler
    {
        private AuthenticationConfiguration _configuration;

        public AuthenticationHandler(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsAuthenticated(string username, string password)
        {
            return _configuration.Username == username && _configuration.Password == password;
        }

    }
}
