(function () {
    'use strict';
    var controllerId = 'UserDasboard';
    angular.module('app').controller(controllerId, ['common', 'datacontext', '$http', UserDasboard]);

    function UserDasboard(common, datacontext, $http) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        
        var vm = this;
        vm.userId = UserId;
   
        vm.title = 'UserDasboard';

        activate();

        function activate() {
            var promises = [];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Dashboard View'); });
        }

        function AssignedProjectsByUserId() {

            return $http({ method: 'GET', url: '/api/loginapi/AssignedProjectsByID/' + vm.userId });

        }


        vm.AssignedProjectsByUserId = function () {
            AssignedProjectsByUserId().then(function (result) {
                console.log("assignedprojets");
                console.log(result.data);
                return vm.assignedprojects = result.data;
            });
        }
       vm.AssignedProjectsByUserId();
      

       vm.GetAllResultsByUserId = function() {
           GetAllResultsByUserId().then(function (result) {
               console.log("GetAllResultsByUserId");
               console.log(result.data);
               return vm.ResultsForUser = result.data;
           });

       }
       function GetAllResultsByUserId() {

           return $http({ method: 'GET', url: '/api/loginapi/GetAllResultsByUserId/' + vm.userId });

       }
       vm.GetAllResultsByUserId();

    }
})();