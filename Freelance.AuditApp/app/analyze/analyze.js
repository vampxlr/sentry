(function () {
    'use strict';
    var controllerId = 'analyze';
    angular.module('app').controller(controllerId, ['common', '$http', '$location', '$filter', analyze]);

    function analyze(common, $http, $location, $filter) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Analyze';
    
        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated Analyze View'); });
        }
        
     
    
        vm.GetActionReport = function GetActionReport() {

            $http({ method: 'GET', url: '/api/loginapi/ActionReport'})
               .success(function (data, status, headers, config) {
                   console.log(data);
                   vm.ActionReport = data;
               });

        }
        vm.GetActionReport();
        vm.QueryActions = function QueryActions() {
            var startDate = $filter('date')(vm.startDate, "dd/MM/yyyy");
           var endDate = $filter('date')(vm.endDate, "dd/MM/yyyy");
            vm.postData = {
                startDate: startDate,
                endDate: endDate
            };

            $http({ method: 'POST', url: '/api/loginapi/QueryActions', data: vm.postData })
                      .success(function (data, status, headers, config) {
                          console.log(data);
                          vm.Actions = data;
                      });



        }

        vm.DownloadExcel = function DownloadExcel() {
            var startDate = $filter('date')(vm.startDate, "dd/MM/yyyy");
            var endDate = $filter('date')(vm.endDate, "dd/MM/yyyy");
            vm.postData = {
                startDate: startDate,
                endDate: endDate
            };

            $http({ method: 'POST', url: '/SPA/ExportToExcelActionReport', data: vm.postData })
                      .success(function (data, status, headers, config) {
                          window.location = '/SPA/ExportToExcelActionReport/';
                      });



        }

        vm.go = function (path) {
            $location.path(path);
        };

    
    }
})();