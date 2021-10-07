using CodenameNightwing.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech.AuthService.Request
{
    class AuthRequest
    {

        public AuthRequest()
        {
            client_id = Configuration.getInstance().auth0clientId.Trim();
            client_secret = Configuration.getInstance().auth0SecretKey.Trim();
        }
        public string client_id { get; set; }
        public string client_secret { get; set; }


    }
}
