using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace xwSearchLib.Model
{
    [DataContract]
    public class XWSpeechPart
    {
        [DataMember]
        public List<string> Nouns { get; set; }
        [DataMember]
        public List<string> Verbs { get; set; }
        [DataMember]
        public List<string> Faction { get; set; }

        public XWSpeechPart(List<string> verbs, 
            List<string> nouns, 
            List<string> adVerbs = null)
        {
            this.Nouns = new List<string>();
            this.Verbs = new List<string>();
            this.Faction = new List<string>()
            {
                "All",
                "Rebel",
                "Imperial",
                "Scum"
            };

            this.Nouns = nouns;
            this.Verbs = verbs;

            if (adVerbs != null)
            {
                unionVerbs(adVerbs);
            }

            this.Verbs.Sort();
            this.Nouns.Sort();
        }

        private void unionVerbs(List<string> adVerbs) 
        {
            this.Verbs = this.Verbs.Union(adVerbs).ToList();        
        }
    }
}
