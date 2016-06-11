(function (app_core) {
    'use strict';

    app_core.controller('NotesDetailsController', NotesDetailsController);

    NotesDetailsController.$inject = ['$scope', '$routeParams', '$location', 'dataService', 'notificationService'];

    function NotesDetailsController($scope, $routeParams, $location, dataService, notificationService) {
        $scope.note = {};
        $scope.note.name = '';
        $scope.note.text = '';
        $scope.note.date = '';
        activate();

        function loadNote() {
            dataService.get('/api/notes/details/' + $routeParams.id, null, loadNoteSucceeded, loadNoteFailed);
        }

        function loadNoteSucceeded(response) {
            $scope.note.id = response.data.id;
            $scope.note.name = response.data.name;
            $scope.note.text = response.data.text;
            $scope.note.date = response.data.date;
        }

        function loadNoteFailed(response) {
            notificationService.displayError("Unauthrized actions detected.");
            $location.path('/notes');
        }

        function activate() {
            loadNote();
        }
    }
})(angular.module('app.core'));
