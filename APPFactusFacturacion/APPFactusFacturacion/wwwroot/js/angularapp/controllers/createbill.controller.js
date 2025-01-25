(function () {
    'use strict';

    angular.module('angularApp').controller('createbillController', createbillController);

    createbillController.$inject = ['NgTableParams', '$scope', 'cloud', '$http'];

    function createbillController(NgTableParams, $scope, cloud, $http) {
        var sp = this;
        sp.lang = 'es';
        sp.getMunicipalities = getMunicipalities();
        sp.municipalities = [];

        $scope.invoice = {
            items: []
        };


        $scope.tableParams = new NgTableParams({}, { dataset: $scope.invoice.items });

        $scope.addItem = function () {
            $scope.invoice.items.push({
                code_reference: '',
                name: '',
                quantity: 1,
                price: 0,
                discount_rate: 0,
                tax_rate: '',
                unit_measure_id: '',
                standard_code_id: '',
                is_excluded: '',
                tribute_id: ''
            });
            $scope.tableParams.reload();
        };

        $scope.removeItem = function (index) {
            $scope.invoice.items.splice(index, 1);
            $scope.tableParams.reload();
        };

        $scope.submitInvoice = function () {
        };

        /*$scope.addItem();*/

        $scope.submitBill = function () {

            const antiForgeryToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

            $http({
                method: 'POST',
                url: '/Factus/CreateBill',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': antiForgeryToken
                },
                data: $scope.formData
            }).then(function (response) {
                if (response.data.success) {
                    Swal.fire({
                        title: '¡Éxito!',
                        text: response.data.message,
                        icon: 'success',
                        confirmButtonText: 'Aceptar'
                    });

                    $scope.formData = {};
                } else {
                    Swal.fire({
                        title: 'Error',
                        text: response.data.message || 'No se pudo crear la factura.',
                        icon: 'error',
                        confirmButtonText: 'Aceptar'
                    });
                }
            }).catch(function (error) {
                console.error('Error al crear la factura:', error);
                Swal.fire({
                    title: 'Error',
                    text: 'Ocurrió un error al intentar crear la factura.',
                    icon: 'error',
                    confirmButtonText: 'Aceptar'
                });
            });
        };

        function getMunicipalities() {
            cloud.getMunicipalities().then(function (response) {
                sp.municipalities = response.data;
            }).catch(function (error) {
                console.error('Error al cargar municipios:', error);
            });
        }

    }
})();