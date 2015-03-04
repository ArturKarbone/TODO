(function () {
    "use strict";
    angular.module("TODO").controller("todayController", ["$scope", "$route", "taskService", "notificationService", "tasks", function ($scope, $route, taskSerivce, notificationService, tasks) {
        
        $scope.createTask = {
            dueDate: new Date()
        };

        $scope.todaysTasks = [];


        $scope.tasks = tasks;
        console.log($scope.tasks);
        if (typeof ($scope.tasks) !== 'string') {
            $scope.tasks.forEach(function (task) {
                var curDate = new Date();
                var date = new Date(task.dueDate);
                if (date.getDate() === curDate.getDate() && !task.done) {
                    $scope.todaysTasks.push(task);
                }
            });
        }

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