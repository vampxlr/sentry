(function () {
    'use strict';
    var controllerId = 'ViewAspect';
    angular.module('app').controller(controllerId, ['common', '$http', '$routeParams', '$location', ViewAspect]);

    function ViewAspect(common, $http, $routeParams, $location) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'ViewAspect';
        vm.id = $routeParams.id;
        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated ViewAspect View'); });
        }
        
        vm.go = function (path) {
            $location.path(path);
        };

        vm.ProjectAspects=[];

        vm.AspectByProjectId= function AspectByProjectId(id) {

            console.log('fire');

            $http({ method: 'GET', url: '/api/loginapi/getAspectByProjectId/' + id })
                .then(function (result) {
                    vm.ProjectAspects = result.data;
                    console.log(vm.ProjectAspects);
                });
           
        }
        vm.AspectByProjectId(vm.id);



    
    }
})();