
using CodenameNightwing.IVR.Models;
using System.Collections.Generic;
using System.Linq;

namespace CodenameNightwing.IVR
{
    public class IVRResponses
    {
        
        private List<IVRresponse> responses = null;

        public long timeElapsed { get; set; }

        public IVRResponses(IVRresponse[] array) {

            this.responses = array.OfType<IVRresponse>().ToList();
        }
        public IVRresponse[] GetAllResponses()
        {
            return responses.ToArray<IVRresponse>();      
        }


        // devuelve el ultimo status que se procesó.
        public string getStatus()
        {
            string status = null;
            foreach (var item in this.GetAllResponses())
            {
                status = item.status;
            }
            return status;
        }

        public string getNumeroTarjeta()
        {
            string field = null;
            foreach (var item in this.GetAllResponses())
            {
                if (item.numeroTarjeta != null && !item.numeroTarjeta.Trim().Equals(""))
                    field = item.numeroTarjeta;
            }
            return field;
        }


        public string getFechaExp()
        {
            string field = null;
            foreach (var item in this.GetAllResponses())
            {
                if (item.fechaExp != null && !item.fechaExp.Trim().Equals(""))
                    field = item.fechaExp;
            }
            return field;
        }


        public string getCVC()
        {
            string field = null;
            foreach (var item in this.GetAllResponses())
            {
                if (item.cvc != null && !item.cvc.Trim().Equals(""))
                    field = item.cvc;
            }
            return field;
        }
    }
}