using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APPFactusFacturacion.Models;

public partial class Bill
{
    public int BillId { get; set; }

    [Required]
    [StringLength(100)]
    public String ClientName { get; set; }

    [Required]
    [StringLength(100)]
    public String ClientEmail { get; set; }

    [Required]
    [StringLength(100)]
    public String ClientPhoneNumber { get; set; }

    public int BillIdFactus { get; set; }

    public string NumberFactus {  get; set; } = null!;

    public string ReferenceCodeFactus {  get; set; } = null!;

    public string CufeFactus {  get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string UserId { get; set; }
    public virtual ProfileUser ProfileUser { get; set; } = null!;
}
