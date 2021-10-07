using CodenameNightwing.Config;
using CodenameNightwing.Varios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.BusinessLogic
{
    public class Terminal
    {

        public Terminal()
        {

            macAddress = NetworkUtils.GetMacAddress();
            machineName = System.Environment.MachineName;
            pinpadId = (Configuration.getInstance().idPinpad != null && !Configuration.getInstance().idPinpad.Equals("")) ? Configuration.getInstance().idPinpad : null;
        }
        private string _pinpadId;
        public string pinpadId
        {
            get { return _pinpadId; }
            set { _pinpadId = value; }
        }


        private string _printerId;
        public string printerId
        {
            get { return _printerId; }
            set { _printerId = value; }
        }

        private string _sucursal;
        public string sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; }
        }

        private string _caja;
        public string caja
        {
            get { return _caja; }
            set { _caja = value; }
        }

        private string _baseArsa;
        public string baseArsa
        {
            get { return _baseArsa; }
            set { _baseArsa = value; }
        }


        private string _macAddress;
        public string macAddress
        {
            get { return _macAddress; }
            set { _macAddress = value; }
        }

        private string _machineName;
        public string machineName
        {
            get { return _machineName; }
            set { _machineName = value; }
        }

        public bool isTerminalValid()
        {

            if ( baseArsa == null || baseArsa.Length != 3)
            {
                return false;
            }

            if (caja == null || caja.Length < 1)
            {
                return false;
            }

            if (sucursal == null || sucursal.Length < 1)
            {
                return false;
            }

            if ( printerId == null )
            {
                return false;
            }

            return true;
        }

    }
}
