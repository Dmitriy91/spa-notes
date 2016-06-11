(function () {
    'use strict';

    angular
        .module('app', ['app.core', 'app.layout'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];

    function config($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/scripts/app/home/index.html',
                controller: 'IndexController'
            })
            .when('/login', {
                templateUrl: '/scripts/app/account/login.html',
                controller: 'LoginController'
            })
            .when('/register', {
                templateUrl: '/scripts/app/account/register.html',
                controller: 'RegisterController'
            })
            .when('/notes', {
                templateUrl: '/scripts/app/notes/notes.html',
                controller: 'NotesController',
                resolve: { isLoggedIn: isLoggedIn }
            })
            .when('/notes/add', {
                templateUrl: '/scripts/app/notes/add.html',
                controller: 'NotesAddController',
                resolve: { isLoggedIn: isLoggedIn }
            })
            .when('/notes/:id', {
                templateUrl: '/scripts/app/notes/details.html',
                controller: 'NotesDetailsController',
                resolve: { isLoggedIn: isLoggedIn }
            })
            .when('/notes/edit/:id', {
                templateUrl: '/scripts/app/notes/edit.html',
                controller: 'NotesEditController',
                resolve: { isLoggedIn: isLoggedIn }
            })
            .otherwise({ redirectTo: '/' });
    }

    run.$inject = ['$rootScope', '$location', '$http', '$window'];

    function run($rootScope, $location, $http, $window) {

    }

    isLoggedIn.$inject = ['membershipService', '$rootScope', '$location'];
     
    function isLoggedIn(membershipService, $rootScope, $location) {
        if (!membershipService.isLoggedIn()) {
            $rootScope.prevLocationPath = $location.path();
            $location.path('/login');
        }
    }
})();
