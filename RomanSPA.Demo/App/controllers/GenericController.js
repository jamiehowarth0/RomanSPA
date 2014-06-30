angular.module('RomanSPA')
       .controller('GenericCtrl',
            ['$scope', '$route', '$routeParams', function 
             ($scope, $route, $routeParams) {

            // Retrieve our model using the modelfactory for our current URL path
            $.ajax($scope.templateUrl, {
                beforeSend: function (xhr) { xhr.setRequestHeader('X-RomanModelRequest', 'true'); },
                success: applyModel
            });

            function applyModel(data) { $scope.model = data; }
       }]);