using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Sabre
{
    class SabreSession
    {

        public SabreSession( string username, string password)
        {
            this.sine = username;
            this.password = password;
        }

        private string _token = "";
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }


        private string _sine = "";
        public string sine
        {
            get { return _sine; }
            set { _sine = value; }
        }


        private string _password = "";
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}
