(function () {
    'use strict';
    angular.module("TODO").controller("nextWeekController", ["$scope", "$location" , "$route", "taskService", "notificationService", "nextweek", function ($scope, $location, $route, taskSerivce, notificationService, nextweek) {
        $scope.daysOfWeek = [];

        $scope.date = new Date();
        $scope.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        $scope.daysOfWeek = $scope.days.slice($scope.date.getDay()).concat($scope.days.slice(0, $scope.date.getDay()));

        $scope.createTask = {
            dueDate: new Date()
        };

        $scope.tasksForWeek = [];
        $scope.nextweek = nextweek;

        if ($scope.nextweek.length > 0) {
            $scope.nextweek.forEach(function (task) {
                task.dueDate = new Date(task.dueDate);
                $scope.tasksForWeek.push(task);
            });
        }

        $scope.done = function (task) {
            task.done = !task.done;
            taskSerivce.updateTask(task).then(function (response) {
                $scope.tasksForWeek.splice($scope.tasksForWeek.indexOf(task), 1);
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