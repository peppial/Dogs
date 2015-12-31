var shoplistApp = angular.module('shoplistApp', ['ngResource', 'ngRoute']).config(function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'templates/Index.html',
        controller: 'listController'
    });
    $routeProvider.when('/register', {
        templateUrl: 'templates/Register.html',
        controller: 'userController'
    });
   
});
