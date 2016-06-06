var xwVerbSelect = $("#xw_verb");
var xwNounSelect = $("#xw_noun");
var xwFactionSelect = $("#xw_faction");
var xwLoading = $(".loading");
var xwSearch = $("#search");
var xwPilotTbody = $(".pilot_tbody");
var xwPilotCount = $("#pilot_count");
var xwUpgradeTbody = $(".upgrade_tbody");
var xwUpgradeCount = $("#upgrade_count");


var optionTemplate = ("<option value='#VALUE#'>#LABEL#</option>");
var xwFinder = {
    "init": function(parameter){
        var fn = {
            "load": function () {
                $.ajax({
                    url: "speech.ashx",
                    contentType: "application/x-www-form-urlencoded",
                    dataType: "json",
                    beforeSend: function(){
                        xwLoading.show();
                        xwSearch.prop("disabled", true);
                    },
                    success: function (data) {
                       
                        xwVerbSelect.find('option')
                                    .remove()
                                    .end()
                                    .append('<option selected="selected" value="default">Select Action</option>')
                                    .val('default');

                        xwNounSelect.find('option')
                                 .remove()
                                 .end()
                                 .append('<option selected="selected" value="default">Select Feature</option>')
                                 .val('default');
                         
                        var actions = [];
                        var features = [];
                        var facitons = [];
                        var actionHtml = "";
                        var featureHtml = "";
                        var factionHtml = "";

                        actions = data.Verbs;                            
                        features = data.Nouns;
                        factions = data.Faction;

                        for (var action in actions) {
                            var templateAction = optionTemplate
                                .replace("#VALUE#", actions[action])
                                .replace("#LABEL#", actions[action]);

                            actionHtml += templateAction;

                        }

                        for (var feature in features) {
                            var templateFeature = optionTemplate
                                .replace("#VALUE#", features[feature])
                                .replace("#LABEL#", features[feature]);

                            featureHtml += templateFeature;
                        }

                        for (var faction in factions) {
                            var templateFaction = optionTemplate
                                .replace("#VALUE#", factions[faction])
                                .replace("#LABEL#", factions[faction]);

                            factionHtml += templateFaction;
                        }

                        xwVerbSelect.append(actionHtml);
                        xwNounSelect.append(featureHtml);
                        xwFactionSelect.append(factionHtml);

                    },
                    complete: function () {
                        xwLoading.hide();
                        xwSearch.prop("disabled", false);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            }                

        }
        fn[parameter]();
    },
    "search": function (parameter) {
        var fn = {
            "basic": function () {

                var noun = xwNounSelect.val();
                var verb = xwVerbSelect.val();
                var faction = xwFactionSelect.val();

                var queryString = [];

                queryString[0] = "noun=" + noun;
                queryString[1] = "verb=" + verb;
                queryString[2] = "faction=" + faction;

                $.ajax({
                    url: "search.ashx?" + queryString.join('&'),
                    contentType: "application/x-www-form-urlencoded",
                    dataType: "json",
                    beforeSend: function()
                    {
                        xwLoading.show();
                        xwSearch.prop("disabled", true);
                    },
                    success: function (data) {
                        console.log(data)
                        var pilots = data["pilots"];
                        var upgrades = data["upgrades"];
                        var pilotTbodyHtml = "";
                        var upgradeTbodyHtml = "";

                        /*
                            <th>Pilot Skill</th>  
                            <th>Squad Points</th>   
                            <th>Unique?</th>                                     
                            <th>Name</th>                                                                                       
                            <th>Faction</th>
                            <th>Ship [p/h/a/s]</th>
                            <th>Ability</th>
                            <th>Availability</th>
                        */                        
                        $(pilots).each(function (index) {

                            var shield              = this.shipMetaData.shieldValue;
                            var hull                = this.shipMetaData.hullValue;
                            var agility             = this.shipMetaData.agilityValue;
                            var primaryWeaponValue  = this.shipMetaData.primaryWeaponValue;
                            var shipTemplate = "["
                                + primaryWeaponValue
                                + "/" + hull
                                + "/" + agility
                                + "/" + shield + "]";

                            pilotTbodyHtml += "<tr>";
                            pilotTbodyHtml += "<td>" + this.pilotSkill      + "</td>";
                            pilotTbodyHtml += "<td>" + this.squadPointCost  + "</td>";
                            pilotTbodyHtml += "<td>" + this.isUnique        + "</td>";
                            pilotTbodyHtml += "<td>" + this.name            + "</td>";
                            pilotTbodyHtml += "<td>" + this.factionDesc     + "</td>";
                            pilotTbodyHtml += "<td>" + this.shipType + " " + shipTemplate + "</td>";
                            pilotTbodyHtml += "<td>" + this.pilotAbility    + "</td>";
                            pilotTbodyHtml += "<td>" + this.availability    + "</td>";
                            pilotTbodyHtml += "</tr>";
                        });

                        /*
                            <th>Squad Points</th>
                            <th>Name</th>   
                            <th>Unique?</th>                                         
                            <th>Type</th>
                            <th>Ability</th>
                            <th>Availability</th>
                       */
                        $(upgrades).each(function (index) {

                            upgradeTbodyHtml += "<tr>";
                            upgradeTbodyHtml += "<td>" + this.points        + "</td>";
                            upgradeTbodyHtml += "<td>" + this.name          + "</td>";
                            upgradeTbodyHtml += "<td>" + this.isUnique      + "</td>";
                            upgradeTbodyHtml += "<td>" + this.typeDesc      + "</td>";
                            upgradeTbodyHtml += "<td>" + this.ability       + "</td>";
                            upgradeTbodyHtml += "<td>" + this.availability  + "</td>";
                            upgradeTbodyHtml += "</tr>";

                        });

                        // upgrade counts
                        xwPilotCount.html(pilots.length);
                        xwUpgradeCount.html(upgrades.length);

                        // tbody
                        xwPilotTbody.html(pilotTbodyHtml);
                        xwUpgradeTbody.html(upgradeTbodyHtml);

                    },
                    complete: function () {
                        xwLoading.hide();
                        xwSearch.prop("disabled", false);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            }
        }
        fn[parameter]();
    }
}
