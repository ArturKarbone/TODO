(function () {
    "use strict";

    angular.module("TODO").controller("createTaskController", ["$scope", "$location", "$route", "taskService", "notificationService", function ($scope, $location, $route, taskService, notificationService) {
        $scope.day = $route.current.params.day;
        $scope.date = new Date();
        $scope.task = {
            dueDate: new Date()
        }
        $scope.task.dueDate.setDate(+$scope.task.dueDate.getDate() + +$scope.day);

        $scope.create = function () {
            $scope.task.done = false;
            taskService.createTask($scope.task).then(function (response) {
                $location.path("/nextweek");
            }, function (response) {
                notificationService.exception(response.data);
            });
        }

    }]);
}());