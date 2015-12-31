    angular.module('shoplistApp').factory('ShopList', ['$resource',
        function ($resource) {
            return $resource('/api/dogsapi/:id', {}, {
                query: { method: 'GET', params: {}, isArray: true },
                
                delete: { method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                     }
            
            }); 
        }
    ]);
