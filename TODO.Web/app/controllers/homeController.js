(function () {
    'use strict';
    angular.module("TODO").controller("homeController", ["$scope", "$route", "taskService", "tasks", function ($scope, $route, taskSerivce, tasks) {


        $scope.tasks = tasks;
        //console.log($scope.tasks);
        $scope.delete = function (id) {
            taskSerivce.deleteTask(id).then(function (response) {
                $route.reload();
            }, function (response) {
                //TBD some error displaying
                alert("Smth goes really wrong");
            });
        }
    }]);

}());