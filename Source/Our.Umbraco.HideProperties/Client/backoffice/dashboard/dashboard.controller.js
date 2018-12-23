function DashboardController($scope, $q, localizationService, hidePropertiesResource) {
    $scope.isLoading = true;
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

    $scope.clearSelection = function () {
        angular.forEach($scope.content.rules, function (rule, index) {
            rule.isSelected = false;
        });
    };

    $scope.isAnythingSelected = function () {
        return $scope.content.rules.some(function (rule) { return rule.isSelected });
    };

    $scope.isSelectedAll = function () {
        return $scope.content.rules.every(function (rule) { return rule.isSelected });
    };

    $scope.changeActiveState = function (activeState) {
        $scope.actionInProgress = true;

        $q.all($scope.content.rules.filter(function (rule) { return rule.isSelected && rule.isActive != activeState }).map(function (rule) {
            rule.isActive = activeState
            return hidePropertiesResource.saveRule(rule);
        })).then(function () {
            $scope.actionInProgress = false;
        });
    }

    $scope.deleteRules = function () {
        $scope.actionInProgress = true;

        $q.all($scope.content.rules.filter(function (rule) { return rule.isSelected }).map(function (rule) {
            return hidePropertiesResource.deleteRule(rule);
        })).then(function (results) {
            $scope.isLoading = true;

            var removedRules = results.map(function (result) { return result.data.key });

            angular.forEach($scope.content.rules, function (rule, index) {
                if (removedRules.includes(rule.key)) {
                    $scope.content.rules.splice(index, 1);
                }
            });

            $scope.isLoading = false;
            $scope.actionInProgress = false;
        });
    }

    $scope.openRuleBuilderOverlay = function (rule) {
        $scope.ruleBuilderOverlay = {
            view: "../App_Plugins/Umbraco-hide-properties/backoffice/overlays/ruleBuilder/ruleBuilder.overlay.html",
            title: localizationService.localize("hideProperties_ruleOverlay"),
            show: true,
            initialRule: rule,
            hideSubmitButton: false,
            submit: function (model) {
                $scope.isLoading = true;

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

                    $scope.isLoading = false;
                })

                $scope.ruleBuilderOverlay.show = false;
                $scope.ruleBuilderOverlay = null;
            }
        };
    };
}

angular.module("umbraco").controller("HideProperties.DashboardController", DashboardController);