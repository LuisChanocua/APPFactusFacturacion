using System.ComponentModel.DataAnnotations;

namespace APPFactusFacturacion.Models
{
    public class InvoiceViewModel
    {
        public string ReferenceCode { get; set; }
        public string Observation { get; set; }
        public string PaymentForm { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string PaymentMethodCode { get; set; }
        public BillingPeriodViewModel BillingPeriod { get; set; }
        public CustomerViewModel Customer { get; set; }
        public List<InvoiceItemViewModel> Items { get; set; }
    }

    public class BillingPeriodViewModel
    {
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
    }

    public class CustomerViewModel
    {
        public string Identification { get; set; }
        public string Dv { get; set; }
        public string Company { get; set; } = "";
        public string TradeName { get; set; } = "";
        public string Names { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int LegalOrganizationId { get; set; }
        public int TributeId { get; set; }
        public int IdentificationDocumentId { get; set; }
        public int MunicipalityId { get; set; }
    }

    public class InvoiceItemViewModel
    {
        public string CodeReference { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal Price { get; set; }
        public string TaxRate { get; set; }
        public int UnitMeasureId { get; set; }
        public int StandardCodeId { get; set; }
        public int IsExcluded { get; set; }
        public int TributeId { get; set; }
        public List<WithholdingTaxViewModel> WithholdingTaxes { get; set; } = new List<WithholdingTaxViewModel>();
    }

    public class WithholdingTaxViewModel
    {
        public string Code { get; set; }
        public string WithholdingTaxRate { get; set; }
    }

}
