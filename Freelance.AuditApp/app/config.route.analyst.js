(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', getRoutes());

    // Configure the routes and route resolvers
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {

        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/Analyze' });
    }

    // Define the routes 
    function getRoutes() {
        return [
            {
                url: '/Analyze',
                config: {
                    templateUrl: 'app/analyze/analyze.html',
                    title: 'ActionReport',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Action Report'
                    }
                }
            },
              {
                  url: '/InspectionReport',
                  config: {
                      templateUrl: 'app/InspectionReport/InspectionReport.html',
                      title: 'InspectionReport',
                      settings: {
                          nav: 2,
                          content: '<i class="fa fa-dashboard"></i> Inspection Report'
                      }
                  }
              }
            

        ];
    }


})();