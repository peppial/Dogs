angular.module('shoplistApp').controller('listController', ['$scope', 'ShopList', function listController($scope, ShopList) {
     
    if ($scope.user == null)
        $scope.user = {
            Email: '', Password: '', Name: 'Peppi', UserId: 'peppi'
        };
    $scope.$watch('user.UserId',
    function () {
        if($scope.user.UserId!='')
            $scope.List = ShopList.query({ id: $scope.user.UserId });
    }
);
    $scope.clear = function (listid) {
        ShopList.delete({ id: listid }).$promise.then(function (response) {
            $scope.List = ShopList.query({ id: $scope.user.UserId });
        });

    };
    $scope.add = function (text) {
        ShopList.save({ Name: text, UserId: $scope.user.UserId }).$promise.then(function (response) {
            $scope.text = "";
            $scope.List = ShopList.query({ id: $scope.user.UserId });
        });

    };
}]);