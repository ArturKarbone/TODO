(function () {
    "use strict";

    angular.module("TODO").controller("createTaskController", ["$scope", "$location", "taskService", function ($scope, $location, taskService) {

        $scope.task = {
            dueDate: new Date()
    }
        $scope.create = function () {
            taskService.createTask($scope.task).then(function (response) {
                $location.path("/home");
            }, function (response) {
                alert("SMTH WENT WRONG");
                console.log(response);
            });
        }

    }]);
}());