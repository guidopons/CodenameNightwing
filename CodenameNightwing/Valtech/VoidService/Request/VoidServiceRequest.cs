using CodenameNightwing.BusinessLogic;
using CodenameNightwing.Config;
using CodenameNightwing.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodenameNightwing.Valtech.VoidService.Request
{
    public class VoidServiceRequest
    {
        public Pointofsale pointOfSale { get; set; }

        public Channel channel { get; set; }

        public List<string> vcr { get; set; }

        public string pnr { get; set; }

        public VoidServiceRequest() {
            this.pointOfSale = new Pointofsale();
            this.channel = new Channel();
            this.vcr = new List<string>();
        }

    }
}

