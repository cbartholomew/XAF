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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}