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

        var findForToday = function() {
            return $http.get(serviceBaseUrl + "findfortoday").success(function (response) {
                return response;
            });
        }

        var findForNextWeek = function() {
            return $http.get(serviceBaseUrl + "findfornextweek").then(function (response) {
                return response;
            });
        }

        var findDone = function() {
            return $http.get(serviceBaseUrl + "finddone").then(function(response) {
                return response;
            });
        }

        var findUndone = function() {
            return $http.get(serviceBaseUrl + "findundone").then(function(response) {
                return response;
            });
        }

        bookServiceFactory.getAllTasks = getAllTasks;
        bookServiceFactory.getTask = getTask;
        bookServiceFactory.deleteTask = deleteTask;
        bookServiceFactory.updateTask = updateTask;
        bookServiceFactory.createTask = createTask;
        bookServiceFactory.findForToday = findForToday;
        bookServiceFactory.findForNextWeek = findForNextWeek;
        bookServiceFactory.findDone = findDone;
        bookServiceFactory.findUndone = findUndone;

        return bookServiceFactory;
    }]);

}());