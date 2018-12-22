function DashboardController($scope, localizationService, hidePropertiesResource) {
    $scope.page = { isLoading: true, };
    $scope.content = { rules: [] };

    hidePropertiesResource.getRules().then(function (result) {
        $scope.content.rules = result.data;
        $scope.isLoading = false;
    });

    $scope.selectItem = function (rule, $event) {
        rule.isSelected = !rule.isSelected;

        $event.stopPropagation();
    };

    $scope.selectAll = function ($event) {
        angular.forEach($scope.content.rules, function (rule, index) {
            rule.isSelected = $event.target.checked;
        });

        $event.stopPropagation();
    };

    $scope.isSelectedAll = function () {
        return $scope.content.rules.every(function (rule) { return rule.isSelected });
    };

    $scope.openRuleBuilderOverlay = function (rule) {
        $scope.ruleBuilderOverlay = {
            view: "../App_Plugins/Umbraco-hide-properties/backoffice/overlays/ruleBuilder/ruleBuilder.overlay.html",
            title: localizationService.localize("hideProperties_ruleOverlay"),
            show: true,
            initialRule: rule,
            hideSubmitButton: false,
            submit: function (model) {
                hidePropertiesResource.saveRule(model.rule).then(function (result) {
                    if (model.rule.key) {
                        angular.forEach($scope.content.rules, function (rule, index) {
                            if (rule.key === result.data.key) {
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