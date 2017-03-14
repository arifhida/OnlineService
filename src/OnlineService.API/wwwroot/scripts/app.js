/// <reference path="../lib-npm/angular/angular.min.js" />
/// <reference path="../lib-npm/angular/angular.js" />

"use strict";
(function () {
    angular.module('authentication', []);
    angular.module('app', ['ui.router', 'authentication']);
})();