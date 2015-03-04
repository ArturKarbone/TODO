(function () {
    "use strict";

    angular.module("TODO").controller("editTaskController", ["$scope", "$location", "$route", "taskService", "notificationService", "task", function ($scope, $location, $route, taskService, notificationService, task) {

        $scope.return = $route.current.params.return;

        $scope.task = {
            id: task.id,
            name: task.name,
            done: task.done,
            dueDate: new Date(task.dueDate)
        };

        $scope.update = function () {
            taskService.updateTask($scope.task).then(function (response) {
                if ($scope.return === "today"){
                    $location.path("/today");
                }
                else if ($scope.return === "archive") {
                    $location.path("/archive");
                }
                else if ($scope.return === "nextweek") {
                    $location.path("/nextweek");
                } else {
                    $location.path("/today");
                }
            }, function (response) {
                notificationService.exception(response.data);
            });
        }

    }]);

}());