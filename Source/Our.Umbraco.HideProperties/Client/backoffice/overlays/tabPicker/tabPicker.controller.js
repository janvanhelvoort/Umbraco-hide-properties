function TabPickerOverlayController($scope) {
    $scope.pickGroup = function (group) {
        if (group.selected) {
            group.selected = false;

            angular.forEach($scope.model.selectedTabs, function (selectedTab, index) {
                if (group.name === selectedTab.name) {
                    $scope.model.selectedTabs.splice(index, 1);
                }
            });
        } else {
            group.selected = true;

            $scope.model.selectedTabs.push(group);
        }
    }

    var selectedTabs = $scope.model.selectedTabs.map(function (tab) { return tab.name });
    angular.forEach($scope.model.selectedContentTypeGroups, function (group) {
        if (selectedTabs.indexOf(group.name) > -1) {
            group.selected = true;
        }
    });
}

angular.module("umbraco").controller("HideProperties.TabPickerOverlayController", TabPickerOverlayController);