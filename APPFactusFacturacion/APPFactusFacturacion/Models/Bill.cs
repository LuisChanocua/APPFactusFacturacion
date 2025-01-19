using System;
using System.Collections.Generic;

namespace APPFactusFacturacion.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public int BillIdFactus { get; set; }

    public string NumberFactus {  get; set; } = null!;

    public string ReferenceCodeFactus {  get; set; } = null!;

    public string CufeFactus {  get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string UserId { get; set; }
    public virtual ProfileUser ProfileUser { get; set; } = null!;
}
