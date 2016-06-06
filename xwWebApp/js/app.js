var xwVerbSelect = $("#xw_verb");
var xwNounSelect = $("#xw_noun");
var xwFactionSelect = $("#xw_faction");

var optionTemplate = ("<option value='#VALUE#'>#LABEL#</option>");
var xwFinder = {
    "init": function(parameter){
            var fn = {
                "load": function () {
                    $.ajax({
                        url: "speech.ashx",
                        contentType: "application/x-www-form-urlencoded",
                        dataType: "json",
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
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(errorThrown);
                        }
                    });
                }
            }
            fn[parameter]();
        }                     
    }     