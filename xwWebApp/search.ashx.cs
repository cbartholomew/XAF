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
            processStandardSearchRequest(context);
        }

        private void processStandardSearchRequest(HttpContext ctx)
        {
            //string file_path = @"C:\Users\Christopher\Source\Repos\XWingAbilityFinder\xwSearchLib\Template\X-Wing Pilots, Upgrades and FAQ.xlsx";
            string file_path = "D:\\Hosting\\4173543\\html\\XAF\bin\\Template\\X-Wing Pilots, Upgrades and FAQ.xlsx";

            ctx.Response.ContentType = "application/json";
            var noun = ctx.Request.QueryString["noun"].ToString();
            var verb = ctx.Request.QueryString["verb"].ToString();
            var faction = ctx.Request.QueryString["faction"].ToString();

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

            XWSearchResult results = new XWSearchResult();

            results = xwSearchHandler.search(
                verb, 
                noun, 
                upgrades, 
                pilots, xwDictionary.getFactionByString(faction));

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