(function () {
    'use strict';
    var controllerId = 'EditProject';
    angular.module('app').controller(controllerId, ['common', '$http', '$routeParams', '$location', '$filter', EditProject]);

    function EditProject(common, $http, $routeParams, $location, $filter) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'EditProject';

        activate();
        vm.projectId = $routeParams.id;
        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated EditProject View'); });
        }

        vm.setStartDate = function setStartDate() {

        }

        vm.split = function split() {
            console.log("fire split");
            vm.temp = vm.rosterWorkBreak.split("/");
            vm.RosterWork = vm.temp[0];
            vm.RosterBreak = vm.temp[1];

        }

       
        vm.GetData = function getProjectById() {
            $http({ method: 'GET', url: '/api/loginapi/getProjectById/' + vm.projectId })
                      .success(function (data, status, headers, config) {
                          vm.ProjectName= data.ProjectName;
                          vm.ProjectNumber= data.ProjectNumber;
                          vm.Location = data.Location;
                          vm.RosterWork= data.RosterWork;
                          vm.RosterBreak=data.RosterBreak;
                          vm.Duration = data.Duration;
                          vm.StartDate = (data.StartDate).substring(0, (data.StartDate).indexOf('T'));
                          vm.joiner();

                      });

        }
        vm.GetData();
        vm.split = function split() {
            console.log("fire split");
            vm.temp = vm.rosterWorkBreak.split("/");
            vm.RosterWork = vm.temp[0];
            vm.RosterBreak = vm.temp[1];

        }
        vm.joiner = function joiner() {
            vm.Join = [vm.RosterWork, vm.RosterBreak];
            vm.rosterWorkBreak = vm.Join.join("/").trim();
            vm.split();
        }

        vm.postProject = function postProject() {
            var startDate = $filter('date')(vm.StartDate, "dd/MM/yyyy");
            vm.postData = {
                Id:vm.projectId,
                ProjectName: vm.ProjectName,
                ProjectNumber: vm.ProjectNumber,
                Location: vm.Location,
                RosterWork: vm.RosterWork,
                RosterBreak: vm.RosterBreak,
                Duration: vm.Duration,
                StartDate: startDate
            };

            $http({ method: 'POST', url: '/api/loginapi/EditProjectById', data: vm.postData })
                      .success(function (data, status, headers, config) {
                          console.log(data);
                          log('Project Data Updated');
                      });

        }



     
        vm.go = function (path) {
            $location.path(path);
        };


    
    }
})();