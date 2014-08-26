angular.module('RomanSPA', ['ngRoute'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider', function
                 ($routeProvider, $locationProvider, $httpProvider) {

            // BH: Note - we can't use $q cause it's an AngularJS service, and you can't inject services into .config() methods,
            // as service resolution in AngularJS happens after module configuration.
            // Route setup has to be done in the .config() method, hence this appoach using jQuery's deferred API.

            $locationProvider.html5Mode(true);

            // BH: This is only if preloading templates is switched off.
            // AngularJS uses the $http service to retrieve templates, so you have to add this header to each template request if you're not pre-loading the templates.
            $httpProvider.defaults.headers.get = { "X-RomanViewRequest": "true" };

            var preloadTemplates = true;

            var populateRouteTable = function (data) {
                for (var r = 0; r < data.length; r++) {

                    if (preloadTemplates) {
                        $.ajax(data[r].templateUrl, { async: false, beforeSend: function (xhr) { xhr.setRequestHeader("X-RomanViewRequest", "true"); } })
                            .always(function (templateContent) { console.debug("RomanSPA: Attempting to load template with URL: '" + data[r].templateUrl + "'") })
                            .done(function (templateContent) {
                                console.debug("RomanSPA: Template loaded for URL '" + data[r].templateUrl + "'");
                                $routeProvider.when(data[r].RoutePattern, { controller: data[r].routeController, template: templateContent });
                            })
                            .fail(function(templateContent) {
                                // BH: If we can't preload the template, let it be loaded on-demand when we navigate
                                console.error("RomanSPA: ERROR: Failed to retrieve template with URL '" + data[r].templateUrl + "'");
                                $routeProvider.when(data[r].RoutePattern, { controller: data[r].routeController, templateUrl: data[r].templateUrl });
                            });
                    } else {
                        // BH: If we're not pre-loading, let templates be loaded on-demand.
                        $routeProvider.when(data[r].RoutePattern, { controller: data[r].routeController, templateUrl: data[r].templateUrl });
                    }
                }
                $routeProvider.otherwise({ redirectTo: '/', caseInsensitiveMatch: true })
            }


            // BH: This is ugly, I know, but your route table MUST be populated before the app runs.
            // Otherwise the app boots up without having any routes configured, thus rendering you a very empty screen!
            $.ajax("/api/Routes/Angular", { async: false })
                .fail(function (data) { console.error("RomanSPA: FATAL: Failed to load route table from MVC. Inspect body for details");})
                .always(function (data) { console.debug("RomanSPA: Loading routing table"); })
                .done(function (data) { console.debug("RomanSPA: Routing table loaded"); populateRouteTable(data); });
        }])
    .run(['$route', function ($route) {
        if ($route.routes.length > 0) { $route.reload(); }
    }])
    .value('breeze', window.breeze);