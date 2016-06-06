using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Model;
using xwSearchLib.Utility;

namespace xwConsole
{
    /// <summary>
    /// Testing Grounds
    /// </summary>
    class Program
    {
        public const string file_path = @"C:\Users\Christopher\Source\Repos\XWingAbilityFinder\xwSearchLib\Template\X-Wing Pilots, Upgrades and FAQ.xlsx";
        
        static void DoSearch(List<Upgrade> upgrades, List<Pilot> pilots)
        {

            XWSearchResult results = new XWSearchResult();

            results = xwSearchHandler.search("assign", "focus", upgrades, pilots, Meta.XW_FACTION.SCUM);

        }

        static void DoSpeechSplitByWords(List<Upgrade> upgrades, List<Pilot> pilots)
        {
            xwSpeech speech = new xwSpeech(pilots, upgrades);

            speech.splitIntoWords();

            speech.justWordList.Sort();

            List<string> verbs = xwDictionary.getVerbs(speech.justWordList);
            List<string> nouns = xwDictionary.getNouns(speech.justWordList);
            List<string> adVerbs = xwDictionary.getAdVerbs(speech.justWordList);

        }

        static void DoSpeechSplitByPhrases(List<Upgrade> upgrades, List<Pilot> pilots)
        {
            xwSpeech speech = new xwSpeech(pilots, upgrades);
            
            speech.splitIntoPhrases();
            
        }

        static void Main(string[] args)
        {
            List<string[]> upgradeRows = xwExcel.createFromExcel(file_path, 
                Meta.UPGRADES_SHEET_START, 
                Meta.UPGRADES_SHEET_END,
                "Upgrades");

            List<string[]> pilotRows = xwExcel.createFromExcel(file_path, 
                Meta.PILOT_SHEET_START,
                Meta.PILOT_SHEET_END,
                "Pilot");

            List<Upgrade> upgrades = new List<Upgrade>();
            foreach (string[] row in upgradeRows)
            {
                upgrades.Add(new Upgrade(row));
            }

            List<Pilot> pilots = new List<Pilot>();
            foreach (string[] row in pilotRows)
            {
                pilots.Add(new Pilot(row));
            }


        
            //DoSearch(upgrades, pilots);
            //DoSpeechSplitByPhrases(upgrades, pilots);
            DoSpeechSplitByWords(upgrades, pilots);
            //string upgradeJSON = xwJSONSerializer.Serialize<List<Upgrade>>(upgrades);
            //string pilotJSON = xwJSONSerializer.Serialize<List<Pilot>>(pilots);
        }
    }
}
