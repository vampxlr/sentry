(function () {
    'use strict';
    var controllerId = 'InspectionReport';
    angular.module('app').controller(controllerId, ['common', '$http', '$location','$filter', InspectionReport]);

    function InspectionReport(common, $http, $location, $filter) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'InspectionReport';
        vm.predicate = 'ProjectName';
        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated InspectionReport View'); });
        }
        

    
        vm.QueryResults = function QueryResults() {
            var startDate = $filter('date')(vm.startDate, "dd/MM/yyyy");
            var endDate = $filter('date')(vm.endDate, "dd/MM/yyyy");
            vm.postData = {
                startDate: startDate,
                endDate: endDate
            };

            $http({ method: 'POST', url: '/api/loginapi/GetAllResultsByDate', data: vm.postData })
                      .success(function (data, status, headers, config) {
                          console.log(data);
                          vm.Results = data;
                      });



        }


        vm.DownloadExcel = function DownloadExcel() {
            var startDate = $filter('date')(vm.startDate, "dd/MM/yyyy");
            var endDate = $filter('date')(vm.endDate, "dd/MM/yyyy");
            vm.postData = {
                startDate: startDate,
                endDate: endDate
            };

            $http({ method: 'POST', url: '/SPA/ExportToExcelInspectionReport', data: vm.postData })
                      .success(function (data, status, headers, config) {
                          window.location = '/SPA/ExportToExcelInspectionReport/';
                      });



        }

        vm.go = function (path) {
            $location.path(path);
        };

    
    }
})();