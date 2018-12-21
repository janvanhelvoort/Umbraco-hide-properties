function HidePropertiesResource($http) {
    return {
        // Rules
        getRules: function () {
            return $http.get(Umbraco.Sys.ServerVariables.hideProperties.getRules);
        },      
        saveRule: function (rule) {
            return $http.post(Umbraco.Sys.ServerVariables.hideProperties.saveRule, rule);
        }
    };
};

angular.module("umbraco.resources").factory("hidePropertiesResource", HidePropertiesResource)