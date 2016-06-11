var xwVerbSelect = $("#xw_verb");
var xwNounSelect = $("#xw_noun");
var xwFactionSelect = $("#xw_faction");
var xwLoading = $(".loading");
var xwSearch = $("#search");
var xwPilotTbody = $(".pilot_tbody");
var xwPilotCount = $("#pilot_count");
var xwUpgradeTbody = $(".upgrade_tbody");
var xwUpgradeCount = $("#upgrade_count");
var xwCostNumberInput = $("#xw_cost");
var xwCostOperator = $("#xw_operator");
var xwName = $("#xw_name");
var xwFreeText = $("#xw_freetext");
var phrases = [];
var optionTemplate = ("<option value='#VALUE#'>#LABEL#</option>");
var popoverTemplate = "<button type='button' id='#ID#' class='btn btn-sm btn-dark' data-toggle='popover' title='#SHIP_NAME_TITLE#' data-content='#CONTENT#'>#SHIP_NAME# &nbsp;<i class='glyphicon glyphicon-eye-open'></i></button>";
var iconTemplate    = "<v class='#ICON#'></v>";
var xwFinder = {
    "init": function(parameter,data){
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
                        xwFinder.init("display", data);
                    },
                    complete: function () {
                        xwLoading.hide();
                        xwSearch.prop("disabled", false);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            },
            "display": function (data)
            {
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
            }
        }
        fn[parameter](data);
    },
    "search": function (parameter, data)
    {
        var fn = {
            "standard": function () {

                var noun = xwNounSelect.val();
                var verb = xwVerbSelect.val();
                var faction = xwFactionSelect.val();

                var queryString = [];

                queryString[0] = "method=standard";
                queryString[1] = "noun=" + noun;
                queryString[2] = "verb=" + verb;
                queryString[3] = "faction=" + faction;
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
                        xwFinder.search("loadSearchResult", data);
                    },
                    complete: function () {
                        xwLoading.hide();
                        xwSearch.prop("disabled", false);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            },
            "cost": function () {                
                var cost = xwCostNumberInput.val();
                var operator = xwCostOperator.val();
                var queryString = [];

                queryString[0] = "method=cost";
                queryString[1] = "operator=" + operator;
                queryString[2] = "cost=" + cost;

                $.ajax({
                    url: "search.ashx?" + queryString.join('&'),
                    contentType: "application/x-www-form-urlencoded",
                    dataType: "json",
                    beforeSend: function () {
                        xwLoading.show();
                        xwSearch.prop("disabled", true);
                    },
                    success: function (data) {
                        xwFinder.search("loadSearchResult", data);
                    },
                    complete: function () {
                        xwLoading.hide();
                        xwSearch.prop("disabled", false);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            },
            "name": function () {                
                var name = xwName.val();
                var queryString = [];

                queryString[0] = "method=name";
                queryString[1] = "name=" + name;

                $.ajax({
                    url: "search.ashx?" + queryString.join('&'),
                    contentType: "application/x-www-form-urlencoded",
                    dataType: "json",
                    beforeSend: function () {
                        xwLoading.show();
                        xwSearch.prop("disabled", true);
                    },
                    success: function (data) {
                        xwFinder.search("loadSearchResult", data);
                    },
                    complete: function () {
                        xwLoading.hide();
                        xwSearch.prop("disabled", false);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            },
            "freetext": function () {
                var freetext = xwFreeText.val();
                var queryString = [];

                queryString[0] = "method=freetext";
                queryString[1] = "freetext=" + freetext;

                $.ajax({
                    url: "search.ashx?" + queryString.join('&'),
                    contentType: "application/x-www-form-urlencoded",
                    dataType: "json",
                    beforeSend: function () {
                        //xwLoading.show();
                        xwSearch.prop("disabled", true);
                    },
                    success: function (data) {
                        xwFinder.search("loadSearchResult", data);
                    },
                    complete: function () {
                        //xwLoading.hide();
                        xwSearch.prop("disabled", false);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log(errorThrown);
                    }
                });
            },
            "loadSearchResult": function () {
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
                    <th>Ship</th>
                    <th>Ability</th>
                    <th>Availability</th>
                */
                $(pilots).each(function (index) {

                    var contentHtml = "";
                    var actionDetailsHtml = "";
                    var shipFeaturesHtml = "";
                    var actionHtml = "";
                    var conditionalActionHtml = "";
                    var shipDetailsHtml = "";
                    var conditionalShipDetailsHtml = "";
                    var popoverHtml = popoverTemplate;
                    var iconHtml = iconTemplate;
                    var YesOrNo = (this.isUnique) ? "Yes" : "No";
                    var shield = this.shipMetaData.shieldValue;
                    var hull = this.shipMetaData.hullValue;
                    var agility = this.shipMetaData.agilityValue;
                    var primaryWeaponValue = this.shipMetaData.primaryWeaponValue;
                    var actionDetails = this.shipMetaData.actionDetails;
                    var shipDetails = this.shipMetaData.shipDetails;
                    var nonStandardWeapon = (this.shipMetaData.nonStandardWeapon == "") ? "None" : this.shipMetaData.nonStandardWeapon;
                    var shipTitle = (this.shipMetaData.shipTitle == "") ? "None" : this.shipMetaData.shipTitle;

                    $(actionDetails).each(function (index) {
                        var iconCls = "";
                        switch (this.actionDesc) {
                            case "TARGET_LOCK":
                                iconCls = "icon-target-lock";
                                break;
                            case "BARREL_ROLL":
                                iconCls = "icon-barrel-roll";
                                break;
                            case "BOOST":
                                iconCls = "icon-boost";
                                break;
                            case "CLOAK":
                                iconCls = "icon-cloak";
                                break;
                            case "EVADE":
                                iconCls = "icon-evade";
                                break;
                            case "FOCUS":
                                iconCls = "icon-focus";
                                break;
                            case "SLAM":
                                iconCls = "icon-slam";
                                break;
                        }

                        var icon = '<v class="' + iconCls + '"></v>&nbsp;';

                        if (this.hasConditional) {
                            conditionalActionHtml += icon + '<em>' + this.conditional + '</em>';
                        } else {
                            actionDetailsHtml += icon
                        }
                    });

                    $(shipDetails).each(function (index) {
                        var iconCls = "icon-type-" + this.slotTypeDesc;
                        var icon = '<v class="' + iconCls + '"></v><em>x' + this.slotCount + '</em>&nbsp;';

                        if (this.hasConditional) {

                            conditionalShipDetailsHtml += icon + '<em>' + this.conditional + '</em>';
                            if ((index % 2) != 0) {
                                conditionalShipDetailsHtml += '<br>';
                            }
                            conditionalShipDetailsHtml = conditionalShipDetailsHtml.replace("'", "");
                        } else {

                            shipDetailsHtml += icon

                            if ((index % 2) != 0) {
                                shipDetailsHtml += '<br>';
                            }
                        }
                    });

                    if (conditionalShipDetailsHtml == "") {
                        conditionalShipDetailsHtml = '<em>None</em>';
                    }

                    var shipTemplate =
                         '<div>'
                       + '<strong>Stats</strong>'
                       + '<br>'
                       + '<v class="icon-primary"></v><strong><label class="stat-font-style text-primary-weapon">' + primaryWeaponValue + "</label></strong>&nbsp;"
                       + '<v class="icon-agility"></v><strong><label class="stat-font-style text-agility">' + agility + "</label></strong>&nbsp;"
                       + '<br>'
                       + '<v class="icon-hull"></v><strong><label class="stat-font-style text-hull">' + hull + "</label></strong>&nbsp;"
                       + '<v class="icon-shield"></v><strong><label class="stat-font-style text-shield">' + shield + "</label></strong>&nbsp;"
                       + '<br>'
                       + '<strong>Avaliable Actions</strong>'
                       + '<br>'
                       + actionDetailsHtml
                       + '<br>'
                       + '<strong>Upgrade Slots</strong>'
                       + '<br>'
                       + shipDetailsHtml
                       + '<br>'
                       + '<strong>Non-Standard Weapon</strong>'
                       + '<br>'
                       + '<em>' + nonStandardWeapon + '</em>'
                       + '<br>'
                       + '<strong>Title</strong>'
                       + '<br>'
                       + '<em>' + shipTitle.replace("'", "") + '</em>'
                       + '<br>'
                       + '<strong>Conditional Actions/Upgrades</strong>'
                       + '<br>'
                       + conditionalActionHtml
                       + '<br>'
                       + conditionalShipDetailsHtml
                       + '</div>';

                    // build pop over
                    popoverHtml = popoverHtml.replace("#SHIP_NAME#", this.shipType);
                    popoverHtml = popoverHtml.replace("#SHIP_NAME_TITLE#", this.shipType);
                    popoverHtml = popoverHtml.replace("#CONTENT#", shipTemplate);
                    popoverHtml = popoverHtml.replace("#ID#", index);

                    switch (this.factionDesc) {
                        case "REBEL":
                            iconHtml = iconHtml.replace("#ICON#", "icon-rebel");
                            break;
                        case "IMPERIAL":
                            iconHtml = iconHtml.replace("#ICON#", "icon-empire");
                            break;
                        case "SCUM":
                            iconHtml = iconHtml.replace("#ICON#", "icon-scum");
                            break;
                    }

                    pilotTbodyHtml += "<tr>";
                    pilotTbodyHtml += "<td><p class='pilot-skill'><strong>" + this.pilotSkill + "</strong></p></td>";
                    pilotTbodyHtml += "<td>" + this.squadPointCost + "</td>";
                    pilotTbodyHtml += "<td>" + YesOrNo + "</td>";
                    pilotTbodyHtml += "<td>" + this.name + "</td>";
                    pilotTbodyHtml += "<td>" + iconHtml + "</td>";
                    pilotTbodyHtml += "<td>" + popoverHtml + "</td>";
                    pilotTbodyHtml += "<td>" + this.pilotAbility + "</td>";
                    pilotTbodyHtml += "<td><label class='label label-default'>" + this.availability + "</label></td>";
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
                    var YesOrNo = (this.isUnique) ? "Yes" : "No";

                    var iconCls = "icon-type-" + this.typeDesc;
                    var icon = '<v class="' + iconCls + '">';

                    upgradeTbodyHtml += "<tr>";
                    upgradeTbodyHtml += "<td><strong>" + this.points + "</strong></td>";
                    upgradeTbodyHtml += "<td><strong>" + this.name + "</strong></td>";
                    upgradeTbodyHtml += "<td>" + YesOrNo + "</td>";
                    upgradeTbodyHtml += "<td>" + icon + "</td>";
                    upgradeTbodyHtml += "<td>" + this.ability + "</td>";
                    upgradeTbodyHtml += "<td><label class='label label-default'>" + this.availability + "</label></td>";
                    upgradeTbodyHtml += "</tr>";

                });

                // upgrade counts
                xwPilotCount.html(pilots.length);
                xwUpgradeCount.html(upgrades.length);

                // tbody
                xwPilotTbody.html(pilotTbodyHtml);
                xwUpgradeTbody.html(upgradeTbodyHtml);

                var popoverOptions = {
                    html: true
                };

                $(function () {
                    $('[data-toggle="popover"]').popover(popoverOptions);
                    $('[data-toggle="popover"]').on('click', function (e) {
                        $('[data-toggle="popover"]').not(this).popover('hide');
                    });
                })

            
            }
        }
        fn[parameter](data);
    }
}

