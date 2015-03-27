(function () {
    'use strict';
    var controllerId = 'addUser';
    angular.module('app').controller(controllerId, ['common','$http','$location', addUser]);

    function addUser(common, $http,$location) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Add User';
    
        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Add User View'); });
        }
        
        vm.RoleOptions = [
                         { Role: 'Administrator' },
                         { Role: 'Analyst' },
                         { Role: 'User' }
        ];
        vm.UserRole = vm.RoleOptions[2];
    
        vm.postUser = function postPeople() {
            
           

            vm.postData = { Username: vm.Username, UserPassword: vm.UserPassword, UserRole: vm.UserRole.Role };


            $http({ method: 'POST', url: '/api/loginapi/postpeople', data: vm.postData })
                      .success(function (data, status, headers, config) {
                          log('Add New User '+vm.Username); 
                          console.log(data);
                      });

        }

      

        vm.go = function (path) {
            $location.path(path);
        };

    
    }
})();