var app = angular.module("app", ["app.controllers"]);

angular.module("app.controllers", [])
    .controller("dashboardController", function ($scope, $filter) {
        //$scope.Users = [{ name: 'dan' }];
        
        $scope.init = function (){
            
            $.ajax("http://localhost:55412/api/user", {
                success: function (data) {
                    $scope.Users = data.Users;
                    $scope.totalUserCount = $scope.Users.length;
                    $scope.totalMaleCount = $filter('filter')($scope.Users, { gender: 'male' }).length;
                    $scope.totalFemaleCount = $filter('filter')($scope.Users, { gender: 'female' }).length;
                    $scope.totalConnectionCount = 595483; //todo: get from graph

                    $scope.$apply();
                },
                error: function (e) {
                    alert(e);
                }
            });
        };

        $scope.getUniqueUserCount = function(filterString) {
            if (filterString) {
                return $filter('filter')($scope.Users, { gender: "female" });
                return 0;
            } else {
                return $scope.Users.length;
            }
        };

        $scope.getFilteredUsers = function (filter) {
            return $scope.Users.filter(filter);
        };


        
        //window.setTimeout($scope.init(), 300);
        //angular.element(document).ready(function () {
        //$scope.init = function () {

        //};
    });
    
