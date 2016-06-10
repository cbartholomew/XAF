using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace xwSearchLib.Model
{
    [DataContract]
    public class XWSearchResult
    {
        [DataMember]
        public List<Pilot> pilots { get; set; }
        [DataMember]
        public List<Upgrade> upgrades { get; set; }
        [DataMember]
        public List<string> phrases { get; set; }

        public XWSearchResult()
        {
            this.pilots = new List<Pilot>();
            this.upgrades = new List<Upgrade>();
            this.phrases = new List<string>();
        }
    }
}
