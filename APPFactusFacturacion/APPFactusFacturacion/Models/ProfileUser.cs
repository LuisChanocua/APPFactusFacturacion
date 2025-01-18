using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace APPFactusFacturacion.Models;

public partial class ProfileUser : IdentityUser
{

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
