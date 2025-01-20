(function () {
    'use strict';

    angular.module('angularApp').controller('createbillController', createbillController);

    createbillController.$inject = ['NgTableParams', '$scope'];

    function createbillController(NgTableParams, $scope) {
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

            console.log($scope.invoice.items);
            $scope.tableParams.reload();
        };

        $scope.removeItem = function (index) {
            $scope.invoice.items.splice(index, 1);
            $scope.tableParams.reload();
        };

        $scope.submitInvoice = function () {
        };

        $scope.addItem();
    }
})();