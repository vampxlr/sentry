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
        $routeProvider.otherwise({ redirectTo: '/' });
    }

    // Define the routes 
    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: 'app/dashboard/dashboard.html',
                    title: 'dashboard',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Dashboard'
                    }
                }
            }, {
                url: '/Users',
                config: {
                    title: 'Users',
                    templateUrl: 'app/users/users.html',
                    settings: {
                        nav: 4,
                        content: '<i class="fa fa-user"></i> List of Users'
                    }
                }
            }
            , {
                url: '/Projects',
                config: {
                    title: 'Project',
                    templateUrl: 'app/Projects/Projects.html',
                    settings: {
                        nav: 5,
                        content: '<i class="fa fa-calendar"></i>List of Projects'
                    }
                }
            }, {
                url: '/addUser',
                config: {
                    title: 'Add Users',
                    templateUrl: 'app/addUser/addUser.html',
                    settings: {
                        nav: 6,
                        content: '<i class="fa fa-user"></i> Add New User'
                    }
                }
            }
            , {
                url: '/addProject',
                config: {
                    title: 'Add Projects',
                    templateUrl: 'app/addProject/addProject.html',
                    settings: {
                        nav: 7,
                        content: '<i class="fa fa-calendar"></i> Add New Project'
                    }
                }
            },
            {
                url: '/Projects/ViewAspect/:id',
                config: {
                    title: 'ViewAspect',
                    templateUrl: 'app/Projects/ViewAspect/ViewAspect.html'
                }
            },
            {
                url: '/Projects/EditAspect/:id',
                config: {
                    title: 'EditAspect',
                    templateUrl: 'app/Projects/EditAspect/EditAspect.html'
                }
            },
            {
                url: '/Projects/EditProject/:id',
                config: {
                    title: 'EditProject',
                    templateUrl: 'app/Projects/EditProject/EditProject.html'
                }
            },
             {
                 url: '/Analyze',
                 config: {
                     templateUrl: 'app/analyze/analyze.html',
                     title: 'Analyze',
                     settings: {
                         nav: 2,
                         content: '<i class="fa fa-dashboard"></i> Action Report'
                     }
                 }
             },
              {
                  url: '/InspectionReport',
                  config: {
                      templateUrl: 'app/InspectionReport/InspectionReport.html',
                      title: 'Inspection Report',
                      settings: {
                          nav: 3,
                          content: '<i class="fa fa-dashboard"></i> Inspection Report'
                      }
                  }
              }

        ];
    }


})();