using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xwSearchLib.Utility;

namespace xwSearchLib.Model
{
    public class ShipMetaData : Meta
    {      
        public int primaryWeaponValue { get; set; }
        public int agilityValue { get; set; }
        public int hullValue { get; set; }
        public int shieldValue { get; set; }
        public string nonStandardWeapon  { get; set; }
        public string shipTitle { get; set; }
        public XW_SHIP_SIZE shipSize { get; set; }
        public List<XWTypeDetail> shipDetails { get; set; }
        public List<XWActionDetail> actionDetails { get; set; }

        public ShipMetaData() {
            this.shipDetails = new List<XWTypeDetail>();
            this.actionDetails = new List<XWActionDetail>();
        }

        public ShipMetaData(string[] row)
        {
            this.shipDetails = new List<XWTypeDetail>();
            this.actionDetails = new List<XWActionDetail>();
           
            // one to one proeprty assignment
            this.nonStandardWeapon = row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.NONSTANDARD_WEAPON)];
            this.shipTitle = row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.TITLE)];
            
            // use this functions to help clean up some of the spread sheet logic
            setShipSize(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.SHIP_SIZE)]);
            setPrimaryWeaponValue(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.PRIMARY_WEAPON_VALUE)]);
            setAgilityValue(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.AGILITY_VALUE)]);
            setHullValue(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.HULL_VALUE)]);
            setShieldValue(row[Convert.ToInt32(Meta.EXCEL_SHEET_PILOT.SHIELD_VALUE)]);

            // set another layer for each lineir property
            setShipDetails(row);
            setActionDetails(row);
        }

        private void setShipDetails(string[] row)
        {
            List<XW_TYPE> listOfUpgradeTypes = new List<XW_TYPE>()
            {
                XW_TYPE.ASTROMECHS,
                XW_TYPE.BOMBS,
                XW_TYPE.CANNONS,
                XW_TYPE.CREW_MEMBERS,
                XW_TYPE.ELITE_TALENTS,
                XW_TYPE.ILLICIT_UPGRADE,
                XW_TYPE.MISSILES,
                XW_TYPE.MODIFICATIONS,
                XW_TYPE.SALVAGED_ASTROMECH,
                XW_TYPE.SYSTEM_UPGRADE,
                XW_TYPE.TECH,
                XW_TYPE.TORPEDOES,
                XW_TYPE.TURRETS
            };

            foreach (XW_TYPE type in listOfUpgradeTypes)
            {
                Meta.EXCEL_SHEET_PILOT pilotColumn = xwDictionary.getPilotColumnByType(type);

                XWTypeDetail typeDetail = new XWTypeDetail(row, pilotColumn, type);

                if (typeDetail.slotType != XW_TYPE.NONE) 
                {
                    this.shipDetails.Add(typeDetail);
                }
            }
        }

        private void setActionDetails(string[] row)
        {
            List<XW_SHIP_ACTIONS> listOfActions = new List<XW_SHIP_ACTIONS>() { 
                XW_SHIP_ACTIONS.BARREL_ROLL,
                XW_SHIP_ACTIONS.BOOST,
                XW_SHIP_ACTIONS.CLOAK,
                XW_SHIP_ACTIONS.EVADE,
                XW_SHIP_ACTIONS.FOCUS,
                XW_SHIP_ACTIONS.SLAM,
                XW_SHIP_ACTIONS.TARGET_LOCK
            };

            foreach (XW_SHIP_ACTIONS shipAction in listOfActions)
            {
                Meta.EXCEL_SHEET_PILOT pilotColumn = xwDictionary.getPilotColumnByAction(shipAction);

                XWActionDetail actionDetail = new XWActionDetail(row, pilotColumn, shipAction);

                if (actionDetail.action != XW_SHIP_ACTIONS.NONE)
                {
                    this.actionDetails.Add(actionDetail);
                }
            }
        }

        private void setPrimaryWeaponValue(string input)
        {
            this.primaryWeaponValue = xwFormatter.fixSpreadSheetNumber(input);
        }

        private void setAgilityValue(string input)
        {
            this.agilityValue = xwFormatter.fixSpreadSheetNumber(input);
        }

        private void setHullValue(string input)
        {
            this.hullValue = xwFormatter.fixSpreadSheetNumber(input);
        }

        private void setShieldValue(string input)
        {
            this.shieldValue = xwFormatter.fixSpreadSheetNumber(input);
        }

        private void setShipSize(string input)
        {
            switch (input)
            {
                case "Small":
                    this.shipSize = XW_SHIP_SIZE.SMALL;
                    break;
                case "Large":
                    this.shipSize = XW_SHIP_SIZE.LARGE;
                    break;
                case "Epic":
                    this.shipSize = XW_SHIP_SIZE.EPIC;
                    break;
                default:
                    this.shipSize = XW_SHIP_SIZE.ERROR;
                    break;
            }

        }
    }
}
