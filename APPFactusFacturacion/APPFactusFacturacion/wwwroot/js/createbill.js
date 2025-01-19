let productIndex = 0; // Índice inicial para los productos

function addProduct() {
    const container = document.getElementById('product-container');

    // Crear el contenedor del producto
    const productDiv = document.createElement('div');
    productDiv.className = 'product-group';
    productDiv.innerHTML = `
    <fieldset>
        <legend>Producto ${productIndex + 1}</legend>
        <div class="group">
            <input name="Items[${productIndex}].CodeReference" class="input-form" type="text" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__CodeReference" class="label-form">Código del producto</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].Name" class="input-form" type="text" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__Name" class="label-form">Nombre del producto</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].Quantity" class="input-form" type="number" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__Quantity" class="label-form">Cantidad</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].Price" class="input-form" type="number" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__Price" class="label-form">Precio</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].DiscountRate" class="input-form" type="number" step="0.01" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__DiscountRate" class="label-form">Descuento (%)</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].TaxRate" class="input-form" type="text" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__TaxRate" class="label-form">Impuesto (%)</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].UnitMeasureId" class="input-form" type="number" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__UnitMeasureId" class="label-form">ID Unidad de Medida</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].StandardCodeId" class="input-form" type="number" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__StandardCodeId" class="label-form">ID Código Estándar</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].IsExcluded" class="input-form" type="number" min="0" max="1" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__IsExcluded" class="label-form">Exento de impuestos (0 o 1)</label>
        </div>
        <div class="group">
            <input name="Items[${productIndex}].TributeId" class="input-form" type="number" required />
            <span class="highlight"></span><span class="bar"></span>
            <label for="Items_${productIndex}__TributeId" class="label-form">ID Tributo</label>
        </div>
        <button type="button" class="button buttonRed" onclick="removeProduct(this)">Eliminar Producto</button>
    </fieldset>
    `;

    container.appendChild(productDiv);
    productIndex++; // Incrementar el índice para el próximo producto
}

function removeProduct(button) {
    // Eliminar el producto correspondiente
    button.parentElement.remove();
}
