using System.ComponentModel.DataAnnotations;

namespace APPFactusFacturacion.DTOS
{
    public class BillsDTO
    {
        public string BillId { get; set; }
        public string UserId { get; set; }
        public string CreatedAt { get; set; }
        public string ReferenceCodeFactus { get; set; }
        public string BillIdFactus { get; set; }
        public string CufeFactus { get; set; }
        public string NumberFactus { get; set; }
        public String ClientName { get; set; }
        public String ClientEmail { get; set; }
        public String ClientPhoneNumber { get; set; }
    }
}
