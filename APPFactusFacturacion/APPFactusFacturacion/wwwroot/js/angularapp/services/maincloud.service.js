(function () {
    'use strict';

    angular
        .module('angularApp')
        .service('cloud', cloud);

    cloud.$inject = ['$http'];

    function cloud($http) {
        var service = {

            getMunicipalities,
            getBills
        };

        var baseURL = '/api/';
        var config = { headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' } };
        return service;

        function getMunicipalities(FechaInicial, FechaFinal, TipoReporte) {
            return $http(
                {
                    url: `${baseURL}getMunicipalities`,
                    method: 'GET',
                    params: {},
                    headers: {
                        'Content-type': 'application/x-www-form-urlencoded; charset=UTF-8'
                    }
                })
                .then(getMunicipalitiesCompleted)
                .catch(getMunicipalitiesFailed);

            function getMunicipalitiesCompleted(response) {
                return response.data;
            }

            function getMunicipalitiesFailed(xhr) {
                console.log(xhr);
            }
        }


        function getBills() {
            return $http(
                {
                    url: `${baseURL}getBills`,
                    method: 'GET',
                    params: {},
                    headers: {
                        'Content-type': 'application/x-www-form-urlencoded; charset=UTF-8'
                    }
                })
                .then(getBillsCompleted)
                .catch(getBillsFailed);

            function getBillsCompleted(response) {
                return response.data;
            }

            function getBillsFailed(xhr) {
                console.log(xhr);
            }
        }

    }
})();
