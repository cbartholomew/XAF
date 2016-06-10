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
            string phrase = "assign focus";
            
            XWSearchResult results = new XWSearchResult();
            results.pilots = pilots;
            results.upgrades = upgrades;

            List<string> searchWordList = phrase.Split(' ').ToList();

            foreach (string w in searchWordList)
            {
               results = xwSearchHandler.search(
               w,
               true,
               results.upgrades,
               results.pilots);
            }
           
            foreach (Pilot pilot in results.pilots)
            {

                List<string> phrases = pilot.pilotAbility.Split(' ').ToList();

                phrases.RemoveAll(x => x == "");

                XWSpeech speech = new XWSpeech(results.pilots, results.upgrades);

                speech.splitIntoPhrases();

                XWSpeech.word tmpWord = recursiveWordFind(speech.listOfPhrases, 0, phrases);

                foreach (KeyValuePair<string, XWSpeech.word> item in tmpWord.nextWord)
                {
                    Console.WriteLine(item.Value.previousWord + " ---> " + item.Key.ToString());
                    results.phrases.Add(String.Concat(item.Value.previousWord, " ", item.Key.ToString()));
                }                
            }

            foreach (Upgrade upgrade in results.upgrades)
            {
                List<string> phrases = upgrade.ability.Split(' ').ToList();

                phrases.RemoveAll(x => x == "");

                XWSpeech speech = new XWSpeech(results.pilots, results.upgrades);

                speech.splitIntoPhrases();

                XWSpeech.word tmpWord = recursiveWordFind(speech.listOfPhrases, 0, phrases);

                foreach (KeyValuePair<string, XWSpeech.word> item in tmpWord.nextWord)
                {
                    Console.WriteLine(item.Value.previousWord + " ---> " + item.Key.ToString());
                    results.phrases.Add(String.Concat(item.Value.previousWord, " ", item.Key.ToString()));
                }
            }

        }

        static XWSpeech.word recursiveWordFind(XWSpeech.word phrase, int index, List<string> builtPhrase)
        {
            if (index >= builtPhrase.Count)
            {
                return phrase;
            }

            string nextWordInPhrase = XWSpeech.cleanPunctuation(builtPhrase[index].ToLower());

            if (phrase.nextWord[nextWordInPhrase].hasValue)
                return recursiveWordFind(phrase.nextWord[nextWordInPhrase], index + 1, builtPhrase);
            else
                return phrase;
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
            /*
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
            */
            List<Upgrade> upgrades = xwJSONSerializer.Deserialize<List<Upgrade>>(
            System.IO.File.ReadAllText(
            xwDictionary.getDictionaryPath(Meta.PATH_TYPE.UPGRADE_FILE)
            ));

            List<Pilot> pilots = xwJSONSerializer.Deserialize<List<Pilot>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.PILOT_FILE)
                ));

            //DoSearch(upgrades, pilots);
            DoSpeechSplitByPhrases(upgrades, pilots);
            //DoSpeechSplitByWords(upgrades, pilots);
            //DoXWDictionaryBuild(upgrades, pilots);
            //DoCreateJSONFiles(upgrades, pilots);
            //DoGetXWSpeechParts();
        }
    }
}
