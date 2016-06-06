using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Model;

namespace xwSearchLib.Utility
{
    public class xwSearchHandler : Meta
    {
        public static XWSearchResult search(
            string verb, 
            string noun,
            List<Upgrade> upgrades, 
            List<Pilot> pilots,
            XW_FACTION faction = XW_FACTION.ALL)
        {
            XWSearchResult searchResult = new XWSearchResult();

            // upgrades w/ search 
            List<Upgrade> upgradesWithVerb = upgrades.FindAll(u => u.ability.Contains(verb));
            List<Upgrade> upgradesWithVerbAndNoun = upgradesWithVerb.FindAll(u => u.ability.Contains(noun));

            // pilot w/ search
            List<Pilot> pilotsWithVerb = pilots.FindAll(p => p.pilotAbility.Contains(verb));
            List<Pilot> pilotsWithVerbAndNoun = pilotsWithVerb.FindAll(p => p.pilotAbility.Contains(noun));

            // faction specific
            if (faction != XW_FACTION.ALL) 
            { 

                XW_RESTRICTION noRestriction = XW_RESTRICTION.NONE;
                XW_RESTRICTION limitedRestriction = XW_RESTRICTION.LIMITED;
                XW_RESTRICTION factionRestriction = xwDictionary.getRestrictionByFactionType(faction);
                XW_RESTRICTION factionLimitedRestriction = xwDictionary.getRestrictionLimitedByFactionType(faction);

                if(factionLimitedRestriction != XW_RESTRICTION.NONE)
                {
                    // only pull upgrades that are everything except the restriction
                    upgradesWithVerbAndNoun = upgradesWithVerbAndNoun.FindAll(u => u.restriction == noRestriction ||
                                                                                 u.restriction == limitedRestriction ||
                                                                                 u.restriction == factionRestriction ||
                                                                                 u.restriction == factionLimitedRestriction);
                }
                else
                {
                    // no limitations (except limited)
                    upgradesWithVerbAndNoun = upgradesWithVerbAndNoun.FindAll(u => u.restriction == noRestriction ||
                                                                                 u.restriction == limitedRestriction ||
                                                                                 u.restriction == factionRestriction);
                }

                // filter down pilots by faction
                pilotsWithVerbAndNoun = pilotsWithVerbAndNoun.FindAll(p => p.faction == faction);
            
            }

            searchResult.upgrades = upgradesWithVerbAndNoun;
            searchResult.pilots = pilotsWithVerbAndNoun;

            return searchResult;
        }
    }
}
