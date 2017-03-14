/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    series = require('stream-series'),
    inject = require('gulp-inject');
    
var webroot = "./wwwroot/";
var ngroutepath = "./node_modules/";
var npmlibPath = webroot + 'lib-npm/';
var npmPaths = {
    angularpath: ngroutepath + 'angular/*.js',
    angularroutepath: ngroutepath + 'angular-ui-router/release/*.js',
    bootstrappath : ngroutepath + 'bootstrap/dist/**/*.*',
    jquerypath: ngroutepath + 'jquery/dist/*.*'
};
gulp.task('copy-npm', function () {
    gulp.src(npmPaths.angularpath).pipe(
        gulp.dest(npmlibPath + 'angular/'));
    gulp.src(npmPaths.angularroutepath).pipe(
        gulp.dest(npmlibPath + 'angular-ui-router/'));
    gulp.src(npmPaths.bootstrappath).pipe(
        gulp.dest(npmlibPath +'bootstrap/'));
    gulp.src(npmPaths.jquerypath).pipe(
        gulp.dest(npmlibPath + 'jquery/'));
    
});
gulp.task('default', function () {
    var stylelibPath = gulp.src(npmlibPath + '**/*.css', { read: false });
    var stylePath = gulp.src(webroot + 'styles/*.css', { read: false });
    var jqPath = gulp.src(npmlibPath + 'jquery/*.min.js', { read: false });
    var bstPath = gulp.src(npmlibPath + 'bootstrap/js/*.js', { read: false });
    var angularSrc = gulp.src(npmlibPath + 'angular/*.min.js', { read: false });
    var routerSrc = gulp.src(npmlibPath + 'angular-ui-router/*.min.js', { read: false });
    var ngScriptSrc = gulp.src(webroot + 'scripts/*.js', { read: false });
    var ngRouteSrc = gulp.src(webroot + 'app/**/*.route.js', { read: false });
    gulp.src(webroot + 'app/Index.html')
    .pipe(inject(series(stylelibPath, stylePath, jqPath, bstPath, angularSrc, routerSrc,
        ngScriptSrc, ngRouteSrc),
        { ignorePath: '/wwwroot' }))
    .pipe(gulp.dest(webroot + 'app'));
});