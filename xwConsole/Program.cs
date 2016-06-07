using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Model;
using xwSearchLib.Utility;
using xwSearchLib.Configuration;

namespace xwConsole
{
    /// <summary>
    /// This program is for building files and testing the library. This is not the application.
    /// </summary>
    class Program
    {
        public const string file_path = @"C:\Users\Christopher\Source\Repos\XWingAbilityFinder\xwSearchLib\Template\X-Wing Pilots, Upgrades and FAQ.xlsx";
        
        static void DoSearch(List<Upgrade> upgrades, List<Pilot> pilots)
        {

            XWSearchResult results = new XWSearchResult();

            results = xwSearchHandler.search("assign", "focus", upgrades, pilots, Meta.XW_FACTION.SCUM);

        }

        static void DoXWDictionaryBuild(List<Upgrade> upgrades, List<Pilot> pilots)
        {
            XWSpeech speech = new XWSpeech(pilots, upgrades);

            speech.splitIntoWords();

            speech.justWordList.Sort();

            List<string> verbs =        xwDictionary.getVerbs(speech.justWordList);
            List<string> nouns =        xwDictionary.getNouns(speech.justWordList);
            List<string> adVerbs =      xwDictionary.getAdVerbs(speech.justWordList);

            string outputVerbFileName = xwDictionary.getDictionaryPath(Meta.PATH_TYPE.XW_VERB);
            string outputAdVerbFileName = xwDictionary.getDictionaryPath(Meta.PATH_TYPE.XW_AD_VERB);
            string outputNounFileName = xwDictionary.getDictionaryPath(Meta.PATH_TYPE.XW_NOUN);

            xwDictionary.writeDictionaryData(verbs, outputVerbFileName);
            xwDictionary.writeDictionaryData(nouns, outputNounFileName);
            xwDictionary.writeDictionaryData(adVerbs, outputAdVerbFileName);
        }

        static void DoGetXWSpeechParts()
        {
            List<string> verbs = xwDictionary.getXWVerbs();
            List<string> nouns =  xwDictionary.getXWNouns();
            List<string> adVerbs = xwDictionary.getXWAdVerbs();

            XWSpeechPart speechPart = new XWSpeechPart(verbs, nouns, adVerbs);

        }

        static void DoSpeechSplitByWords(List<Upgrade> upgrades, List<Pilot> pilots)
        {
            XWSpeech speech = new XWSpeech(pilots, upgrades);
           
            speech.splitIntoWords();

            speech.justWordList.Sort();

            List<string> verbs = xwDictionary.getVerbs(speech.justWordList);
            List<string> nouns = xwDictionary.getNouns(speech.justWordList);
            List<string> adVerbs = xwDictionary.getAdVerbs(speech.justWordList);
        }

        static void DoSpeechSplitByPhrases(List<Upgrade> upgrades, List<Pilot> pilots)
        {
            XWSpeech speech = new XWSpeech(pilots, upgrades);
            
            speech.splitIntoPhrases();
            
        }

        static void DoCreateJSONFiles(List<Upgrade> upgrades, List<Pilot> pilots)
        {
            string upgradeJSON = xwJSONSerializer.Serialize<List<Upgrade>>(upgrades);
            string pilotJSON = xwJSONSerializer.Serialize<List<Pilot>>(pilots);

            System.IO.File.WriteAllText(@"C:\Users\Christopher\Source\Repos\XWingAbilityFinder\xwSearchLib\Dictionary\upgrades.in", upgradeJSON);
            System.IO.File.WriteAllText(@"C:\Users\Christopher\Source\Repos\XWingAbilityFinder\xwSearchLib\Dictionary\pilots.in", pilotJSON);        
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
            //DoSpeechSplitByWords(upgrades, pilots);
            //DoXWDictionaryBuild(upgrades, pilots);
            //DoCreateJSONFiles(upgrades, pilots);
            //DoGetXWSpeechParts();
        }
    }
}
