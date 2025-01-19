namespace APPFactusFacturacion.DTOS.factus_response
{
    public class FactusInvoiceResponseDTO
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; } = new List<string>();
        public InvoiceDataDTO data { get; set; }
    }

    public class InvoiceDataDTO
    {
        public BillDTO bill { get; set; }
    }

    public class BillDTO
    {
        public int id { get; set; }
        public string number { get; set; }
        public string reference_code { get; set; }
        public string cufe { get; set; }
    }
  
}
