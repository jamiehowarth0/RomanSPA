angular.module('RomanSPA')
       .controller('GenericCtrl', ['$scope', function ($scope) {

           // This will attempt to make an AJAX request to the current route URL
           // e.g. /blog and it will load a JSON model specified in the factory on the 
           // corresponding action's [RomanAction] attribute, if it exists. If not, it will return null
           // and you can do as you please with the model, but it's a handy shortcut for simple JSON loads.

           $.ajax($scope.templateUrl, {
               beforeSend: function (xhr) { xhr.setRequestHeader('X-RomanModelRequest', 'true'); },
               success: applyModel
           });

           function applyModel(data) { $scope.model = data; }
       }]);