(function () {
    'use strict';
    var controllerId = 'projects';
    angular.module('app').controller(controllerId, ['common', 'datacontext','$location', projects]);

    function projects(common, datacontext, $location) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Projects';
        vm.projects = [];
        activate();

        function activate() {
            var promises = [getProjects()];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Projects View'); });
        }
        function getProjects() {
            return datacontext.getProjects().then(function (data) {
                console.log("projectdata");
                console.log(data);
                return vm.projects = data;
            });
        }

        vm.go = function (path) {
            $location.path(path);
        };

    }
})();