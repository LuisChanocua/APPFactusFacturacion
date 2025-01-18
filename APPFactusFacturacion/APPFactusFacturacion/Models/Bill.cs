using System;
using System.Collections.Generic;

namespace APPFactusFacturacion.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string BillLink { get; set; } = null!;

    public string UserId { get; set; }
    public virtual ProfileUser ProfileUser { get; set; } = null!;
}
