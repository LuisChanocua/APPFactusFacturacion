(function () {
    'use strict';

    angular.module('angularApp').controller('createbillController', createbillController);

    createbillController.$inject = ['NgTableParams', '$scope', 'cloud'];

    function createbillController(NgTableParams, $scope, cloud) {
        var sp = this;
        sp.lang = 'es';
        sp.getMunicipalities = getMunicipalities();
        $scope.invoice = {
            items: []
        };

        sp.municipalities = [];
        //getMunicipalities();

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

        $scope.addItem();

        function getMunicipalities() {
            cloud.getMunicipalities().then(function (response) {
                sp.municipalities = response.data;
            }).catch(function (error) {
                console.error('Error al cargar municipios:', error);
            });
        }

    }
})();