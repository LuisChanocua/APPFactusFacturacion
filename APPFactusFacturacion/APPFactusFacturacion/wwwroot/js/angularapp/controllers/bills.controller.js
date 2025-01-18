(function () {
    'use strict';

    angular.module('angularApp').controller('billsController', billsController);
    billsController.$inject = ['$scope', 'cloud', 'exportUiGridService'];

    function billsController($scope, cloud, exportUiGridService) {
        var sp = this;
        sp.lang = 'es';
        sp.getReportG = getReportG;
        sp.dowloading = false;
        sp.downloadReport = downloadReport;

        var hoy = new Date();
        sp.FechaInicial = formatDate(hoy);
        sp.fechaFinal = formatDate(new Date(hoy.setMonth(hoy.getMonth() + 1)));

        var fInicial = '';
        var fFinal = '';

        var DateInicial = '';
        var DateFinal = '';

        $scope.$watch('fechaInicial', function (value) {
            try {
                DateInicial = new Date(value);
                fInicial = value;
            } catch (e) { }
            if (!DateInicial) {
                fInicial = '';
                $scope.error = "This is not a valid date";
            } else {
                $scope.error = false;
            }
        });

        $scope.$watch('fechaFinal', function (value) {
            try {
                DateFinal = new Date(value);
                fFinal = value;
            } catch (e) { }
            if (!DateFinal) {
                fFinal = '';
                $scope.error = "This is not a valid date";
            } else {
                $scope.error = false;
            }
        });

        $scope.verFactura = function (row) {
            //feat: cargar factura de la DIAN

        };

        //Grids
        sp.dataGrid = {
            columnDefs: [
                { name: 'UsuarioId', field: 'ID' },
                { name: 'Email', field: 'EMAIL' },
                {
                    name: 'Opciones',
                    cellTemplate: `
                <div class="ui-grid-cell-contents">
                    <button class="btn" ng-click="grid.appScope.verFactura(row.entity.EMAIL)"><i class="fa-solid fa-eye"></i></button>
                </div>
                `,
                    enableSorting: false,
                    width: 150,
                    height: 50
                },
                
            ],
            enableGridMenu: true,
            enableSelectAll: true,
            exporterCsvFilename: `Reporte_Usuarios_${fInicial}_${fFinal}.csv`,
            exporterMenuPdf: false, // ADD THIS
            exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
            }, gridMenuCustomItems: [{
                title: 'Descargar reporte EXCEL',
                action: function ($event) {
                    exportUiGridService.exportToExcel('Sheet 1', $scope.gridApi, 'all', 'all');
                },
                order: 110
            }
            ]
        };

        function getReportG() {
            sp.dataGrid.data = [
                { ID: 1, EMAIL: 'juan.perez@example.com' },
                { ID: 2, EMAIL: 'ana.gomez@example.com' },
                { ID: 3, EMAIL: 'carlos.lopez@example.com' }
            ];
        }

        function formatDate(date) {
            var d = new Date(date),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2)
                month = '0' + month;
            if (day.length < 2)
                day = '0' + day;

            return [year, month, day].join('-');
        }
        function downloadReport() {
            sp.dowloading = true;
            return cloud.downloadReportG(fInicial, fFinal, "1").then(function (data) {
                sp.dowloading = false;
                // TODO when WS success
                var file = new Blob([data], {
                    type: 'application/csv'
                });
                //trick to download store a file having its URL
                var fileURL = URL.createObjectURL(file);
                var a = document.createElement('a');
                a.href = fileURL;
                a.target = '_blank';
                a.download = `Reporte_Usuarios_${fInicial}_${fFinal}.xlsx`;
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
            });
        }


        //Test de data
        getReportG();
    };
})();
