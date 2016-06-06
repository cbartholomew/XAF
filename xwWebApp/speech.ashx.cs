using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using xwSearchLib.Model;
using xwSearchLib.Utility;
using xwSearchLib.Configuration;

namespace xwWebApp
{
    /// <summary>
    /// Summary description for speech
    /// </summary>
    public class speech : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            processLoadContent(context);
        }

        private void processLoadContent(HttpContext ctx) 
        {
            ctx.Response.ContentType = "application/json";

            List<string> verbs      = xwDictionary.getXWVerbs();
            List<string> nouns      = xwDictionary.getXWNouns();
            List<string> adVerbs    = xwDictionary.getXWAdVerbs();

            XWSpeechPart speechPart = new XWSpeechPart(verbs, nouns, adVerbs);

            string resultJSON = xwJSONSerializer.Serialize<XWSpeechPart>(speechPart);

            ctx.Response.Write(resultJSON);

            ctx.Response.End();
        }


        private void processStandardSpeechPhraseRequest(HttpContext ctx)
        {
            //string file_path = @"C:\Users\Christopher\Source\Repos\XWingAbilityFinder\xwSearchLib\Template\X-Wing Pilots, Upgrades and FAQ.xlsx";
            string file_path = "D:\\Hosting\\4173543\\html\\XAF\bin\\Template\\X-Wing Pilots, Upgrades and FAQ.xlsx";

            ctx.Response.ContentType = "application/json";

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

            xwSpeech speech = new xwSpeech(pilots, upgrades);

            speech.splitIntoPhrases();

            string resultJSON = xwJSONSerializer.Serialize<xwSpeech.word>(speech.listOfPhrases);

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