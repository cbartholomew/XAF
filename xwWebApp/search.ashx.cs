using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xwSearchLib.Model;
using xwSearchLib.Utility;

namespace xwWebApp
{
    /// <summary>
    /// Summary description for search
    /// </summary>
    public class search : IHttpHandler
    {

        public void ProcessRequest(HttpContext context) 
        {
            var searchMethod = context.Request.QueryString["method"].ToString();
            
            switch (xwDictionary.getSearchType(searchMethod))
            {
                case Meta.XW_SEARCH_TYPE.STANDARD:
                    processStandardSearchRequest(context);
                    break;
                case Meta.XW_SEARCH_TYPE.COST:
                    processByCostSearchRequest(context);
                    break;
                case Meta.XW_SEARCH_TYPE.NAME:
                    processNameSearchRequest(context);
                    break;
                case Meta.XW_SEARCH_TYPE.FREE_TEXT:
                    processFreeTextSearch(context);
                    break;
                default:
                    break;
            }
        }

        private void processStandardSearchRequest(HttpContext ctx)
        {                        
            ctx.Response.ContentType = "application/json";
            var noun = ctx.Request.QueryString["noun"].ToString();
            var verb = ctx.Request.QueryString["verb"].ToString();
            var faction = ctx.Request.QueryString["faction"].ToString();

            List<Upgrade> upgrades = xwJSONSerializer.Deserialize<List<Upgrade>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.UPGRADE_FILE)
                ));

            List<Pilot> pilots = xwJSONSerializer.Deserialize<List<Pilot>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.PILOT_FILE)
                ));

            XWSearchResult results = new XWSearchResult();

            results = xwSearchHandler.search(
                verb, 
                noun, 
                upgrades, 
                pilots, 
                xwDictionary.getFactionByString(faction));

            string resultJSON = xwJSONSerializer.Serialize<XWSearchResult>(results);

            ctx.Response.Write(resultJSON);

            ctx.Response.End();
        }

        private void processByCostSearchRequest(HttpContext ctx) 
        {
            ctx.Response.ContentType = "application/json";

            if (ctx.Request.QueryString["cost"] == null)
            {
                return;         
            }

            var op      = ctx.Request.QueryString["operator"].ToString();
            var cost    = ctx.Request.QueryString["cost"].ToString();

            List<Upgrade> upgrades = xwJSONSerializer.Deserialize<List<Upgrade>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.UPGRADE_FILE)
                ));

            List<Pilot> pilots = xwJSONSerializer.Deserialize<List<Pilot>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.PILOT_FILE)
                ));

            XWSearchResult results = new XWSearchResult();

            results = xwSearchHandler.search(
                Convert.ToInt32(cost),
                xwDictionary.getSearchOperator(op),
                upgrades,
                pilots);

            string resultJSON = xwJSONSerializer.Serialize<XWSearchResult>(results);

            ctx.Response.Write(resultJSON);

            ctx.Response.End();
        }

        private void processNameSearchRequest(HttpContext ctx)
        {
            ctx.Response.ContentType = "application/json";
            var name = ctx.Request.QueryString["name"].ToString();        

            List<Upgrade> upgrades = xwJSONSerializer.Deserialize<List<Upgrade>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.UPGRADE_FILE)
                ));

            List<Pilot> pilots = xwJSONSerializer.Deserialize<List<Pilot>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.PILOT_FILE)
                ));

            XWSearchResult results = new XWSearchResult();

            results = xwSearchHandler.search(
                name,
                upgrades,
                pilots);

            string resultJSON = xwJSONSerializer.Serialize<XWSearchResult>(results);

            ctx.Response.Write(resultJSON);

            ctx.Response.End();        
        }

        private void processFreeTextSearch(HttpContext ctx)
        {
            ctx.Response.ContentType = "application/json";
            var text = ctx.Request.QueryString["freetext"].ToString();

            List<Upgrade> upgrades = xwJSONSerializer.Deserialize<List<Upgrade>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.UPGRADE_FILE)
                ));

            List<Pilot> pilots = xwJSONSerializer.Deserialize<List<Pilot>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.PILOT_FILE)
                ));

            List<string> ships = xwJSONSerializer.Deserialize<List<string>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.XW_SHIP_FILE)
                ));

            List<string> factions = xwJSONSerializer.Deserialize<List<string>>(
                System.IO.File.ReadAllText(
                xwDictionary.getDictionaryPath(Meta.PATH_TYPE.XW_FACTION_FILE)
                ));

            Dictionary<string, List<string>> shipsFound = xwSearchHandler.ShipTypeRequest(text, ships);

            XWSearchResult results = new XWSearchResult();

            results.pilots = pilots;
            results.upgrades = upgrades;

            List<string> searchWordList = text.Split(' ').ToList();

            foreach (string aWord in searchWordList)
            {
               if (shipsFound.ContainsKey(aWord)) {
                   continue; 
               }

               results = xwSearchHandler.search(
               aWord,
               true,
               results.upgrades,
               results.pilots);
            }

            if (shipsFound.Count > 0)
            {
                foreach(string key in shipsFound.Keys)
                {
                    results = xwSearchHandler.filterByRequestedShip(
                        results.pilots, 
                        results.upgrades, 
                        key);
                }
            }

            //foreach (Pilot pilot in results.pilots)
            //{

            //    List<string> phrases = pilot.pilotAbility.Split(' ').ToList();

            //    phrases.RemoveAll(x => x == "");

            //    XWSpeech speech = new XWSpeech(results.pilots, results.upgrades);

            //    speech.splitIntoPhrases();

            //    XWSpeech.word tmpWord = xwSearchHandler.recursiveWordFind(speech.listOfPhrases, 0, phrases);

            //    foreach (KeyValuePair<string, XWSpeech.word> item in tmpWord.nextWord)
            //    {
            //        results.phrases.Add(String.Concat(item.Value.previousWord, " ", item.Key.ToString()));
            //    }                
            //}

            //foreach (Upgrade upgrade in results.upgrades)
            //{
            //    List<string> phrases = upgrade.ability.Split(' ').ToList();

            //    phrases.RemoveAll(x => x == "");

            //    XWSpeech speech = new XWSpeech(results.pilots, results.upgrades);

            //    speech.splitIntoPhrases();

            //    XWSpeech.word tmpWord = xwSearchHandler.recursiveWordFind(speech.listOfPhrases, 0, phrases);

            //    foreach (KeyValuePair<string, XWSpeech.word> item in tmpWord.nextWord)
            //    {                    
            //        results.phrases.Add(String.Concat(item.Value.previousWord, " ", item.Key.ToString()));
            //    }
            //}

                string resultJSON = xwJSONSerializer.Serialize<XWSearchResult>(results);

                ctx.Response.Write(resultJSON);

                ctx.Response.End(); 
            
                
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}