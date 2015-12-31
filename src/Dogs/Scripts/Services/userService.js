angular.module('shoplistApp').factory('userService', ['$resource', function ($resource) {
   
    return $resource('/api/Account', { }, {
        login: {
            url:'/api/Account/Login',
             method: 'POST'
        },
        register: {
                url: '/api/Account/Register',
                method: 'POST'
        },

        delete: {
            
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        }

    });
    }]);

   
        
