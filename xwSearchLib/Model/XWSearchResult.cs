using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwSearchLib.Model
{
    public class XWSearchResult
    {
        public List<Pilot> pilots { get; set; }
        public List<Upgrade> upgrades { get; set; }

        public XWSearchResult()
        {
            this.pilots = new List<Pilot>();
            this.upgrades = new List<Upgrade>();
        }
    }
}
