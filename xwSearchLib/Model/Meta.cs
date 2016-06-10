using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwSearchLib.Model
{
    public class Meta
    {
        public const string UPGRADES_SHEET_START = "A";
        public const string UPGRADES_SHEET_END = "H";

        public const string PILOT_SHEET_START = "A";
        public const string PILOT_SHEET_END = "AI";


        public enum XW_SEARCH_TYPE 
        { 
            STANDARD,
            COST,
            NAME,
            FREE_TEXT
        }

        public enum XW_SEARCH_OPERATOR
        { 
            LESS_THAN,
            MORE_THAN,
            EQUAL_TO        
        }

        public enum PATH_TYPE
        {
            VERB,
            NOUN,
            AD_VERB,
            XW_VERB,
            XW_NOUN,
            XW_AD_VERB,     
            PILOT_FILE,
            UPGRADE_FILE,
            ERROR
        }

        public enum XW_TYPE
        {
            ASTROMECHS,
            BOMBS,
            CANNONS,
            CREW_MEMBERS,
            ELITE_TALENTS,
            ILLICIT_UPGRADE,
            MISSILES,
            MODIFICATIONS,
            SALVAGED_ASTROMECH,
            SYSTEM_UPGRADE,
            TECH,
            TITLES,
            TORPEDOES,
            TURRETS,
            ERROR,
            NONE
        }

        public enum XW_RESTRICTION
        {
            REBEL_ONLY,
            SCUM_ONLY,
            IMPERIAL_ONLY,
            LIMITED,
            SCUM_ONLY_LIMITED,
            NONE
        }

        public enum XW_FACTION
        {
            IMPERIAL,
            REBEL,
            SCUM,
            ALL,
            ERROR
        }

        public enum XW_SHIP_SIZE
        {
            SMALL,
            LARGE,
            EPIC,
            ERROR
        }

        public enum XW_SHIP_ACTIONS
        {
            TARGET_LOCK,
            BARREL_ROLL,
            BOOST,
            CLOAK,
            EVADE,
            FOCUS,
            SLAM,
            NONE
        }

        public enum EXCEL_SHEET_UPGRADE
        {
            TYPE            = 0,
            NAME            = 1,   
            POINTS          = 2,
            UNIQUE          = 3,
            RESTRICTION     = 4,
            ABILITY         = 5,
            AVAILABILITY    = 6
        }

        public enum EXCEL_SHEET_PILOT
        {
            PILOT_NAME = 0,
            UNIQUE  = 1,
            FACTION = 2,
            SHIP_TYPE = 3,
            SHIP_SIZE = 4,
            NONSTANDARD_WEAPON = 5,
            PILOT_SKILL = 6,
            SQUAD_POINT_COST = 7,
            PRIMARY_WEAPON_VALUE = 8,
            AGILITY_VALUE = 9,
            HULL_VALUE = 10,
            SHIELD_VALUE = 11,
            BARREL_ROLL = 12,
            BOOST = 13,
            CLOAK = 14,
            EVADE = 15,
            FOCUS = 16,
            SLAM = 17,
            TARGET_LOCK = 18,
            ASTROMECH = 19,
            BOMB = 20,
            CANNON = 21,
            CREW_MEMBER = 22,
            ELITE_PILOT_SKILL = 23,
            ILLICIT_UPGRADE = 24,
            MISSILES = 25,
            MODIFICATIONS = 26,
            SALVAGED_ATSTROMECH = 27,
            SYSTEM_UPGRADE = 28,
            TECH = 29,
            TITLE = 30,
            TORPEDOES = 31,
            TURRET = 32, 
            PILOT_ABILITY = 33,
            AVAILABILITY = 34,
            ERROR = 99
        }
    }
}
