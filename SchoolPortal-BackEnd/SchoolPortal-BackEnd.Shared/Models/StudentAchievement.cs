using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class StudentAchievement
{
    public Guid StudAchId { get; set; }

    public Guid StudStuGuid { get; set; }

    public string? StudAchSchool { get; set; }

    public string? StudAchSession { get; set; }

    public string? StudAchDescription { get; set; }

    public int StudSchId { get; set; }

    public int StudCmpId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
