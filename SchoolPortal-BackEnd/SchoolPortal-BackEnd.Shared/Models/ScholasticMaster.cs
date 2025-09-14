using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class ScholasticMaster
{
    public Guid ScholasticId { get; set; }

    public string? ScholasticName { get; set; }

    public string? ScholasticDesc { get; set; }

    public int? ScholasticParentId { get; set; }

    public int? ScholasticSubjectId { get; set; }

    public int ScholasticCmpId { get; set; }

    public int ScholasticSchId { get; set; }

    public string? ScholasticSession { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
