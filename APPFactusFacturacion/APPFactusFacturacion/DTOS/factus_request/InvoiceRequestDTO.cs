namespace APPFactusFacturacion.DTOS.factus_request
{
    using System.Text.Json.Serialization;

    using System.Text.Json.Serialization;

    public class InvoiceRequestDTO
    {
        [JsonPropertyName("reference_code")]
        public string ReferenceCode { get; set; }

        [JsonPropertyName("observation")]
        public string Observation { get; set; }

        [JsonPropertyName("payment_form")]
        public string PaymentForm { get; set; }

        [JsonPropertyName("payment_due_date")]
        public DateTime PaymentDueDate { get; set; }

        [JsonPropertyName("payment_method_code")]
        public string PaymentMethodCode { get; set; }

        [JsonPropertyName("billing_period")]
        public BillingPeriodDTO BillingPeriod { get; set; }

        [JsonPropertyName("customer")]
        public CustomerDTO Customer { get; set; }

        [JsonPropertyName("items")]
        public List<InvoiceItemDTO> Items { get; set; } = new List<InvoiceItemDTO>();
    }

    public class BillingPeriodDTO
    {
        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        [JsonPropertyName("end_time")]
        public string EndTime { get; set; }
    }

    public class CustomerDTO
    {
        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("dv")]
        public string Dv { get; set; } = ""; // Valor predeterminado

        [JsonPropertyName("company")]
        public string Company { get; set; } = ""; // Valor predeterminado

        [JsonPropertyName("trade_name")]
        public string TradeName { get; set; } = ""; // Valor predeterminado

        [JsonPropertyName("names")]
        public string Names { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("legal_organization_id")]
        public int LegalOrganizationId { get; set; }

        [JsonPropertyName("tribute_id")]
        public int TributeId { get; set; }

        [JsonPropertyName("identification_document_id")]
        public int IdentificationDocumentId { get; set; }

        [JsonPropertyName("municipality_id")]
        public int MunicipalityId { get; set; }
    }

    public class InvoiceItemDTO
    {
        [JsonPropertyName("code_reference")]
        public string CodeReference { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("discount_rate")]
        public decimal DiscountRate { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("tax_rate")]
        public string TaxRate { get; set; }

        [JsonPropertyName("unit_measure_id")]
        public int UnitMeasureId { get; set; }

        [JsonPropertyName("standard_code_id")]
        public int StandardCodeId { get; set; }

        [JsonPropertyName("is_excluded")]
        public int IsExcluded { get; set; }

        [JsonPropertyName("tribute_id")]
        public int TributeId { get; set; }

        [JsonPropertyName("withholding_taxes")]
        public List<WithholdingTaxDTO> WithholdingTaxes { get; set; } = new List<WithholdingTaxDTO>();
    }

    public class WithholdingTaxDTO
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("withholding_tax_rate")]
        public string WithholdingTaxRate { get; set; }
    }

}
