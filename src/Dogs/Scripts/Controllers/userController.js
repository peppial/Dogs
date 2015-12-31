angular.module('shoplistApp').controller('userController', ['userService', '$scope', '$location', function (userService, $scope, $location) {
    var vm = this;
    
    
    this.loggedIn = false;
    
    this.login = function () {
        userService.login(vm.user).$promise.then(function (data) {
            vm.loggedIn = true;
            $scope.user.UserId = data.userId;
            $scope.user.Name = data.Name;
        }, function (err) {
            console.log('Error: ', err);
        });
    };

    this.register = function () {
        userService.register(vm.user).$promise.then(function (data) {
            $location.path('/');
            $location.replace();
        }, function (err) {
            console.log('Error: ', err);
        });
    };
}]);