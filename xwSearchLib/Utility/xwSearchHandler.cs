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
            List<Upgrade> upgradesWithVerb = upgrades.FindAll(u => u.ability.IndexOf(verb,
                StringComparison.OrdinalIgnoreCase) >= 0);
            List<Upgrade> upgradesWithVerbAndNoun = upgradesWithVerb.FindAll(u => u.ability.IndexOf(noun, 
                StringComparison.OrdinalIgnoreCase) >= 0);

            // pilot w/ search
            List<Pilot> pilotsWithVerb = pilots.FindAll(p => p.pilotAbility.IndexOf(verb,
                StringComparison.OrdinalIgnoreCase) >= 0);
            List<Pilot> pilotsWithVerbAndNoun = pilotsWithVerb.FindAll(p => p.pilotAbility.IndexOf(noun,
                StringComparison.OrdinalIgnoreCase) >= 0);

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

        public static XWSearchResult search(
            string name,            
            List<Upgrade> upgrades,
            List<Pilot> pilots)
        {

            XWSearchResult searchResult = new XWSearchResult();
            // upgrades w/ search 

            List<Upgrade> upgradesWithName = upgrades.FindAll(u => u.name.IndexOf(name, 
                StringComparison.OrdinalIgnoreCase) >= 0);

            // pilots w/ search 
            List<Pilot> pilotsWithName = pilots.FindAll(p => p.name.IndexOf(name, 
                StringComparison.OrdinalIgnoreCase) >= 0);

            searchResult.upgrades = upgradesWithName;
            searchResult.pilots = pilotsWithName;

            return searchResult;
        }

        public static XWSearchResult search(string freeText, 
            bool usePhrase, 
            List<Upgrade> upgrades, 
            List<Pilot> pilots) 
        {
            XWSearchResult searchResult = new XWSearchResult();
            // upgrades w/ search 

            List<Upgrade> upgradesWithName = upgrades.FindAll(u => u.ability.IndexOf(freeText,
                StringComparison.OrdinalIgnoreCase) >= 0);

            // pilots w/ search 
            List<Pilot> pilotsWithName = pilots.FindAll(p => p.pilotAbility.IndexOf(freeText,
                StringComparison.OrdinalIgnoreCase) >= 0);

            searchResult.upgrades = upgradesWithName;
            searchResult.pilots = pilotsWithName;

            return searchResult;
        }

        public static XWSearchResult search(
        int pointCost,
        XW_SEARCH_OPERATOR op,
        List<Upgrade> upgrades,
        List<Pilot> pilots)
        {            
            XWSearchResult searchResult     = new XWSearchResult();
            List<Upgrade> upgradesWithCost  = new List<Upgrade>();
            List<Pilot> pilotsWithCost      = new List<Pilot>();
            switch (op)
            {
                case XW_SEARCH_OPERATOR.LESS_THAN:                    
                    upgradesWithCost = upgrades.FindAll(u => u.points <= pointCost);
                    pilotsWithCost   = pilots.FindAll(p => p.squadPointCost <= pointCost);
                    break;
                case XW_SEARCH_OPERATOR.MORE_THAN:
                    upgradesWithCost = upgrades.FindAll(u => u.points >= pointCost);
                    pilotsWithCost   = pilots.FindAll(p => p.squadPointCost >= pointCost);
                    break;
                case XW_SEARCH_OPERATOR.EQUAL_TO:
                    upgradesWithCost = upgrades.FindAll(u => u.points == pointCost);
                    pilotsWithCost   = pilots.FindAll(p => p.squadPointCost == pointCost);
                    break;
                default:
                    break;
            }

            searchResult.upgrades = upgradesWithCost;
            searchResult.pilots = pilotsWithCost;

            return searchResult;
        }

        public static XWSpeech.word recursiveWordFind(XWSpeech.word phrase, int index, List<string> builtPhrase)
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
    }
}
