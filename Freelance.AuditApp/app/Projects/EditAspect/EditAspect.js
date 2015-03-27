(function () {
    'use strict';
    var controllerId = 'EditAspect';
    angular.module('app').controller(controllerId, ['common', '$http', '$routeParams', '$location', '$timeout', EditAspect]);

    function EditAspect(common, $http, $routeParams, $location, $timeout) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'EditAspect';
        vm.projectId = $routeParams.id;
        activate();
        
        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated EditAspect View'); });
        }
        
        vm.getProjectById = function getProjectById() {

            $http({ method: 'GET', url: '/api/loginapi/getProjectById/' + vm.projectId }).then(function (result) { vm.Project = result.data; console.log(result.data); });

        }
        vm.getProjectById();
  

        vm.go = function (path) {
            $location.path(path);
        };



        //-----------------------------------------------------------------------------------------------------




        vm.Aspect = [];
     
        var projectId = vm.projectId;



        vm.getAspectByProjectId = function getAspectByProjectId() {

            $http({ method: 'GET', url: '/api/loginapi/getAspectByProjectId/' + vm.projectId }).then(function (result) { vm.Aspect = result.data; selectionProcess(); });
            
        }

        vm.getAspectByProjectId();

     
        function selectionProcess() {
            vm.selectedOptionAspects = vm.Aspect[0];
            console.log("Selection Process");
            console.log(vm.selectedOptionAspects);
        }

       

        function DelayLoad() {

            $timeout(function () { vm.getAspectByProjectId(); log('Aspect Data Updated'); }, 600);

        }





        //-----------------------------------------------------------------------------------------------------------
        //===Add New Aspect Item---------------------------------------------
     
        vm.AddNewAspectItem = function AddNewAspectItem() {
          
            vm.postData = { projectId: vm.projectId, projectAspectId: vm.selectedOptionAspects.ProjectAspectsID, aspectItemName: vm.NewAspectItem };
            $http({ method: 'POST', url: '/api/loginapi/AddNewAspectItem', data: vm.postData })
                .then(
                DelayLoad()
                );
            log('New Aspect Item Added');


        }

        //===End Add New Aspect Item---------------------------------------------

        //===Add New Aspect Item---------------------------------------------

        vm.AddNewAspect = function AddNewAspect() {

            vm.postData = { projectId: vm.projectId, AspectName: vm.AspectName };
            $http({ method: 'POST', url: '/api/loginapi/AddNewAspect', data: vm.postData })
                .then(
                DelayLoad()
                );

            log('New Aspect Added');

        }

        //===End Add New Aspect Item---------------------------------------------
    }
})();