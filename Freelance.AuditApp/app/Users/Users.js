(function () {
    
    var controllerId = 'Users';
    angular.module('app').controller(controllerId, ['common', 'datacontext','$http','$scope', Users]);

    function Users(common, datacontext, $http,$scope) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        $scope.som = [];
        var vm = this;
        vm.title = 'Users';
        vm.users = [];
        vm.projects = [];
        vm.UserProjects = [];
        activate();
        vm.customModel = [];
        vm.getUnassignedProjectsByUserId =[];
        vm.projectData = [];
        vm.UserProjectData = [];
        vm.friends = [{id:4},{id:5}];

        function activate() {
            var promises = [getUserProjects(), getProjects(), getUsers()];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Users View'); });
        }
     
        function getUserProjects() {
            return datacontext.getUserProjects().then(function (data) {
               
                return vm.UserProjects = data;
            });
        }

       
        function getProjects(){
            return datacontext.getProjects().then(function (data) {
              
                return vm.projects = data;
            });
        
        }
    
        function getUsers() {
            return datacontext.getUsers().then(function (data) {
                return vm.users = data;
            });
        }
        //customAction();
        //function customAction(){

        //    var successProjects = function (data) {
        //        vm.projectData = data;
               

        //        var successBoth = function (data) {
        //            vm.UserProjectData = data;
        //            // reference
        //            // vm.projectData
        //            // vm.UserProjectData
        //            var projectData = vm.projectData;
        //            var UserProjectData = vm.UserProjectData;

        //            //final place for processing
        //            //console.log(projectData);
        //            for (var x in projectData) {
        //                //console.log('projectName' +' = '+ projectData[x]['projectName']);
        //                //console.log('projectId' +' = ' + projectData[x]['projectId']);
        //            }
        //            //end of final place of work
        //        }

        //        getUserProjects().then(successBoth);
        //    }
        //    getProjects().then(successProjects);

            
            
        //}

        //function postandGetUserProjects() {
        //    return datacontext.postandGetUserProjects().then(function (data) {

        //        return data;
        //    });
        //}
        //var GetProjects = {}
        
        // vm.userIdArray = [4, 5, 8,9];
        // vm.userIdArray.forEach(function (entry) {
        //     vm.getUnassignedProjectsByUserId.entry = entry+2;

        //    console.log(vm.getUnassignedProjectsByUserId[entry]);
        //});
        //GetProjects[UserId] = 42;

        //var UserId = 0;
        
        $scope.$watch('users', function (n, o) {
            vm.users = n;

        });
        vm.go = function (path) {
            $location.path(path);
        };
    }
})();