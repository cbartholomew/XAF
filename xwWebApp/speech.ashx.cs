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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}