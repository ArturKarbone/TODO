(function () {
    'use strict';
    angular.module("TODO").controller("archiveController", ["$scope", "$route", "taskService", "notificationService", "tasks", function ($scope, $route, taskSerivce, notificationService, tasks) {

        $scope.createTask = {
            dueDate: new Date()
        };

        $scope.tasks = tasks;

        $scope.delete = function (id) {
            taskSerivce.deleteTask(id).then(function (response) {
                $route.reload();
            }, function (response) {
                notificationService.exception(response.data);
            });
        }

        $scope.done = function (task) {
            task.done = !task.done;
            taskSerivce.updateTask(task).then(function (response) {
                notificationService.success("Updated");
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