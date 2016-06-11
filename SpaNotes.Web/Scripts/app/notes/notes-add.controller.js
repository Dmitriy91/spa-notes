(function (app_core) {
    'use strict';

    app_core.controller('NotesAddController', NotesAddController);

    NotesAddController.$inject = ['$scope', '$rootScope', '$location', 'dataService', 'notificationService'];

    function NotesAddController($scope, $rootScope, $location, dataService, notificationService) {
        $scope.note = {};
        $scope.note.name = '';
        $scope.note.text = '';
        $scope.note.date = '';
        $scope.note.add = addNote;
        activate();

        function addNote() {
            var noteData = {};

            noteData.name = $scope.note.name;
            noteData.text = $scope.note.text;
            noteData.date = $scope.note.date;
            dataService.post('/api/notes/add', noteData, addNoteSucceeded, addNoteFaild);
        }

        function addNoteSucceeded(response) {
            notificationService.displaySuccess("Note has been successfu created.");
            $location.path('/notes');
        }

        function addNoteFaild(response) {
            notificationService.displayError("Unauthorised actions detected.");
        }

        function activate() {
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
