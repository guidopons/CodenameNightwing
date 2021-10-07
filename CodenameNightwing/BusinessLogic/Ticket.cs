using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodenameNightwing.BusinessLogic
{
    public class Ticket
    {

        public string getTicketNumber()
        {
            if ( this.descripcion != null)
            {
                return this.descripcion.Substring(2,13);
            }
            return null;
        }

        public Ticket(string tktDesc)
        {
            this.descripcion = tktDesc;
            this.fops = new List<FOP>();
            from = null;
        }

        private List<FOP> _fops;
        public List<FOP> fops
        {
            get { return _fops; }
            set { _fops = value; }
        }

        private string _descripcion;
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private string _issueDate;
        public string issueDate
        {
            get { return _issueDate; }
            set { _issueDate = value; }
        }

        private string _agentDesc;
        public string agentDesc
        {
            get { return _agentDesc; }
            set { _agentDesc = value; }
        }

        private string _descExpanded;
        public string descExpanded
        {
            get { return _descExpanded; }
            set { _descExpanded = value; }
        }


        private string _from;
        public string from
        {
            get { return _from; }
            set { _from = value; }
        }


        private string _status;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _tourCode;
        public string tourCode
        {
            get { return _tourCode; }
            set { _tourCode = value; }
        }

        /*
         *                <tickets>
                  <!--Optional:-->
                  <descExpanded>ho</descExpanded>
                  <!--Optional:-->
                  <descripcion>pepe</descripcion>
                  <!--Zero or more repetitions:-->
                  <fops>crop</fops>
                  <!--Optional:-->
                  <from>pepe</from>
                  <!--Optional:-->
                  <tourCode>AT</tourCode>
               </tickets>
*/

        public string toXMLOperacion()
        {
            StringBuilder xmlOutput = new StringBuilder();

            xmlOutput.Append("<tickets>");
            xmlOutput.Append("<descExpanded>" + descExpanded + "</descExpanded>");
            xmlOutput.Append("<descripcion>" + descripcion + "</descripcion>");
            string fopsStr = (fops != null) ? string.Join("|", fops.Select(i => i.ToString()).ToArray()) : "";
            xmlOutput.Append("<fops>" +  fops +"</fops>");
            xmlOutput.Append("<from>" +  from + "</from>");
            xmlOutput.Append("<tourCode>" + tourCode + "</tourCode>");
            xmlOutput.Append("</tickets>");

            return xmlOutput.ToString();

        }
    }
}
