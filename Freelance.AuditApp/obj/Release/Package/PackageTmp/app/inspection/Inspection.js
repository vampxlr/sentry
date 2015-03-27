(function () {
    'use strict';
    var controllerId = 'inspection';
    angular.module('app').controller(controllerId, ['common', '$http', '$location', 'datacontext', inspection]);

    function inspection(common, $http, $location, datacontext) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Inspection';
        vm.projects = [];
        activate();
        vm.userId = UserId;
        console.log(UserId);
        function activate() {
            var promises = [];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Inspection View'); });
        }
      
        //function getProjects() {
        //    return AssignedProjectsByUserId().then(function (data) {
        //        console.log("projectdata");
        //        console.log(data);
        //        return vm.projects = data;
               
        //    });
        //}
        AssignedProjectsByUserId();
        function AssignedProjectsByUserId() {

            return $http({ method: 'GET', url: '/api/loginapi/AssignedProjectsByID/' + vm.userId }).then(function (data) {
                vm.projects = data.data;
                console.log(data.data);
            });

        }

        vm.SelectedProject = vm.projects[1];
       
        vm.go = function (path) {
            $location.path(path + vm.SelectedProject.Id);
        };


    
    }
})();