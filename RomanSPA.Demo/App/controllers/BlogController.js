angular.module('RomanSPA')
        .controller('BlogCtrl',
        ['$scope', '$route', '$routeParams', , function 
         ($scope, $route, $routeParams) {
            $controller('GenericCtrl', {
                $Scope: $scope,
                $route: $route,
                $routeParams: $routeParams,
            });

            function storePostsOffline(data) {
                if (!supportsLocalStorage()) { return false; }
                localStorage["RomanSPA.data.blog.posts"] = data;
                return true;
            }

            storePostsOffline($scope.model);
        }]);