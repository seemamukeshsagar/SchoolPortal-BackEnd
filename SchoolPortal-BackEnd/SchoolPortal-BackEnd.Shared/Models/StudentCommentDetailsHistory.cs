using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class StudentCommentDetailsHistory
{
    public Guid StucommenthId { get; set; }

    public int StucommenthStucommentId { get; set; }

    public Guid StucommenthStuGuid { get; set; }

    public int StucommenthCmId { get; set; }

    public int StucommenthSecId { get; set; }

    public string? StucommenthSession { get; set; }

    public string? StucommenthQ1Desc { get; set; }

    public string? StucommenthQ2Desc { get; set; }

    public string? StucommenthQ3Desc { get; set; }

    public string? StucommenthSa1Desc { get; set; }

    public string? StucommenthSa2Desc { get; set; }

    public string? StucommenthFinalDesc { get; set; }

    public bool? StucommenthIsAactive { get; set; }

    public int StucommenthCmpId { get; set; }

    public int StucommenthSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
