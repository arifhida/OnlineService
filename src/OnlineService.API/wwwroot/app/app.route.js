/// <reference path="../lib-npm/angular/angular.min.js" />
/// <reference path="../lib-npm/angular/angular.js" />
/// <reference path="../lib-npm/angular-ui-router/angular-ui-router.min.js" />
/// <reference path="../lib-npm/angular-ui-router/angular-ui-router.js" />
/// <reference path="../scripts/app.js" />
"use strict";
(function () {
    angular.module('app').config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/");
        $stateProvider
        .state('/', {
            url: "/",
            templateUrl: "app/home/home.html"
        }).state('login', {
            url: "/login",
            templateUrl: "app/login/index.html"
            
        });
        
    });
})();