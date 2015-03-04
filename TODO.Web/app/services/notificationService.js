(function () {
    'use strict';

    angular.module('TODO').factory('notificationService', [function () {

        var notificationServiceFactory = {};

        var success = function (message) {
            toastr.success(message);
        };

        var exception = function (error) {
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "newestOnTop": false,
                "progressBar": true,
                "positionClass": "toast-top-center",
                "preventDuplicates": false
            };

            if (error != null) {
                //debugger;
                if (error.modelState != null && error.modelState.validationErrors != null) {
                    var message = "";
                    for (var i = 0; i < error.modelState.validationErrors.length; i++) {
                        message += error.modelState.validationErrors[i] + "<br />";
                    }
                    toastr.error(message, 'Валидация');
                } else {
                    toastr.error(error.message, 'Ошибка');
                }
            } else {
                toastr.error("Неизвестная ошибка.");
            }
        };

        notificationServiceFactory.success = success;
        notificationServiceFactory.exception = exception;

        return notificationServiceFactory;

    }]);
}());