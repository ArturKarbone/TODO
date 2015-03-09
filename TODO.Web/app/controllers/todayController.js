(function () {
    "use strict";
    angular.module("TODO").controller("todayController", ["$scope", "$route", "taskService", "notificationService", "today", function ($scope, $route, taskSerivce, notificationService, today) {
        
        $scope.createTask = {
            dueDate: new Date()
        };

        $scope.todaysTasks = today;

        $scope.date = new Date();

        $scope.done = function (task) {
            task.done = !task.done;
            taskSerivce.updateTask(task).then(function (response) {
                $scope.todaysTasks.splice($scope.todaysTasks.indexOf(task), 1);
            }, function (response) {
                notificationService.exception(response.data);
            });
        }

        $scope.create = function () {
            $scope.createTask.done = false;
            taskSerivce.createTask($scope.createTask).then(function (response) {
                $route.reload();
            }, function (response) {
                notificationService.exception(response.data);
            });
        }

    }]);

}());