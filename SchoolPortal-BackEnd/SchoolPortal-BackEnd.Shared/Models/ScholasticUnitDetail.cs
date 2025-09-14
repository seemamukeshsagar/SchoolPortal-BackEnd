using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class ScholasticUnitDetail
{
    public int ScohudScholasticId { get; set; }

    public int ScohudUnitId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
