using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Utility;


namespace xwSearchLib.Model
{
    public class Upgrade : Meta
    {
        public XW_TYPE type { get; set; }
        public XW_RESTRICTION restriction { get; set; }
        public string typeDesc { get; set; }
        public string restrictionDesc { get; set; }
        public int points { get; set; }
        public string ability { get; set; }
        public string availability { get; set; }
        public string name { get; set; }
        public bool isUnique { get; set; }


        public Upgrade()
        { 

        }

        public Upgrade(string[] row) 
        {
            this.ability = row[Convert.ToInt32(Meta.EXCEL_SHEET_UPGRADE.ABILITY)];
            this.availability = row[Convert.ToInt32(Meta.EXCEL_SHEET_UPGRADE.AVAILABILITY)];
            this.name = row[Convert.ToInt32(Meta.EXCEL_SHEET_UPGRADE.NAME)];

            setPoints(row[Convert.ToInt32(Meta.EXCEL_SHEET_UPGRADE.POINTS)]);
            setUnique(row[Convert.ToInt32(Meta.EXCEL_SHEET_UPGRADE.UNIQUE)]);
            setRestriction(row[Convert.ToInt32(Meta.EXCEL_SHEET_UPGRADE.RESTRICTION)]);
            setType(row[Convert.ToInt32(Meta.EXCEL_SHEET_UPGRADE.TYPE)]);
        }

        private void setPoints(string input)
        {
            this.points = xwFormatter.fixSpreadSheetNumber(input);
        }
        private void setUnique(string input)
        {
            this.isUnique = xwFormatter.fixSpreadSheetBoolean(input);
        }

        private void setRestriction(string input)
        {
            switch (input)
            {
                case "Rebel Only.":
                    this.restriction = XW_RESTRICTION.REBEL_ONLY;
                    break;
                case "Imperial Only.":
                    this.restriction = XW_RESTRICTION.IMPERIAL_ONLY;
                    break;
                case "Scum Only.":
                    this.restriction = XW_RESTRICTION.SCUM_ONLY;
                    break;
                case "Limited.":
                    this.restriction = XW_RESTRICTION.LIMITED;
                    break;
                case "Scum Only. Limited.":
                    this.restriction = XW_RESTRICTION.SCUM_ONLY_LIMITED;
                    break;                   
                default:
                    this.restriction = XW_RESTRICTION.NONE;
                    break;
            }

            this.restrictionDesc = this.restriction.ToString();
        }

        private void setType(string input)
        {
            switch (input)
            {
                case "Astromechs":
                    this.type = Meta.XW_TYPE.ASTROMECHS;
                    break;
                case "Bombs":
                    this.type = Meta.XW_TYPE.BOMBS;
                    break;
                case "Cannons":
                    this.type = Meta.XW_TYPE.CANNONS;
                    break;
                case "Crew Members":
                    this.type = Meta.XW_TYPE.CREW_MEMBERS;
                    break;
                case "Elite Talents":
                    this.type = Meta.XW_TYPE.ELITE_TALENTS;
                    break;
                case "Illicit Upgrade":
                    this.type = Meta.XW_TYPE.ILLICIT_UPGRADE;
                    break;
                case"Missiles":
                    this.type = Meta.XW_TYPE.MISSILES;
                    break;
                case "Modifications":
                    this.type = Meta.XW_TYPE.MODIFICATIONS;
                    break;
                case "Salvaged Astromech":
                    this.type = Meta.XW_TYPE.SALVAGED_ASTROMECH;
                    break;
                case "System Upgrade":
                    this.type = Meta.XW_TYPE.SYSTEM_UPGRADE;
                    break;
                case "Tech":
                    this.type = Meta.XW_TYPE.TECH;
                    break;
                case "Titles":
                    this.type = Meta.XW_TYPE.TITLES;
                    break;
                case "Torpedoes":
                    this.type = Meta.XW_TYPE.TORPEDOES;
                    break;
                case "Turrets":
                    this.type = Meta.XW_TYPE.TURRETS;
                    break;
                default:
                    this.type = Meta.XW_TYPE.ERROR;
                    break;
            }

            this.typeDesc = this.type.ToString();
        }
    }
}
