namespace APPFactusFacturacion.DTOS.factus_request
{
    public class FactusInvoiceRequestDTO
    {
        public string reference_code { get; set; }
        public string observation { get; set; }
        public string payment_form { get; set; }
        public DateTime payment_due_date { get; set; }
        public string payment_method_code { get; set; }
        public BillingPeriodDTO billing_period { get; set; }
        public CustomerDTO customer { get; set; }
        public List<InvoiceItemDTO> items { get; set; } = new List<InvoiceItemDTO>();
    }

    public class BillingPeriodDTO
    {
        public string start_date { get; set; }
        public string start_time { get; set; }
        public string end_date { get; set; }
        public string end_time { get; set; }
    }

    public class CustomerDTO
    {
        public string identification { get; set; }
        public string dv { get; set; } = "";
        public string company { get; set; } = "";
        public string trade_name { get; set; } = "";
        public string names { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int legal_organization_id { get; set; }
        public int tribute_id { get; set; }
        public int identification_document_id { get; set; }
        public int municipality_id { get; set; }
    }

    public class InvoiceItemDTO
    {
        public string code_reference { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public decimal discount_rate { get; set; }
        public decimal price { get; set; }
        public string tax_rate { get; set; }
        public int unit_measure_id { get; set; }
        public int standard_code_id { get; set; }
        public int is_excluded { get; set; }
        public int tribute_id { get; set; }
        public List<WithholdingTaxDTO> withholding_taxes { get; set; } = new List<WithholdingTaxDTO>();
    }

    public class WithholdingTaxDTO
    {
        public string code { get; set; }
        public string withholding_tax_rate { get; set; }
    }

}
