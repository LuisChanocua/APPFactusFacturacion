(function () {
    'use strict';

    angular
        .module('angularApp')
        .service('cloud', cloud);

    cloud.$inject = ['$http'];

    function cloud($http) {
        var service = {

            getReportG,
            downloadReportG
        };

        var baseURL = '/api/';
        var config = { headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' } };
        return service;

        function downloadReportG(FechaInicial, FechaFinal, TipoReporte) {
            return $http(
                {
                    url: baseURL + "dwReport",
                    method: 'POST',
                    params: { "FechaInicial": FechaInicial, "FechaFinal": FechaFinal, TipoReporte },
                    headers: {
                        'Content-type': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
                    },
                    responseType: 'arraybuffer'
                })
                .then(downloadReportGCompleted)
                .catch(downloadReportGFailed);

            function downloadReportGCompleted(response) {
                return response.data;
            }

            function downloadReportGFailed(xhr) {
                console.log(xhr);
            }
        }

        function getReportG(TipoReporte) {
            return $http.get(`${baseURL}getReportG?TipoReporte=${TipoReporte}`)
                .then(getReportGCompleted)
                .catch(getReportGFailed);

            function getReportGCompleted(response) {
                return response.data;
            }

            function getReportGFailed(xhr) {
                console.log(xhr);
            }
        }
    }
})();
