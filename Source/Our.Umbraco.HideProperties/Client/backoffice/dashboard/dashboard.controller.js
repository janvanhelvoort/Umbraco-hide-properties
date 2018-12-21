﻿function DashboardController($scope, localizationService, hidePropertiesResource) {
    $scope.page = { isLoading: true, };
    $scope.content = { rules: undefined };

    hidePropertiesResource.getRules().then(function (result) {
        $scope.content.rules = result.data;
        $scope.isLoading = false;
    });

    $scope.openRuleBuilderOverlay = function (rule) {        
        $scope.ruleBuilderOverlay = {
            view: "../App_Plugins/Umbraco-hide-properties/backoffice/overlays/ruleBuilder/ruleBuilder.overlay.html",
            title: localizationService.localize("hideProperties_ruleOverlay"),
            show: true,
            initialRule: rule,
            hideSubmitButton: false,
            submit: function (model) {
                hidePropertiesResource.saveRule(model.rule).then(function(result) {
                    if(model.rule.key){
                        angular.forEach($scope.content.rules, function(rule, index){
                            if(rule.key === result.data.key) {
                                $scope.content.rules[index] = result.data;
                            }
                        }); 
                    } else {
                        $scope.content.rules.push(result.data);
                    }
                })
            
                $scope.ruleBuilderOverlay.show = false;
                $scope.ruleBuilderOverlay = null;
            }
        };        
    };
}

angular.module("umbraco").controller("HideProperties.DashboardController", DashboardController);