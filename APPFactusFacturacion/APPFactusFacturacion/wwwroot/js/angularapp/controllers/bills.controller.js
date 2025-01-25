(function () {
    'use strict';

    angular.module('angularApp').controller('billsController', billsController);
    billsController.$inject = ['$scope', 'cloud', 'exportUiGridService'];

    function billsController($scope, cloud, exportUiGridService) {
        var sp = this;
        sp.lang = 'es';
        sp.getBills = getBills();

        //Grids
        sp.dataGrid = {
            columnDefs: [
                { name: 'FacturaId', field: 'billId' },
                { name: 'UsuarioId', field: 'userId' },
                { name: 'FechaCreacion', field: 'createdAt' },
                { name: 'NombreCliente', field: 'clientName' },
                { name: 'EmailCliente', field: 'clientEmail' },
                //{ name: 'IdFactus', field: 'billIdFactus' },
                //{ name: 'Cufe', field: 'cufeFactus' },
                /*{ name: 'Number', field: 'numberFactus' },*/
                { name: 'ReferenceCode', field: 'referenceCodeFactus' },
                {
                    name: 'Opciones',
                    cellTemplate: `
                    <div class="ui-grid-cell-contents">
                        <button class="btn button-verfactura" ng-click="grid.appScope.verFactura(row.entity)">
                            <i class="fa-solid fa-eye"></i> Ver Factura
                        </button>
                    </div>
                    `,
                    enableSorting: false,
                    width: 150,
                }
            ],
            enableGridMenu: true,
            enableSelectAll: true,
            exporterCsvFilename: `Reporte_Bilss.csv`,
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

        $scope.verFactura = function (row) {
            if (!row || !row.cufeFactus) {
                Swal.fire({
                    title: 'Error',
                    text: 'El CUFE no está disponible para esta factura.',
                    icon: 'error',
                    confirmButtonText: 'Aceptar'
                });
                return;
            }

            var url = `https://catalogo-vpfe-hab.dian.gov.co/User/SearchDocument?DocumentKey=${row.cufeFactus}`;

            Swal.fire({
                title: 'Abrir Factura',
                text: 'Haz clic en "Abrir" para ver los detalles de la factura.',
                icon: 'info',
                showCancelButton: true,
                confirmButtonText: 'Abrir',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.open(url, '_blank');
                }
            });
        };

        //Test de data
        function getBills() {
            cloud.getBills().then(function (data) {
                sp.dataGrid.data = data;
            }).catch(function (error) {
                console.error('Error al cargar las facturas:', error);
            });
        }
    };
})();
