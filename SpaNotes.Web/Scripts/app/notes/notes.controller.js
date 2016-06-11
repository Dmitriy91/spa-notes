(function (app_core) {
    'use strict';

    app_core.controller('NotesController', NotesController);

    NotesController.$inject = ['$scope', '$route', '$rootScope', '$location', 'dataService', 'notificationService'];

    function NotesController($scope, $route, $rootScope, $location, dataService, notificationService) {
        $scope.notes = [];
        $scope.removeNote = removeNote;
        activate();

        function loadNotes() {
            dataService.get('/api/notes', null, loadNotesSucceeded, loadNotesFailed)
        }

        function loadNotesSucceeded(response) {
            $scope.notes = response.data;
        }

        function loadNotesFailed(response) {
            notificationService.displayError("Notes haven't been loaded. Please try again later.");
            $location.path('/');
        }

        function removeNote(inx) {
            var noteId = $scope.notes[inx].id;

            dataService.post('/api/notes/delete/' + noteId, null, removeNoteSucceeded, removeNoteFailed)
        }

        function removeNoteFailed(response) {
            notificationService.displayError('Unauthorised action detected.');
        }

        function removeNoteSucceeded(response) {
            $route.reload();
            notificationService.displaySuccess('Note has been successfuly removed.');
        }

        function activate() {
            loadNotes();
        }
    }
})(angular.module('app.core'));
