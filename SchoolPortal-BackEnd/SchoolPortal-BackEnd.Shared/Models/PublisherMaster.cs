﻿using System;
using System.Collections.Generic;

namespace SchoolPortal.Shared.Models;

public partial class PublisherMaster
{
    public Guid PublisherId { get; set; }

    public string? PublisherName { get; set; }

    public string? PublisherDescription { get; set; }

    public string? PublisherAddress { get; set; }

    public string? PublisherCity { get; set; }

    public string? PublisherState { get; set; }

    public string? PublisherCountry { get; set; }

    public string? PublisherZipcode { get; set; }

    public string? PublisherPhone { get; set; }

    public string? PublisherMobile { get; set; }

    public string? PublisherEmail { get; set; }

    public int? PublisherCityId { get; set; }

    public int? PublisherStateId { get; set; }

    public bool PublisherIsActive { get; set; }

    public int PublisherCmpId { get; set; }

    public int PublisherSchId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
