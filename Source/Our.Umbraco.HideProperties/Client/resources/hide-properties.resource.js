function HidePropertiesResource($http) {
    return {
        // Rules
        getRules: function () {
            return $http.get(Umbraco.Sys.ServerVariables.hideProperties.getRules);
        },      
        saveRule: function (rule) {
            return $http.post(Umbraco.Sys.ServerVariables.hideProperties.saveRule, rule);
        },
        deleteRule: function(rule){
            return $http.delete(Umbraco.Sys.ServerVariables.hideProperties.deleteRule + "?key=" + rule.key);
        }
    };
};

angular.module("umbraco.resources").factory("hidePropertiesResource", HidePropertiesResource)