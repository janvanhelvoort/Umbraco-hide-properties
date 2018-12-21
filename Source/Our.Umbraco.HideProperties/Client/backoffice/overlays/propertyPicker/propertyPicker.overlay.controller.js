function PropertyPickerOverlayController($scope) {
    $scope.pickProperty = function (property) {
        if (property.selected) {
            property.selected = false;

            angular.forEach($scope.model.selectedProperties, function (selectedProperty, index) {
                if (property.alias === selectedProperty.alias) {
                    $scope.model.selectedProperties.splice(index, 1);
                }
            });
        } else {
            property.selected = true;

            $scope.model.selectedProperties.push(property);
        }
    }

    $scope.getGroupName = function (groupId) {
        return $scope.model.selectedContentType.groups.find(function (group) {
            return group.id === groupId;
        }).name;
    }

    var selectedPropertyAliasses = $scope.model.selectedProperties.map(function (property) { return property.alias });
    angular.forEach($scope.model.selectedContentTypeProperties, function (property) {
        if (selectedPropertyAliasses.indexOf(property.alias) > -1) {
            property.selected = true;
        }
    });
}

angular.module("umbraco").controller("HideProperties.PropertyPickerOverlayController", PropertyPickerOverlayController);