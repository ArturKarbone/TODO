(function () {
    'use strict';
    angular.module("TODO").controller("nextWeekController", ["$scope", "$location" , "$route", "taskService", "notificationService", "tasks", function ($scope, $location, $route, taskSerivce, notificationService, tasks) {
        $scope.daysOfWeek = [];

        $scope.date = new Date();
        $scope.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        $scope.daysOfWeek = $scope.days.slice($scope.date.getDay()).concat($scope.days.slice(0, $scope.date.getDay()));

        $scope.createTask = {
            dueDate: new Date()
        };

        $scope.tasksForWeek = [];
        $scope.tasks = tasks;

        if (typeof ($scope.tasks) !== 'string') {
            $scope.tasks.forEach(function (task) {
                var curDate = new Date();
                curDate.setDate(curDate.getDate() + 6);
                var date = new Date(task.dueDate);
                if (date < curDate && !task.done) {
                    var newTask = task;
                    newTask.dueDate = new Date(task.dueDate);
                    $scope.tasksForWeek.push(newTask);
                }
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