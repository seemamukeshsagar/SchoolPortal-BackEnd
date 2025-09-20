using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("EmpMaster")]
public partial class EmpMaster
{
    [Key]
    public int EMP_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_FIRST_NAME { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_LAST_NAME { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EMP_DOB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EMP_DOJ { get; set; }

    public int? EMP_PROBATION_PERIOD { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EMP_CONFIRMATION_DATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_PAN_NUMBER { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_ESIC_NUMBER { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_PF_NUMBER { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EMP_CURRENT_ADDRESS { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EMP_PERMANENT_ADDRESS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_CURRENT_CITY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_CURRENT_STATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_PERMANENT_CITY { get; set; }

    [StringLength(10)]
    public string EMP_PERMANENT_STATE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_PHONE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_MOBILE { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string EMP_EMAIL { get; set; }

    public int EMP_DEPT_ID { get; set; }

    public int EMP_DESIG_ID { get; set; }

    public int? EMP_PAYMENT_MODE_ID { get; set; }

    public int EMP_TYPE_ID { get; set; }

    public int EMP_CAT_ID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_BANK_ACCOUNT_NUMBER { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string EMP_BANK_NAME { get; set; }

    public int EMP_SCH_ID { get; set; }

    public int EMP_CMP_ID { get; set; }

    public bool EMP_IS_ACTIVE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string EMP_GENDER { get; set; }

    public int? EMP_BLOOD_GROUP_ID { get; set; }

    public int EMP_GRADE_ID { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EMP_IMAGE { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string EMP_CURRENT_ZIPCODE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_CURRENT_COUNTRY { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string EMP_PERMANENT_ZIPCODE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_PERMANENT_COUNTRY { get; set; }

    public int? EMP_OLD_ID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string EMP_FATHERS_NAME { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string EMP_DESCRIPTION { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_LICENCE_NUMBER { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EMP_LICENCE_ISSUE_DATE { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EMP_LICENCE_VALID_DATE { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string EMP_LICENCE_DESCRIPTION { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string EMP_LICENCE_IMAGE { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_LICENCE_TYPE { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string EMP_SALUTATION { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EMP_DATE_OF_LEAVING { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string EMP_MARTIAL_STATUS { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_YEARS_OF_EXPERIECNE { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string EMP_PREVIOUS_SCHOOL_COMPANY { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EMP_AADHAR_NUMBER { get; set; }

    public int? EMP_CATEGORY_ID { get; set; }

    public int? EMP_MATHS_UPTO_CLASS { get; set; }

    public int? EMP_ENGLISH_UPTO_CLASS { get; set; }

    public int? EMP_SST_UPTO_CLASS { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string CREATED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CREATED_DATE { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string MODIFIED_BY { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime MODIFIED_DATE { get; set; }

    [ForeignKey("EMP_CAT_ID")]
    [InverseProperty("EmpMasters")]
    public virtual EmpCategoryMaster EMP_CAT { get; set; }

    [ForeignKey("EMP_PAYMENT_MODE_ID")]
    [InverseProperty("EmpMasters")]
    public virtual PaymentModeMaster EMP_PAYMENT_MODE { get; set; }

    [ForeignKey("EMP_TYPE_ID")]
    [InverseProperty("EmpMasters")]
    public virtual EmpTypeMaster EMP_TYPE { get; set; }

    [InverseProperty("EMP_ATTEND_EMP")]
    public virtual ICollection<EmpAttendanceDetail> EmpAttendanceDetails { get; set; } = new List<EmpAttendanceDetail>();

    [InverseProperty("EMP_ATTENDH_EMP")]
    public virtual ICollection<EmpAttendanceDetailsHistory> EmpAttendanceDetailsHistories { get; set; } = new List<EmpAttendanceDetailsHistory>();

    [InverseProperty("EDOC_EMP")]
    public virtual ICollection<EmpDocumentDetail> EmpDocumentDetails { get; set; } = new List<EmpDocumentDetail>();

    [InverseProperty("EMPLAD_EMP")]
    public virtual ICollection<EmpLeaveAvailDetail> EmpLeaveAvailDetails { get; set; } = new List<EmpLeaveAvailDetail>();

    [InverseProperty("EMPLD_EMP")]
    public virtual ICollection<EmpLeaveDetail> EmpLeaveDetails { get; set; } = new List<EmpLeaveDetail>();

    [InverseProperty("EMPLDH_EMP")]
    public virtual ICollection<EmpLeaveDetailsHistory> EmpLeaveDetailsHistories { get; set; } = new List<EmpLeaveDetailsHistory>();

    [InverseProperty("EPQUALD_EMP")]
    public virtual ICollection<EmpProfQualiDetail> EmpProfQualiDetails { get; set; } = new List<EmpProfQualiDetail>();

    [InverseProperty("ESSD_EMP")]
    public virtual ICollection<EmpSalaryStructureDetail> EmpSalaryStructureDetails { get; set; } = new List<EmpSalaryStructureDetail>();

    [InverseProperty("ESSDH_EMP")]
    public virtual ICollection<EmpSalaryStructureDetailsHistory> EmpSalaryStructureDetailsHistories { get; set; } = new List<EmpSalaryStructureDetailsHistory>();

    [InverseProperty("TTDETAILH_TEACHER")]
    public virtual ICollection<TimeTableDetailsHistory> TimeTableDetailsHistories { get; set; } = new List<TimeTableDetailsHistory>();

    [InverseProperty("TTSUBSD_TEACHER_ID_NEWNavigation")]
    public virtual ICollection<TimeTableSubstitutionDetail> TimeTableSubstitutionDetailTTSUBSD_TEACHER_ID_NEWNavigations { get; set; } = new List<TimeTableSubstitutionDetail>();

    [InverseProperty("TTSUBSD_TEACHER")]
    public virtual ICollection<TimeTableSubstitutionDetail> TimeTableSubstitutionDetailTTSUBSD_TEACHERs { get; set; } = new List<TimeTableSubstitutionDetail>();

    [InverseProperty("TTSUBSDH_TEACHER_ID_NEWNavigation")]
    public virtual ICollection<TimeTableSubstitutionDetailsHistory> TimeTableSubstitutionDetailsHistoryTTSUBSDH_TEACHER_ID_NEWNavigations { get; set; } = new List<TimeTableSubstitutionDetailsHistory>();

    [InverseProperty("TTSUBSDH_TEACHER")]
    public virtual ICollection<TimeTableSubstitutionDetailsHistory> TimeTableSubstitutionDetailsHistoryTTSUBSDH_TEACHERs { get; set; } = new List<TimeTableSubstitutionDetailsHistory>();
}
