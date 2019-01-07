function RuleBuilderOverlayController($scope, contentTypeResource, userGroupsResource, localizationService) {

    $scope.isLoading = true;
    $scope.content = { selectedContentType: undefined, selectedTabs: [], selectedProperties: [], selectedUserGroups: [] };

    contentTypeResource.getAll().then((contentTypes) => {        
        $scope.isLoading = false;

        if ($scope.model) {
            $scope.contentTypes = contentTypes;

            if ($scope.model.initialRule) {
                var initialRule = $scope.model.initialRule;

                $scope.model.rule = angular.copy(initialRule);

                var contentType = $scope.contentTypes.find(function (contentType) {
                    return contentType.alias === initialRule.contentTypeAlias;
                });

                if (contentType) {
                    contentTypeResource.getById(contentType.id).then(function (contentType) {
                        $scope.content.selectedContentType = contentType;

                        $scope.content.selectedTabs = contentType.groups.filter(function (group) {
                            return initialRule.tabs.includes(group.name);
                        });

                        var properties = contentType.groups.reduce(function (accumulator, currentValue) {
                            return accumulator.concat(currentValue.properties);
                        }, []);

                        $scope.content.selectedProperties = properties.filter(function (property) {
                            return initialRule.properties.includes(property.alias);
                        });
                    });

                    userGroupsResource.getUserGroups({ onlyCurrentUserGroups: false }).then((userGroups) => {
                        $scope.content.selectedUserGroups = userGroups.filter(function (userGroup) {
                            return initialRule.userGroups.includes(userGroup.alias);
                        });
                    });

                    $scope.ruleBuilderForm.selectedContentType.$setValidity("selectedContentType", true)
                    $scope.ruleBuilderForm.selectedUserGroups.$setValidity("selectedUserGroups", initialRule.userGroups.length > 0);
                    $scope.ruleBuilderForm.selectedTabsOrProperties.$setValidity("selectedTabsOrProperties", initialRule.tabs.length > 0 || initialRule.properties.length > 0);
                } else {
                    $scope.ruleBuilderForm.selectedContentType.$setValidity("selectedContentType", false);
                }
            } else {
                $scope.model.rule = { isActive: true, tabs: [], properties: [], userGroups: [] };
                $scope.ruleBuilderForm.selectedContentType.$setValidity("selectedContentType", false);
            }
        }
    });

    $scope.getGroupName = function (groupId) {
        return $scope.content.selectedContentType.groups.find(function (group) {
            return group.id === groupId;
        }).name;
    }

    $scope.openContentTypePickerOverlay = function () {
        $scope.contentTypePickerOverlay = {
            view: "itempicker",
            title: localizationService.localize("hideProperties_contentTypeOverlay"),
            availableItems: $scope.contentTypes,
            show: true,
            submit: function (model) {
                contentTypeResource.getById(model.selectedItem.id).then(function (contentType) {
                    $scope.content.selectedContentType = contentType;
                    $scope.model.rule.contentTypeAlias = contentType.alias;

                    $scope.contentTypePickerOverlay.show = false;
                    $scope.contentTypePickerOverlay = null;

                    $scope.ruleBuilderForm.selectedContentType.$setValidity("selectedContentType", true);
                    $scope.ruleBuilderForm.selectedTabsOrProperties.$setValidity("selectedTabsOrProperties", false);
                    $scope.ruleBuilderForm.selectedUserGroups.$setValidity("selectedUserGroups", false);
                });
            }
        };
    };

    $scope.openTabPickerOverlay = function () {
        $scope.tabPickerOverlay = {
            view: "../App_Plugins/Umbraco-hide-properties/backoffice/overlays/tabPicker/tabPicker.overlay.html",
            title: localizationService.localize("hideProperties_tabOverlay"),
            show: true,
            hideSubmitButton: false,
            selectedTabs: $scope.content.selectedTabs,
            selectedContentTypeGroups: $scope.content.selectedContentType.groups,
            submit: function (model) {
                $scope.content.selectedTabs = model.selectedTabs;
                $scope.model.rule.tabs = model.selectedTabs.map(function (tab) { return tab.name });

                $scope.tabPickerOverlay.show = false;
                $scope.tabPickerOverlay = null;

                $scope.ruleBuilderForm.selectedTabsOrProperties.$setValidity("selectedTabsOrProperties", true);
            }
        };
    };

    $scope.openPropertyPickerOverlay = function () {
        var properties = $scope.content.selectedContentType.groups.reduce(function (accumulator, currentValue) {
            return accumulator.concat(currentValue.properties);
        }, []);

        $scope.propertyPickerOverlay = {
            view: "../App_Plugins/Umbraco-hide-properties/backoffice/overlays/propertyPicker/propertyPicker.overlay.html",
            title: localizationService.localize("hideProperties_propertyOverlay"),
            show: true,
            hideSubmitButton: false,
            selectedProperties: $scope.content.selectedProperties,
            selectedContentType: $scope.content.selectedContentType,
            selectedContentTypeProperties: properties,
            submit: function (model) {
                $scope.content.selectedProperties = model.selectedProperties;
                $scope.model.rule.properties = model.selectedProperties.map(function (property) { return property.alias });

                $scope.propertyPickerOverlay.show = false;
                $scope.propertyPickerOverlay = null;

                $scope.ruleBuilderForm.selectedTabsOrProperties.$setValidity("selectedTabsOrProperties", true);
            }
        };
    };

    $scope.openUserGroupPickerOverlay = function () {
        $scope.userGroupPickerOverlay = {
            view: "usergrouppicker",
            title: localizationService.localize("hideProperties_userGroupOverlay"),
            show: true,
            hideSubmitButton: false,
            selection: $scope.content.selectedUserGroups,
            submit: function (model) {
                $scope.content.selectedUserGroups = model.selection;
                $scope.model.rule.userGroups = model.selection.map(function (userGroup) { return userGroup.alias });

                $scope.userGroupPickerOverlay.show = false;
                $scope.userGroupPickerOverlay = null;

                $scope.ruleBuilderForm.selectedUserGroups.$setValidity("selectedUserGroups", true);
            }
        };
    }

    $scope.removeContentType = function () {
        $scope.content = { selectedTabs: [], selectedProperties: [], selectedUserGroups: [] };

        $scope.ruleBuilderForm.selectedContentType.$setValidity("selectedContentType", false);
        $scope.ruleBuilderForm.selectedUserGroups.$setValidity("selectedUserGroups", true);
    };

    $scope.removeTab = function (tab) {
        angular.forEach($scope.content.selectedTabs, function (selectedTab, index) {
            if (tab.name === selectedTab.name) {
                $scope.content.selectedTabs.splice(index, 1);
            }
        });

        $scope.model.rule.tabs = $scope.content.selectedTabs.map(function (tab) { return tab.name });

        $scope.ruleBuilderForm.selectedTabsOrProperties.$setValidity("selectedTabsOrProperties", $scope.model.rule.tabs.length > 0 || $scope.model.rule.properties.length > 0);
    };

    $scope.removeProperty = function (property) {
        angular.forEach($scope.content.selectedProperties, function (selectedProperty, index) {
            if (property.alias === selectedProperty.alias) {
                $scope.content.selectedProperties.splice(index, 1);
            }
        });

        $scope.model.rule.properties = $scope.content.selectedProperties.map(function (property) { return property.alias });

        $scope.ruleBuilderForm.selectedTabsOrProperties.$setValidity("selectedTabsOrProperties", $scope.model.rule.tabs.length > 0 || $scope.model.rule.properties.length > 0);
    };

    $scope.removeUserGroup = function (userGroup) {
        angular.forEach($scope.content.selectedUserGroups, function (selectedUserGroup, index) {
            if (userGroup.alias === selectedUserGroup.alias) {
                $scope.content.selectedUserGroups.splice(index, 1);
            }
        });

        $scope.model.rule.userGroups = $scope.content.selectedUserGroups.map(function (userGroup) { return userGroup.alias });

        $scope.ruleBuilderForm.selectedUserGroups.$setValidity("selectedUserGroups", $scope.model.rule.userGroups.length > 0);
    }
}

angular.module("umbraco").controller("HideProperties.RuleBuilderOverlayController", RuleBuilderOverlayController);