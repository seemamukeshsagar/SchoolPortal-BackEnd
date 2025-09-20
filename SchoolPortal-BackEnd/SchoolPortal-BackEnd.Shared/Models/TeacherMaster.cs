using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

[Table("TeacherMaster")]
public partial class TeacherMaster
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DOB { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DOJ { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date_OF_LEAVING { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Address { get; set; }

    public Guid? CityId { get; set; }
    
    [ForeignKey("CityId")]
    public virtual CityMaster? City { get; set; }

    public Guid? StateId { get; set; }
    
    [ForeignKey("StateId")]
    public virtual StateMaster? State { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ZipCode { get; set; }

    [Required]
    [StringLength(10)]
    [Unicode(false)]
    public string GENDER { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string MaritalStatus { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Image { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Phone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MobilePhone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string YearsOfExperience { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string PreviousSchool { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Salutation { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Email { get; set; }

    public Guid CompanyId { get; set; }

    public Guid SchoolId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Status { get; set; }

    [StringLength(255)]
    public string StatusMessage { get; set; }
}
