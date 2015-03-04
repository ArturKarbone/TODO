(function () {
    var app = angular.module("TODO", ["ngRoute", "ngMessages"]);

    app.config(function ($routeProvider) {

        $routeProvider.when("/createtask", {
            controller: "createTaskController",
            templateUrl: "app/templates/createtask.html"
        });

        $routeProvider.when("/edittask", {
            controller: "editTaskController",
            templateUrl: "app/templates/edittask.html",
            resolve: {
                task: function ($route, taskService) {
                    return taskService.getTask($route.current.params.id).then(function (response) {
                        return response.data;
                    });
                }
            }
        });

        $routeProvider.when("/archive", {
            controller: "archiveController",
            templateUrl: "app/templates/archive.html",
            resolve: {
                tasks: function (taskService) {
                    return taskService.getAllTasks().then(function (response) {
                        return response.data;
                    });
                }
            }
        });

        $routeProvider.when("/today", {
            controller: "todayController",
            templateUrl: "app/templates/today.html",
            resolve: {
                tasks: function (taskService) {
                    return taskService.getAllTasks().then(function (response) {
                        return response.data;
                    });
                }
            }
        });

        $routeProvider.when("/nextweek", {
            controller: "nextWeekController",
            templateUrl: "app/templates/nextweek.html",
            resolve: {
                tasks: function (taskService) {
                    return taskService.getAllTasks().then(function (response) {
                        return response.data;
                    });
                }
            }
        });

        $routeProvider.otherwise({ redirectTo: "/today" });

    });
}());