using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Utility;

namespace xwSearchLib.Model
{
    public class Pilot : Meta
    {
        public int squadPointCost { get; set; }
        public string name { get; set; }
        public string shipType { get; set; }
        public int pilotSkill { get; set; }
        public string pilotAbility { get; set; }
        public bool isUnique { get; set; }
        public string availability { get; set; }
        public XW_FACTION faction { get; set; }
        public string factionDesc { get; set; }
        public ShipMetaData shipMetaData { get; set; }

        public Pilot() 
        {
            this.shipMetaData = new ShipMetaData();
        }

        public Pilot(string[] row)
        {
            this.shipMetaData = new ShipMetaData();
            this.name = row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.PILOT_NAME)];
            this.shipType = row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.SHIP_TYPE)];
            this.availability = row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.AVAILABILITY)];
            this.pilotAbility = row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.PILOT_ABILITY)];

            setPilotSkill(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.PILOT_SKILL)]);
            setUnique(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.UNIQUE)]);
            setFaction(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.FACTION)]);
            setSquadPointCost(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.SQUAD_POINT_COST)]);

            // pass on to the next constructor
            setShipMetaData(row);
        }

        private void setShipMetaData(string[] row)
        {
            this.shipMetaData = new ShipMetaData(row);
        }

        private void setFaction(string input)
        {
            switch (input)
            {
                case "Imperial":
                    this.faction = XW_FACTION.IMPERIAL;
                    break;
                case "Rebel":
                    this.faction = XW_FACTION.REBEL;
                    break;
                case "Scum":
                    this.faction = XW_FACTION.SCUM;
                    break;
                default:
                    this.faction = XW_FACTION.ERROR;
                    break;
            }

            this.factionDesc = this.faction.ToString();
        }

        private void setUnique(string input)
        {
            this.isUnique = xwFormatter.fixSpreadSheetBoolean(input);
        }

        private void setPilotSkill(string input)
        {
            this.pilotSkill = xwFormatter.fixSpreadSheetNumber(input);
        }

        private void setSquadPointCost(string input)
        {
            this.squadPointCost = xwFormatter.fixSpreadSheetNumber(input);
        }
    }
}
