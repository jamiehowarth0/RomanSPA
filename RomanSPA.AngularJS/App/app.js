// angular.module('RomanSPA.Controllers', []);

angular.module('RomanSPA', ['ngRoute'])
        .config(['$routeProvider', '$locationProvider', function 
                 ($routeProvider, $locationProvider) {

            $locationProvider.html5Mode(true);

            // initialization
            // this calls an AJAX request to get available server routes.
            // then, it parses the route dictionary and sets up AngularJS's routeProvider.
            //
            // Normally we would use templateUrl, however we need to add the headers.
            // We *COULD* set $http defaults, as that's what AngularJS uses to retrieve the view.
            // However, that's ugly, as then we're adding a header to every $http request - not ideal and not suitable for the purpose.
            // The next option to us is $route.template, which represents the actual markup
            // ($route.template == $.ajax($route.templateUrl).success(fn(data) {} )
            // 
            // So we manually retrieve the template markup for the view using jQuery AJAX, as it gives us a little finer control.
            // However we need to preload all the templates - hence async:false - before we wire up routing.

            // The upshot of this is that our templates are then cached in AngularJS, so no further requests to server
            // and enables offline access.
            
            $.ajax({
                url: '/api/RouteApi/ServerRoutes',
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        getRouteTemplate($routeProvider, data[i].RoutePattern, data[i].controller, data[i].templateUrl);
                    }
                },
                async: false
            });

            // Private helpers
            function addHeaders(xhr) { xhr.setRequestHeader('X-RomanViewRequest', 'true'); }

            function getRouteTemplate($routeProvider, pattern, controller, templUrl) {
                $.ajax({ url: templUrl, beforeSend: addHeaders, async: false })
                 .done(function (data) {
                     setRoute($routeProvider, pattern, controller, data);
                     $routeProvider.otherwise({ redirectTo: '/', caseInsensitiveMatch: true });
                 });
            }

            function setRoute($routeProvider, pattern, controller, template) {
                $routeProvider.when(pattern, { controller: controller, template: template, caseInsensitiveMatch: true });
            }
            // end Private helpers
        }])
    .run(['$route', function ($route) {
        // If route table is populated (cause AJAX is *A*synchronous, we forced async:false specifically so that this would fire *after* routes are initialized.
        if ($route.routes.length > 0) $route.reload();
    }])
    .value('breeze', window.breeze);