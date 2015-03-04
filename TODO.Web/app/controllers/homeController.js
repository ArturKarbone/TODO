(function () {
    'use strict';
    angular.module("TODO").controller("homeController", ["$scope", "$route", "taskService", "tasks", function ($scope, $route, taskSerivce, tasks) {
        $scope.all = true;
        $scope.todays = false;
        $scope.nweeks = false;

        $scope.todaysTasks = [];
        $scope.tasksForWeek = [];
        $scope.daysOfWeek = [];
        $scope.tasks = tasks;
        console.log($scope.tasks);
        $scope.tasks.forEach(function (task) {
            var curDate = new Date();
            var date = new Date(task.dueDate);
            if (date.getDate() === curDate.getDate()) {
                console.log(task);
                $scope.todaysTasks.push(task);
            }
        });
        $scope.date = new Date();
        $scope.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        $scope.date1 = $scope.days[$scope.date.getDay()];
        $scope.date2 = $scope.days[($scope.date.getDay() + 6) % 7];
        $scope.daysOfWeek = $scope.days.slice($scope.date.getDay()).concat($scope.days.slice(0, $scope.date.getDay()));

        $scope.tasks.forEach(function (task) {
            var curDate = new Date();
            curDate.setDate(curDate.getDate() + 6);
            var date = new Date(task.dueDate);
            if (date < curDate) {
                console.log(task);
                var newTask = task;
                newTask.dueDate = new Date(task.dueDate);
                $scope.tasksForWeek.push(newTask);
            }
        });
        console.log($scope.tasksForWeek);
        console.log($scope.daysOfWeek);
        //console.log($scope.tasks);
        $scope.delete = function (id) {
            taskSerivce.deleteTask(id).then(function (response) {
                $route.reload();
            }, function (response) {
                //TBD some error displaying
                alert("Smth goes really wrong");
                console.log(response);
            });
        }

        $scope.showall = function() {
            $scope.all = true;
            $scope.todays = false;
            $scope.nweeks = false;
        }

        $scope.showtoday = function() {
            $scope.all = false;
            $scope.todays = true;
            $scope.nweeks = false;
        }

        $scope.shownextweek = function() {
            $scope.all = false;
            $scope.todays = false;
            $scope.nweeks = true;
        }
    }]);

}());