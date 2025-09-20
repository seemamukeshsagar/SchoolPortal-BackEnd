using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[PrimaryKey("STUH_GUID", "STUH_SESSION")]
[Table("StudentMasterHistory")]
public partial class StudentMasterHistory
{
    [Key]
    public Guid STUH_GUID { get; set; }

    public int STUH_ROLL_NUMBER { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string STUH_FIRST_NAME { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string STUH_LAST_NAME { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string STUH_ADDRESS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_CITY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_STATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_ZIP_CODE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_COUNTRY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_CONTACT_NUMBER { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_EMERGENCY_CONTACT_NUMBER { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime STUH_DOB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime STUH_DOJ { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string STUH_REGSTRATION_NUMBER { get; set; }

    public int STUH_CM_ID { get; set; }

    public int STUH_SEC_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_HOUSE_ALLOTTED { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_TRANSPORT { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string STUH_IMAGE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_EMAIL { get; set; }

    public int STUH_CAT_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_SIBLING_IF_ANY { get; set; }

    public int? STUH_SIBLING_CM_ID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string STUH_GENDER { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_DISABILITY_ANY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_MEDICAL_ALERGIES_ANY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_BIRTH_PLACE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_BIRTH_COUNTRY { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string STUH_PREVIOUS_SCHOOL_ATTENDED { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_PREVIOUS_SCHOOL_CLASS { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUH_PREVIOUS_SCHOOL_PERCENTAGE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_PREVIOUS_SCHOOL_RANK { get; set; }

    public int? STUH_PREVIOUS_SCHOOL_BOARD_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? STUH_PREVIOUS_SCHOOL_FROM_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? STUH_PREVIOUS_SCHOOL_TO_DATE { get; set; }

    public int STUH_CMP_ID { get; set; }

    public int STUH_SCH_ID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? STUH_WITHDRAWN_DATE { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string STUH_WITHDRAWN_REASON { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_BLOOD_GROUP { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_NATIONALITY { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string STUH_HOBBIES { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_RELIGION { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string STUH_PHONE { get; set; }

    public int? STUH_CITY_ID { get; set; }

    public int? STUH_STATE_ID { get; set; }

    public int? STUH_BLOOD_GROUP_ID { get; set; }

    public int? STUH_RELIGION_ID { get; set; }

    public int? STUH_ROUTE_ID { get; set; }

    public int? STUH_ROUTE_STOP_ID { get; set; }

    public int? STUH_CLASS_TEACHER_ID { get; set; }

    public int? STUH_ROUTE_PICK_AND_DROP { get; set; }

    public int? STUH_FDCM_ID { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUH_TUTION_FEES { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUH_ANNUAL_FFES { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? STUH_TRANSPORT_FEES { get; set; }

    public bool? STUH_USE_TRANSPORT_FEES { get; set; }

    public bool? STUH_IS_ACTIVE { get; set; }

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string STUH_SESSION { get; set; }

    [Required]
    [StringLength(50)]
    public string CREATED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CREATED_DATE { get; set; }

    [Required]
    [StringLength(50)]
    public string MODIFIED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime MODIFIED_DATE { get; set; }
}
