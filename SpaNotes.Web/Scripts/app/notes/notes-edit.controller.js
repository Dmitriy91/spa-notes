(function (app_core) {
    'use strict';

    app_core.controller('NotesEditController', NotesEditController);

    NotesEditController.$inject = ['$scope', '$routeParams', '$location', 'dataService', 'notificationService'];

    function NotesEditController($scope, $routeParams, $location, dataService, notificationService) {
        $scope.note = {};
        $scope.note.name = '';
        $scope.note.text = '';
        $scope.note.date = '';
        $scope.note.update = updateNote;
        activate();

        function updateNote() {
            var noteData = {};

            noteData.id = $routeParams.id;
            noteData.name = $scope.note.name;
            noteData.text = $scope.note.text;
            noteData.date = $scope.note.date;
            dataService.post('/api/notes/update/' + noteData.id, noteData, updateNoteSucceeded, updateNoteFaild);
        }

        function updateNoteSucceeded(response) {
            notificationService.displaySuccess("Note has been successfu updated.");
            $location.path('/notes');
        }

        function updateNoteFaild(response) {
            notificationService.displayError("Unauthorised actions detected.");
        }

        function loadNote() {
            dataService.get('/api/notes/details/' + $routeParams.id, null, loadNoteSucceeded, loadNoteFailed);
        }

        function loadNoteSucceeded(response) {
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
            jQuery(".datepicker").datepicker({
                forceParse: true,
                format: "yyyy-mm-dd",
                todayBtn: "linked",
                todayHighlight: true,
                daysOfWeekHighlighted: "0,6",
                calendarWeeks: true,
                weekStart: 1,
                todayHighlight: true,
                autoclose: true
            });
        }
    }
})(angular.module('app.core'));
