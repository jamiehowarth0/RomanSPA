angular.module('RomanSPA.Controllers', ['RomanSPA'])
       .controller('GenericController',
            ['$scope', '$route', '$routeParams', function
             ($scope, $route, $routeParams) {

                // Retrieve our model using the modelfactory for our current URL path
                $.ajax($scope.templateUrl, { beforeSend: function (xhr) { xhr.setRequestHeader('X-RomanModelRequest', 'true'); } })
                 .done(function (data) { $scope.model = data; });
            }]);