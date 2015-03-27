(function () {
    'use strict';
    var controllerId = 'inspecting';
    angular.module('app').controller(controllerId, ['common', '$http', '$location', '$routeParams', '$filter', '$timeout', inspecting]);

    function inspecting(common, $http, $location, $routeParams, $filter, $timeout) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId,'success');
      
        var vm = this;
        vm.UserId = UserId;
        vm.title = 'inspecting';
        vm.projectId = $routeParams.id;
        activate();
        vm.username = UserName;
        vm.Aspect = [];
        vm.AspectItemSatisfaction = [];
        vm.AspectItemObservationalComment = [];
        vm.postData = {};
        function activate() {
            vm.postObject = {};
            common.activateController([], controllerId)
                .then(function () { log('Activated inspecting View'); });
        }
        
        vm.getAspectByProjectId = function getAspectByProjectId() {

            $http({ method: 'GET', url: '/api/loginapi/getAspectByProjectId/' + vm.projectId }).then(function (result) { vm.Aspect = result.data; console.log(result.data); });

        }
        vm.getAspectByProjectId();
        
        vm.getProjectById = function getProjectById() {

            $http({ method: 'GET', url: '/api/loginapi/getProjectById/' + vm.projectId }).then(function (result) { vm.Project = result.data; console.log(result.data); });

        }
        vm.getProjectById();
        
     
        vm.postProjectAspects = function postProjectAspects() {
            var AuditDate = $filter('date')(vm.AuditDate, "dd/MM/yyyy");
            vm.postData.ProjectAspects = [];
            vm.Aspect.forEach(function (aspect) {
                aspect.AspectItems.forEach(function (aspectItem) {
                    
                  var  data = {};
                    data["AspectId"] = aspectItem.ProjectAspectID;
                    data["ObservationalComments"] = vm.AspectItemObservationalComment[aspectItem.AspectItemsID];
                    data["Satisfactory"] = vm.AspectItemSatisfaction[aspectItem.AspectItemsID];
                    data["WeatherObservations"] = vm.AuditWeather;
                    data["DateRecorded"] = AuditDate;
                    data["ProjectId"] = vm.projectId;
                    data["Auditee"] = vm.username;
                    
                    vm.postData.ProjectAspects.push(data);
                   
                });
               
            }
                );
          
            console.log(vm.postData);
           
            $http({ method: 'POST', url: '/api/loginapi/SaveAudit/', data: JSON.stringify(vm.postData) }).then(delayChangeRoute());

        };
        vm.disable = false;
        function delayChangeRoute() {
            vm.disable = true;
            log('Inspection Saved');
            $timeout(function () { vm.go('/inspection/') }, 1000);
           
        }

        vm.consoleLog = function consoleLog() {
            console.log(vm);
        }

        vm.AddNewAction = function AddNewAction() {
            console.log("adding new action");
            var newActionDate = $filter('date')(vm.newActionDate, "dd/MM/yyyy");
          var  postData = {
                projectId: vm.projectId,
                description: vm.actionDescription,
                dateTime: newActionDate,
                person: vm.username,
                priority: vm.priority,
                user: vm.username
            }
          vm.SendEMail();
          $http({ method: 'POST', url: '/api/loginapi/AddNewAction/', data: postData }).then(function () { log('New Action Added'); });
        }

        vm.SendEMail = function SendEMail() {
            vm.EmailFrom = vm.username;
            vm.Subject = "A new action has been assigned to you";
            vm.Body = "Action description:" + vm.actionDescription;
         
            var postData = {
                EmailTo: vm.EmailTo,
                EmailFrom: vm.EmailFrom,
                Subject: vm.Subject,
                Body: vm.Body
            }
            $http({ method: 'POST', url: '/api/loginapi/SendEmail/', data: postData }).then(function () { log('Mail Sent to '+vm.EmailTo); });


        }
        
      
        vm.GetUsersByProjectId = function GetUsersByProjectId() {
            $http({ method: 'GET', url: '/api/loginapi/GetUsersByProjectId/' + vm.projectId }).then(function (data) { vm.AssignedUsersList = data.data; console.log(data.data); vm.SelectedUser = vm.AssignedUsersList[1]; });
        }
        vm.GetUsersByProjectId();
        vm.go = function (path) {
            $location.path(path);
        };

    
    }
})();