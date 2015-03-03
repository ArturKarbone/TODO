(function () {
    "use strict";

    angular.module("TODO").factory("taskService", ["$http", function ($http) {

        var serviceBaseUrl = "http://localhost:62565/api/tasks/";
        var bookServiceFactory = {};

        var getAllTasks = function () {
            return $http.get(serviceBaseUrl + "findall").then(function (response) {
                return response;
            });
        }

        var deleteTask = function (Id) {
            return $http.get(serviceBaseUrl + "delete", { params: { id: Id } }).then(function (response) {
                return response;
            });
        }

        var getTask = function (Id) {
            return $http.get(serviceBaseUrl + "findbyid", { params: { id: Id } }).then(function (response) {
                return response;
            });
        }

        var updateTask = function (updatedTask) {
            return $http.post(serviceBaseUrl + "update", updatedTask).then(function (response) {
                return response;
            });
        }

        var createTask = function (newTask) {
            return $http.post(serviceBaseUrl + "create", newTask).then(function (response) {
                return response;
            });
        }

        bookServiceFactory.getAllTasks = getAllTasks;
        bookServiceFactory.getTask = getTask;
        bookServiceFactory.deleteTask = deleteTask;
        bookServiceFactory.updateTask = updateTask;
        bookServiceFactory.createTask = createTask;

        return bookServiceFactory;
    }]);

}());