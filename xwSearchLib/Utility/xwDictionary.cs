using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Model;
using xwSearchLib.Configuration;
using System.IO;

namespace xwSearchLib.Utility
{
    public class xwDictionary : Meta
    {
        public static List<string> getAdVerbs(List<string> listOfWords)
        {
            xwSearch searchConfig = new xwSearch();

            List<string> adVerbs = new List<string>();

            adVerbs = File.ReadAllLines(searchConfig.DICTIONARY_OUTPUT + searchConfig.ADVERB).ToList();

            adVerbs.Sort();

            adVerbs = listOfWords.FindAll(x => x == adVerbs.Find(y => y == x)).ToList();

            return adVerbs;
        
        }

        public static List<string> getVerbs(List<string> listOfWords)
        {
            xwSearch searchConfig = new xwSearch();

            List<string> verbs = new List<string>();

            verbs = File.ReadAllLines(searchConfig.DICTIONARY_OUTPUT + searchConfig.VERB).ToList();

            verbs.Sort();

            verbs = listOfWords.FindAll(x=> x == verbs.Find(y=>y == x)).ToList();

            return verbs;
        }

        public static List<string> getNouns(List<string> listOfWords)
        {
            xwSearch searchConfig = new xwSearch();

            List<string> nouns = new List<string>();

            nouns = File.ReadAllLines(searchConfig.DICTIONARY_OUTPUT + searchConfig.NOUN).ToList();

            nouns.Sort();

            nouns = listOfWords.FindAll(x => x == nouns.Find(y => y == x)).ToList();

            return nouns;
        }

        public static List<string> getXWVerbs()
        {
            xwSearch searchConfig = new xwSearch();

            List<string> verbs = new List<string>();

            verbs = File.ReadAllLines(getDictionaryPath(PATH_TYPE.XW_VERB)).ToList();

            verbs.RemoveAll(v => v == "");

            return verbs;
        }

        public static List<string> getXWAdVerbs()
        {
            xwSearch searchConfig = new xwSearch();

            List<string> adVerbs = new List<string>();

            adVerbs = File.ReadAllLines(getDictionaryPath(PATH_TYPE.XW_AD_VERB)).ToList();

            adVerbs.RemoveAll(a => a == "");

            return adVerbs;
        }

        public static List<string> getXWNouns()
        {
            xwSearch searchConfig = new xwSearch();

            List<string> nouns = new List<string>();

            nouns = File.ReadAllLines(getDictionaryPath(PATH_TYPE.XW_NOUN)).ToList();

            nouns.RemoveAll(n => n == "");

            return nouns;
        }

        public static void writeDictionaryData(List<string> listOfWords, string filePath)
        {
            xwSearch searchConfig = new xwSearch();

            StringBuilder output = new StringBuilder();

            foreach (string word in listOfWords)
                output.AppendLine(word);

            File.WriteAllText(filePath,output.ToString());       
        }

        public static string getDictionaryPath(PATH_TYPE pathType)
        {
            xwSearch searchConfig = new xwSearch();
            
            string path = (searchConfig.IS_LOCAL) ? searchConfig.LOCAL_OUTPUT : 
                searchConfig.DICTIONARY_OUTPUT;

            switch (pathType)
            {
                case PATH_TYPE.VERB:
                    path += searchConfig.VERB;
                    break;
                case PATH_TYPE.NOUN:
                    path += searchConfig.NOUN;
                    break;
                case PATH_TYPE.AD_VERB:
                    path += searchConfig.ADVERB;
                    break;
                case PATH_TYPE.XW_VERB:
                    path += searchConfig.XW_VERB_FILE_NAME;
                    break;
                case PATH_TYPE.XW_NOUN:
                    path += searchConfig.XW_NOUN_FILE_NAME;
                    break;
                case PATH_TYPE.XW_AD_VERB:
                    path += searchConfig.XW_ADVERB_FILE_NAME;
                    break;
                case PATH_TYPE.PILOT_FILE:
                    path += searchConfig.PILOT_FILE;
                    break;
                case PATH_TYPE.UPGRADE_FILE:
                    path += searchConfig.UPGRADE_FILE;
                    break;
                default:
                    path += "PATH_NOT_FOUND";
                    break;
            }

            return path;

        }

        public static XW_RESTRICTION getRestrictionByFactionType(XW_FACTION faction)
        {
            switch (faction)
            {
                case XW_FACTION.IMPERIAL:
                    return XW_RESTRICTION.IMPERIAL_ONLY;
                case XW_FACTION.REBEL:
                    return XW_RESTRICTION.REBEL_ONLY;
                case XW_FACTION.SCUM:
                    return XW_RESTRICTION.SCUM_ONLY;
                default:
                    return XW_RESTRICTION.NONE;
            }
        }

        public static XW_RESTRICTION getRestrictionLimitedByFactionType(XW_FACTION faction)
        {
            switch (faction)
            {
                case XW_FACTION.SCUM:
                    return XW_RESTRICTION.SCUM_ONLY_LIMITED;
                default:
                    return XW_RESTRICTION.NONE;
            }
        }

        public static EXCEL_SHEET_PILOT getPilotColumnByAction(XW_SHIP_ACTIONS action)
        {
            switch (action)
            {
                case XW_SHIP_ACTIONS.TARGET_LOCK:
                    return EXCEL_SHEET_PILOT.TARGET_LOCK;           
                case XW_SHIP_ACTIONS.BARREL_ROLL:
                    return EXCEL_SHEET_PILOT.BARREL_ROLL;            
                case XW_SHIP_ACTIONS.BOOST:
                    return EXCEL_SHEET_PILOT.BOOST;              
                case XW_SHIP_ACTIONS.CLOAK:
                    return EXCEL_SHEET_PILOT.CLOAK;        
                case XW_SHIP_ACTIONS.EVADE:
                    return EXCEL_SHEET_PILOT.EVADE;
                case XW_SHIP_ACTIONS.FOCUS:
                    return EXCEL_SHEET_PILOT.FOCUS;
                case XW_SHIP_ACTIONS.SLAM:
                    return EXCEL_SHEET_PILOT.SLAM;      
                default:
                    return EXCEL_SHEET_PILOT.ERROR;
            }
        }

        public static XW_FACTION getFactionByString(string input)
        {
            input = input.ToUpper();

            XW_FACTION output = XW_FACTION.ALL;

            switch (input)
            {
                case "IMPERIAL":
                    output = XW_FACTION.IMPERIAL;
                    break;
                case "REBEL":
                    output = XW_FACTION.REBEL;
                    break;
                case "SCUM":
                    output = XW_FACTION.SCUM;
                    break;
                case "ALL":
                    output = XW_FACTION.ALL;
                    break;
                default:
                    output = XW_FACTION.ALL;
                    break;
            }

            return output;
        
        }

        public static EXCEL_SHEET_PILOT getPilotColumnByType(XW_TYPE type)
        {
            switch (type)
            {
                case XW_TYPE.ASTROMECHS:
                    return EXCEL_SHEET_PILOT.ASTROMECH;
                case XW_TYPE.BOMBS:
                    return EXCEL_SHEET_PILOT.BOMB;
                case XW_TYPE.CANNONS:
                    return EXCEL_SHEET_PILOT.CANNON;
                case XW_TYPE.CREW_MEMBERS:
                    return EXCEL_SHEET_PILOT.CREW_MEMBER;
                case XW_TYPE.ELITE_TALENTS:
                    return EXCEL_SHEET_PILOT.ELITE_PILOT_SKILL;
                case XW_TYPE.ILLICIT_UPGRADE:
                    return EXCEL_SHEET_PILOT.ILLICIT_UPGRADE;
                case XW_TYPE.MISSILES:
                    return EXCEL_SHEET_PILOT.MISSILES;
                case XW_TYPE.MODIFICATIONS:
                    return EXCEL_SHEET_PILOT.MODIFICATIONS;
                case XW_TYPE.SALVAGED_ASTROMECH:
                    return EXCEL_SHEET_PILOT.SALVAGED_ATSTROMECH;
                case XW_TYPE.SYSTEM_UPGRADE:
                    return EXCEL_SHEET_PILOT.SYSTEM_UPGRADE;
                case XW_TYPE.TECH:
                    return EXCEL_SHEET_PILOT.TECH;
                case XW_TYPE.TITLES:
                    return EXCEL_SHEET_PILOT.TITLE;
                case XW_TYPE.TORPEDOES:
                    return EXCEL_SHEET_PILOT.TORPEDOES;
                case XW_TYPE.TURRETS:
                    return EXCEL_SHEET_PILOT.TURRET;
                default:
                    return EXCEL_SHEET_PILOT.ERROR;
            }
        }
    }
}
