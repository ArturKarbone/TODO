(function () {
    "use strict";

    angular.module("TODO").controller("editTaskController", ["$scope", "$location", "taskService", "task", function ($scope, $location, taskService, task) {

        $scope.task = {
            id: task.id,
            name: task.name,
            done: task.done,
            dueDate: new Date(task.dueDate)
        };

        $scope.update = function () {
            taskService.updateTask($scope.task).then(function (response) {
                $location.path("/home");
            }, function (response) {
                //TDB some error messagin here
                alert("Smth goes really wrong");
                console.log(response);
            });
        }

    }]);

}());