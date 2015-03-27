(function () {
    'use strict';
    var controllerId = 'CloseAction';
    angular.module('app').controller(controllerId, ['common', '$http', '$location', '$routeParams', '$timeout', '$filter', CloseAction]);

    function CloseAction(common, $http, $location, $routeParams, $timeout,$filter) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        var vm = this;
        vm.title = 'Close Action';
        vm.projectId = $routeParams.id;
        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () { log('Activated CloseAction View'); });
        }
        
        vm.closeUserAction = function closeUserAction(actionid) {
            var dateClosed = $filter('date')(vm.dateClosed, "dd/MM/yyyy");
            vm.actionId = actionid;
            var postData = {
                actionid: actionid,
                description:vm.description,
                dateClosed:dateClosed
            }
            vm.SendEMail();
            log('Action Closed');
           $http({ method: 'POST', url: '/api/loginapi/CloseUserAction/', data: postData }).then(DelayLoad());


        }
        function DelayLoad() {

            $timeout(function () { vm.getActions() }, 600);
        }


        vm.getActions = function getActions() {

            $http({ method: 'GET', url: '/api/loginapi/GetCloseUserActions/' + vm.projectId }).then(function (result) { vm.Actions = result.data; console.log(result.data); });

        }
        vm.getActions();

        vm.SendEMail = function SendEMail() {
            vm.EmailFrom = "";
            vm.Subject = "An Action has been closed";
            vm.Body = "Action Id:" + vm.actionId + " Description:" + vm.description;

            var postData = {
                EmailTo: vm.EmailTo,
                EmailFrom: vm.EmailFrom,
                Subject: vm.Subject,
                Body: vm.Body
            }
            $http({ method: 'POST', url: '/api/loginapi/SendEmail/', data: postData }).then(function () { log('Mail Sent to ' + vm.EmailTo); });


        }





        vm.go = function (path) {
            $location.path(path);
        };

    
    }
})();