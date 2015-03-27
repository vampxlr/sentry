(function () {
    'use strict';
    var controllerId = 'addProject';
    angular.module('app').controller(controllerId, ['common','$location','$http','$filter', addProject]);

    function addProject(common, $location, $http, $filter) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Add Project';

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Add Project View'); });
        }


        vm.setStartDate = function setStartDate() {

        }

        vm.split = function split() {
            console.log("fire split");
            vm.temp = vm.rosterWorkBreak.split("/");
            vm.RosterWork = vm.temp[0];
            vm.RosterBreak = vm.temp[1];

        }
      

        vm.postProject = function postProject() {
            var startDate = $filter('date')(vm.StartDate, "dd/MM/yyyy");
     

            vm.postData = {
                ProjectName: vm.ProjectName,
                ProjectNumber: vm.ProjectNumber,
                Location: vm.Location,
                RosterWork: vm.RosterWork,
                RosterBreak: vm.RosterBreak,
                Duration: vm.Duration,
                StartDate: startDate
            };

            $http({ method: 'POST', url: '/api/loginapi/postproject', data: vm.postData })
                      .success(function (data, status, headers, config) {
                          console.log(data);
                          log('New Project Added');
                      });

        }



        $(function () {
            $("#datepicker").datepicker();
        });
        vm.go = function (path) {
            $location.path(path);
        };
    }
})();