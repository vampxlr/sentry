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
        $routeProvider.otherwise({ redirectTo: '/UserDasboard' });
    }

    // Define the routes 
    function getRoutes() {
        return [
             {
                 url: '/Inspection',
                config: {
                    title: 'Inspection',
                    templateUrl: 'app/inspection/inspection.html',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-user"></i> Inspection'
                    }
                }
            }
            ,
               {
                   url: '/UserDasboard',
                   config: {
                       title: 'UserDasboard',
                       templateUrl: 'app/UserDasboard/UserDasboard.html',
                       settings: {
                           nav: 1,
                           content: '<i class="fa fa-dashboard"></i> Dasboard'
                       }
                   }
               },





            {
                url: '/inspection/inspecting/:id',
                config: {
                    title: 'inspecting',
                    templateUrl: 'app/inspection/inspecting/inspecting.html'
                }
            }
             , {
                 url: '/inspection/closeaction/:id',
                 config: {
                     title: 'closeaction',
                     templateUrl: 'app/inspection/closeaction/closeaction.html'
                 }
             }

        ];
    }


})();