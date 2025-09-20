using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolPortal.Shared.Models;

public partial class SchoolNewPortalContext : DbContext
{
    public SchoolNewPortalContext()
    {
    }

    public SchoolNewPortalContext(DbContextOptions<SchoolNewPortalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssesmentMaster> AssesmentMasters { get; set; }

    public virtual DbSet<AttendanceReasonMaster> AttendanceReasonMasters { get; set; }

    public virtual DbSet<AuthorMaster> AuthorMasters { get; set; }

    public virtual DbSet<BillDetail> BillDetails { get; set; }

    public virtual DbSet<BillMaster> BillMasters { get; set; }

    public virtual DbSet<BloodGroupMaster> BloodGroupMasters { get; set; }

    public virtual DbSet<BookCategoryMaster> BookCategoryMasters { get; set; }

    public virtual DbSet<BookMaster> BookMasters { get; set; }

    public virtual DbSet<BookTransactionDetail> BookTransactionDetails { get; set; }

    public virtual DbSet<BookTransactionType> BookTransactionTypes { get; set; }

    public virtual DbSet<BookTypeMaster> BookTypeMasters { get; set; }

    public virtual DbSet<CategoryMaster> CategoryMasters { get; set; }

    public virtual DbSet<CityMaster> CityMasters { get; set; }

    public virtual DbSet<ClassMaster> ClassMasters { get; set; }

    public virtual DbSet<ClassSMSTasksDetail> ClassSMSTasksDetails { get; set; }

    public virtual DbSet<ClassScholasticDetail> ClassScholasticDetails { get; set; }

    public virtual DbSet<ClassScholasticDetailHistory> ClassScholasticDetailHistories { get; set; }

    public virtual DbSet<ClassSectionDetail> ClassSectionDetails { get; set; }

    public virtual DbSet<ClassSubjectDetail> ClassSubjectDetails { get; set; }

    public virtual DbSet<ClassSubjectDetailHistory> ClassSubjectDetailHistories { get; set; }

    public virtual DbSet<CleanerMaster> CleanerMasters { get; set; }

    public virtual DbSet<CompanyMaster> CompanyMasters { get; set; }

    public virtual DbSet<CountryMaster> CountryMasters { get; set; }

    public virtual DbSet<DeptDesigDetail> DeptDesigDetails { get; set; }

    public virtual DbSet<DeptMaster> DeptMasters { get; set; }

    public virtual DbSet<DesigGradeDetail> DesigGradeDetails { get; set; }

    public virtual DbSet<DesigMaster> DesigMasters { get; set; }

    public virtual DbSet<DriverMaster> DriverMasters { get; set; }

    public virtual DbSet<EmpAttendanceDetail> EmpAttendanceDetails { get; set; }

    public virtual DbSet<EmpAttendanceDetailsHistory> EmpAttendanceDetailsHistories { get; set; }

    public virtual DbSet<EmpCatLeaveDetail> EmpCatLeaveDetails { get; set; }

    public virtual DbSet<EmpCatLeaveDetailsHistory> EmpCatLeaveDetailsHistories { get; set; }

    public virtual DbSet<EmpCategoryMaster> EmpCategoryMasters { get; set; }

    public virtual DbSet<EmpDocumentDetail> EmpDocumentDetails { get; set; }

    public virtual DbSet<EmpLeaveAvailDetail> EmpLeaveAvailDetails { get; set; }

    public virtual DbSet<EmpLeaveDetail> EmpLeaveDetails { get; set; }

    public virtual DbSet<EmpLeaveDetailsHistory> EmpLeaveDetailsHistories { get; set; }

    public virtual DbSet<EmpMaster> EmpMasters { get; set; }

    public virtual DbSet<EmpProfQualiDetail> EmpProfQualiDetails { get; set; }

    public virtual DbSet<EmpSalaryDetail> EmpSalaryDetails { get; set; }

    public virtual DbSet<EmpSalaryDetailsHistory> EmpSalaryDetailsHistories { get; set; }

    public virtual DbSet<EmpSalaryMaster> EmpSalaryMasters { get; set; }

    public virtual DbSet<EmpSalaryMasterHistory> EmpSalaryMasterHistories { get; set; }

    public virtual DbSet<EmpSalaryStructureDetail> EmpSalaryStructureDetails { get; set; }

    public virtual DbSet<EmpSalaryStructureDetailsHistory> EmpSalaryStructureDetailsHistories { get; set; }

    public virtual DbSet<EmpTypeMaster> EmpTypeMasters { get; set; }

    public virtual DbSet<ExamCategoryMaster> ExamCategoryMasters { get; set; }

    public virtual DbSet<ExamUnitMaster> ExamUnitMasters { get; set; }

    public virtual DbSet<ExpenseCategoryMaster> ExpenseCategoryMasters { get; set; }

    public virtual DbSet<FeeClassDetail> FeeClassDetails { get; set; }

    public virtual DbSet<FeeClassDetailsHistory> FeeClassDetailsHistories { get; set; }

    public virtual DbSet<FeesCategoryMaster> FeesCategoryMasters { get; set; }

    public virtual DbSet<FeesDiscountCategoryMaster> FeesDiscountCategoryMasters { get; set; }

    public virtual DbSet<GenderMaster> GenderMasters { get; set; }

    public virtual DbSet<GradeMaster> GradeMasters { get; set; }

    public virtual DbSet<HolidayClassDetail> HolidayClassDetails { get; set; }

    public virtual DbSet<HolidayDeptDetail> HolidayDeptDetails { get; set; }

    public virtual DbSet<HolidayMaster> HolidayMasters { get; set; }

    public virtual DbSet<HolidayTypeMaster> HolidayTypeMasters { get; set; }

    public virtual DbSet<InventoryMaster> InventoryMasters { get; set; }

    public virtual DbSet<ItemLocationMaster> ItemLocationMasters { get; set; }

    public virtual DbSet<ItemMaster> ItemMasters { get; set; }

    public virtual DbSet<ItemTypeMaster> ItemTypeMasters { get; set; }

    public virtual DbSet<LeaveStatusMaster> LeaveStatusMasters { get; set; }

    public virtual DbSet<LeaveTypeMaster> LeaveTypeMasters { get; set; }

    public virtual DbSet<LocationMaster> LocationMasters { get; set; }

    public virtual DbSet<MarksGradeMaster> MarksGradeMasters { get; set; }

    public virtual DbSet<NotificationReceiverMaster> NotificationReceiverMasters { get; set; }

    public virtual DbSet<ParentMaster> ParentMasters { get; set; }

    public virtual DbSet<PaymentModeMaster> PaymentModeMasters { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<ProfessionMaster> ProfessionMasters { get; set; }

    public virtual DbSet<PublisherMaster> PublisherMasters { get; set; }

    public virtual DbSet<QualificationMaster> QualificationMasters { get; set; }

    public virtual DbSet<RegistrationMaster> RegistrationMasters { get; set; }

    public virtual DbSet<ReligionMaster> ReligionMasters { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<RolePrivilege> RolePrivileges { get; set; }

    public virtual DbSet<RouteDetail> RouteDetails { get; set; }

    public virtual DbSet<RouteMaster> RouteMasters { get; set; }

    public virtual DbSet<RouteStopDetail> RouteStopDetails { get; set; }

    public virtual DbSet<SMSTask> SMSTasks { get; set; }

    public virtual DbSet<SMSTaskHistory> SMSTaskHistories { get; set; }

    public virtual DbSet<SMSTaskSchedule> SMSTaskSchedules { get; set; }

    public virtual DbSet<SMSTaskSmtpDetail> SMSTaskSmtpDetails { get; set; }

    public virtual DbSet<SMSTaskStatusMaster> SMSTaskStatusMasters { get; set; }

    public virtual DbSet<SalaryDesigGradeDetail> SalaryDesigGradeDetails { get; set; }

    public virtual DbSet<SalaryDesigGradeDetailsHistory> SalaryDesigGradeDetailsHistories { get; set; }

    public virtual DbSet<SalaryHeadMaster> SalaryHeadMasters { get; set; }

    public virtual DbSet<ScholasticMaster> ScholasticMasters { get; set; }

    public virtual DbSet<ScholasticUnitDetail> ScholasticUnitDetails { get; set; }

    public virtual DbSet<SchoolContactMaster> SchoolContactMasters { get; set; }

    public virtual DbSet<SchoolMaster> SchoolMasters { get; set; }

    public virtual DbSet<SectionMaster> SectionMasters { get; set; }

    public virtual DbSet<SessionMaster> SessionMasters { get; set; }

    public virtual DbSet<SmtpDetail> SmtpDetails { get; set; }

    public virtual DbSet<StateMaster> StateMasters { get; set; }

    public virtual DbSet<StudentAchievement> StudentAchievements { get; set; }

    public virtual DbSet<StudentAttendanceDetail> StudentAttendanceDetails { get; set; }

    public virtual DbSet<StudentCommentDetail> StudentCommentDetails { get; set; }

    public virtual DbSet<StudentCommentDetailsHistory> StudentCommentDetailsHistories { get; set; }

    public virtual DbSet<StudentFeeDetail> StudentFeeDetails { get; set; }

    public virtual DbSet<StudentFeeDetailsHistory> StudentFeeDetailsHistories { get; set; }

    public virtual DbSet<StudentGradeDetail> StudentGradeDetails { get; set; }

    public virtual DbSet<StudentGradeDetailsHistory> StudentGradeDetailsHistories { get; set; }

    public virtual DbSet<StudentMarksDetail> StudentMarksDetails { get; set; }

    public virtual DbSet<StudentMarksDetailsHistory> StudentMarksDetailsHistories { get; set; }

    public virtual DbSet<StudentMaster> StudentMasters { get; set; }

    public virtual DbSet<StudentMasterHistory> StudentMasterHistories { get; set; }

    public virtual DbSet<StudentReportCardDetail> StudentReportCardDetails { get; set; }

    public virtual DbSet<StudentReportCardDetailsHistory> StudentReportCardDetailsHistories { get; set; }

    public virtual DbSet<StudentReportCardMaster> StudentReportCardMasters { get; set; }

    public virtual DbSet<StudentReportCardMasterHistory> StudentReportCardMasterHistories { get; set; }

    public virtual DbSet<SubjectCategoryDetail> SubjectCategoryDetails { get; set; }

    public virtual DbSet<SubjectCategoryDetailsHistory> SubjectCategoryDetailsHistories { get; set; }

    public virtual DbSet<SubjectMaster> SubjectMasters { get; set; }

    public virtual DbSet<SupplierMaster> SupplierMasters { get; set; }

    public virtual DbSet<SystemParameter> SystemParameters { get; set; }

    public virtual DbSet<TeacherClassDetail> TeacherClassDetails { get; set; }

    public virtual DbSet<TeacherDocumentDetail> TeacherDocumentDetails { get; set; }

    public virtual DbSet<TeacherMaster> TeacherMasters { get; set; }

    public virtual DbSet<TeacherQualificationDetail> TeacherQualificationDetails { get; set; }

    public virtual DbSet<TeacherSectionDetail> TeacherSectionDetails { get; set; }

    public virtual DbSet<TeacherSubjectDetail> TeacherSubjectDetails { get; set; }

    public virtual DbSet<TimeTableClassPeriodDetail> TimeTableClassPeriodDetails { get; set; }

    public virtual DbSet<TimeTableClassPeriodDetailsHistory> TimeTableClassPeriodDetailsHistories { get; set; }

    public virtual DbSet<TimeTableDetailsHistory> TimeTableDetailsHistories { get; set; }

    public virtual DbSet<TimeTablePeriodMaster> TimeTablePeriodMasters { get; set; }

    public virtual DbSet<TimeTablePeriodMasterHistory> TimeTablePeriodMasterHistories { get; set; }

    public virtual DbSet<TimeTableSession> TimeTableSessions { get; set; }

    public virtual DbSet<TimeTableSetupDetail> TimeTableSetupDetails { get; set; }

    public virtual DbSet<TimeTableSetupDetailsHistory> TimeTableSetupDetailsHistories { get; set; }

    public virtual DbSet<TimeTableSubstitutionDetail> TimeTableSubstitutionDetails { get; set; }

    public virtual DbSet<TimeTableSubstitutionDetailsHistory> TimeTableSubstitutionDetailsHistories { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<VehicleExpenseDetail> VehicleExpenseDetails { get; set; }

    public virtual DbSet<VehicleMaster> VehicleMasters { get; set; }

    public virtual DbSet<VendorMaster> VendorMasters { get; set; }

    public virtual DbSet<VisitorMaster> VisitorMasters { get; set; }

    public virtual DbSet<VoucherMaster> VoucherMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SAGAR\\SQL2022;Initial Catalog=SchoolNewPortal;Integrated Security=True;Trust Server Certificate=True;Command Timeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssesmentMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status)
                .HasDefaultValue("INC")
                .IsFixedLength();
        });

        modelBuilder.Entity<AttendanceReasonMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status)
                .HasDefaultValue("INC")
                .IsFixedLength();
        });

        modelBuilder.Entity<AuthorMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).IsFixedLength();
        });

        modelBuilder.Entity<BillMaster>(entity =>
        {
            entity.HasOne(d => d.BILL_VEHICLE).WithMany(p => p.BillMasters).HasConstraintName("FK_BillMaster_VehicleMaster");

            entity.HasOne(d => d.BILL_VENDOR).WithMany(p => p.BillMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillMaster_VendorMaster");
        });

        modelBuilder.Entity<BloodGroupMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BookCategoryMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BookMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BookTransactionDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BookTransactionType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<BookTypeMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CategoryMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CityMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CityState).WithMany(p => p.CityMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CityMaster_CityStateID");
        });

        modelBuilder.Entity<ClassMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ClassSMSTasksDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ClassScholasticDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ClassScholasticDetailHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ClassSectionDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ClassSubjectDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ClassSubjectDetailHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CleanerMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<CompanyMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.City).WithMany(p => p.CompanyMasterCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMaster_CityID");

            entity.HasOne(d => d.Country).WithMany(p => p.CompanyMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMaster_CountryID");

            entity.HasOne(d => d.JudistrictionAreaNavigation).WithMany(p => p.CompanyMasterJudistrictionAreaNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMaster_JudistrictionArea");

            entity.HasOne(d => d.State).WithMany(p => p.CompanyMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyMaster_StateID");
        });

        modelBuilder.Entity<CountryMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<DeptDesigDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.DeptDesigDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptDesigDetails_CompanyID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DeptDesigDetailCreatedByNavigations).HasConstraintName("FK_DeptDesigDetails_CreatedID");

            entity.HasOne(d => d.Department).WithMany(p => p.DeptDesigDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptDesigDetails_DepartmentId");

            entity.HasOne(d => d.Designation).WithMany(p => p.DeptDesigDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptDesigDetails_DesignationId");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.DeptDesigDetailModifiedByNavigations).HasConstraintName("FK_DeptDesigDetails_ModifiedBy");

            entity.HasOne(d => d.School).WithMany(p => p.DeptDesigDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptDesigDetails_SchoolID");
        });

        modelBuilder.Entity<DeptMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.DeptMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptMaster_CompanyID");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DeptMasterCreatedByNavigations).HasConstraintName("FK_DeptMaster_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.DeptMasterModifiedByNavigations).HasConstraintName("FK_DeptMaster_ModifiedBy");

            entity.HasOne(d => d.School).WithMany(p => p.DeptMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptMaster_SchoolID");
        });

        modelBuilder.Entity<DesigGradeDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.DesigGradeDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DesigGradeDetails_CompanyId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DesigGradeDetailCreatedByNavigations).HasConstraintName("FK_DesigGradeDetails_CreatedBy");

            entity.HasOne(d => d.Designation).WithMany(p => p.DesigGradeDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DesigGradeDetails_DesignationId");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.DesigGradeDetailModifiedByNavigations).HasConstraintName("FK_DesigGradeDetails_ModifiedBy");

            entity.HasOne(d => d.School).WithMany(p => p.DesigGradeDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DesigGradeDetails_SchoolId");
        });

        modelBuilder.Entity<DesigMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.DesigMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DesigMaster_CompanyID");

            entity.HasOne(d => d.School).WithMany(p => p.DesigMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DesigMaster_SchoolID");
        });

        modelBuilder.Entity<EmpAttendanceDetail>(entity =>
        {
            entity.HasKey(e => e.EMP_ATTEND_ID).HasName("PK_EMPAttendanceDetails");

            entity.HasOne(d => d.EMP_ATTEND_EMP).WithMany(p => p.EmpAttendanceDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpAttendanceDetails_EmpMaster");
        });

        modelBuilder.Entity<EmpAttendanceDetailsHistory>(entity =>
        {
            entity.HasKey(e => e.EMP_ATTENDH_ID).HasName("PK_EMPAttendanceDetailsHistory");

            entity.HasOne(d => d.EMP_ATTENDH_EMP).WithMany(p => p.EmpAttendanceDetailsHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpAttendanceDetailsHistory_EmpMaster");
        });

        modelBuilder.Entity<EmpCatLeaveDetail>(entity =>
        {
            entity.HasOne(d => d.ECAT_LEAVE_CAT).WithMany(p => p.EmpCatLeaveDetails).HasConstraintName("FK_EmpCatLeaveDetails_EmpCategoryMaster");
        });

        modelBuilder.Entity<EmpCatLeaveDetailsHistory>(entity =>
        {
            entity.HasOne(d => d.ECATH_LEAVE_CAT).WithMany(p => p.EmpCatLeaveDetailsHistories).HasConstraintName("FK_EmpCatLeaveDetailsHistory_EmpCategoryMaster");
        });

        modelBuilder.Entity<EmpDocumentDetail>(entity =>
        {
            entity.HasOne(d => d.EDOC_EMP).WithMany(p => p.EmpDocumentDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpDocumentDetails_EmpMaster");
        });

        modelBuilder.Entity<EmpLeaveAvailDetail>(entity =>
        {
            entity.HasOne(d => d.EMPLAD_EMP).WithMany(p => p.EmpLeaveAvailDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpLeaveAvailDetails_EmpMaster");

            entity.HasOne(d => d.EMPLAD_STATUS).WithMany(p => p.EmpLeaveAvailDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpLeaveAvailDetails_LeaveStatusMaster");
        });

        modelBuilder.Entity<EmpLeaveDetail>(entity =>
        {
            entity.HasOne(d => d.EMPLD_CAT).WithMany(p => p.EmpLeaveDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpLeaveDetails_EmpCategoryMaster");

            entity.HasOne(d => d.EMPLD_EMP).WithMany(p => p.EmpLeaveDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpLeaveDetails_EmpMaster");
        });

        modelBuilder.Entity<EmpLeaveDetailsHistory>(entity =>
        {
            entity.HasOne(d => d.EMPLDH_CAT).WithMany(p => p.EmpLeaveDetailsHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpLeaveDetailsHistory_EmpCategoryMaster");

            entity.HasOne(d => d.EMPLDH_EMP).WithMany(p => p.EmpLeaveDetailsHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpLeaveDetailsHistory_EmpMaster");
        });

        modelBuilder.Entity<EmpMaster>(entity =>
        {
            entity.Property(e => e.EMP_PERMANENT_STATE).IsFixedLength();

            entity.HasOne(d => d.EMP_CAT).WithMany(p => p.EmpMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpMaster_EmpCategoryMaster");

            entity.HasOne(d => d.EMP_PAYMENT_MODE).WithMany(p => p.EmpMasters).HasConstraintName("FK_EmpMaster_PayModeMaster");

            entity.HasOne(d => d.EMP_TYPE).WithMany(p => p.EmpMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpMaster_EmpTypes");
        });

        modelBuilder.Entity<EmpProfQualiDetail>(entity =>
        {
            entity.HasOne(d => d.EPQUALD_EMP).WithMany(p => p.EmpProfQualiDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpProfQualiDetails_EmpMaster");
        });

        modelBuilder.Entity<EmpSalaryStructureDetail>(entity =>
        {
            entity.HasOne(d => d.ESSD_EMP).WithMany(p => p.EmpSalaryStructureDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpSalaryStructureDetails_EmpMaster");
        });

        modelBuilder.Entity<EmpSalaryStructureDetailsHistory>(entity =>
        {
            entity.HasOne(d => d.ESSDH_EMP).WithMany(p => p.EmpSalaryStructureDetailsHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmpSalaryStructureDetailsHistory_EmpMaster");
        });

        modelBuilder.Entity<ExamUnitMaster>(entity =>
        {
            entity.HasKey(e => e.EXAM_UNIT_ID).HasName("PK_ExamUnitMaster_1");
        });

        modelBuilder.Entity<ExpenseCategoryMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<FeeClassDetail>(entity =>
        {
            entity.HasKey(e => e.FCD_ID).HasName("PK_FeesMaster");
        });

        modelBuilder.Entity<FeeClassDetailsHistory>(entity =>
        {
            entity.HasOne(d => d.FCDH_FEE_CAT).WithMany(p => p.FeeClassDetailsHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FeeClassDetailsHistory_FeeCategoryMaster");
        });

        modelBuilder.Entity<FeesDiscountCategoryMaster>(entity =>
        {
            entity.Property(e => e.FeesDiscountCategoryMasterId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue("INC");
        });

        modelBuilder.Entity<GenderMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.GenderMasterCreatedByNavigations).HasConstraintName("FK_GenderMaster_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.GenderMasterModifiedByNavigations).HasConstraintName("FK_GenderMaster_ModifiedBy");
        });

        modelBuilder.Entity<GradeMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<HolidayClassDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<HolidayDeptDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<HolidayMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<HolidayTypeMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<InventoryMaster>(entity =>
        {
            entity.HasOne(d => d.INV_ITEM).WithMany(p => p.InventoryMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryMaster_ItemMaster");

            entity.HasOne(d => d.INV_ITEM_LOCATION).WithMany(p => p.InventoryMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InventoryMaster_ItemLocationMaster");
        });

        modelBuilder.Entity<ItemMaster>(entity =>
        {
            entity.HasOne(d => d.ITEM_TYPE_MASTER).WithMany(p => p.ItemMasters).HasConstraintName("FK_ItemMaster_ItemTypeMaster");
        });

        modelBuilder.Entity<LeaveTypeMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<LocationMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ParentMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<QualificationMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RegistrationMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ReligionMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Company).WithMany(p => p.RoleMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleMaster_CompanyId");

            entity.HasOne(d => d.School).WithMany(p => p.RoleMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleMaster_SchoolId");
        });

        modelBuilder.Entity<RolePrivilege>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Privilege).WithMany(p => p.RolePrivileges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolePrivi__Privi__6A85CC04");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePrivileges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolePrivileges_RoleId");
        });

        modelBuilder.Entity<RouteDetail>(entity =>
        {
            entity.Property(e => e.RouteDetailId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue("INC");
        });

        modelBuilder.Entity<RouteMaster>(entity =>
        {
            entity.Property(e => e.RouteId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue("INC");
        });

        modelBuilder.Entity<RouteStopDetail>(entity =>
        {
            entity.Property(e => e.RouteStopId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue("INC");
        });

        modelBuilder.Entity<SMSTask>(entity =>
        {
            entity.HasOne(d => d.STS_NOTIFICATION_RECEIEVER).WithMany(p => p.SMSTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SMSTask_NotificationReceiver");
        });

        modelBuilder.Entity<SMSTaskSchedule>(entity =>
        {
            entity.HasOne(d => d.STS_SCHEDULE_TASK).WithMany(p => p.SMSTaskSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SMSTaskSchedule_SMSTasks");
        });

        modelBuilder.Entity<SMSTaskSmtpDetail>(entity =>
        {
            entity.HasOne(d => d.STSSMTP_TASK).WithMany(p => p.SMSTaskSmtpDetails).HasConstraintName("FK_SMSTaskSmtpDetails_SMSTask");
        });

        modelBuilder.Entity<SalaryDesigGradeDetail>(entity =>
        {
            entity.HasOne(d => d.SALDGD_SALHM).WithMany(p => p.SalaryDesigGradeDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalaryDesigGradeDetails_SalaryHeadMaster");
        });

        modelBuilder.Entity<SalaryDesigGradeDetailsHistory>(entity =>
        {
            entity.HasOne(d => d.SALDGDH_SALHM).WithMany(p => p.SalaryDesigGradeDetailsHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalaryDesigGradeDetailsHistory_SalaryHeadMaster");
        });

        modelBuilder.Entity<ScholasticUnitDetail>(entity =>
        {
            entity.HasOne(d => d.SCOHUD_SCHOLASTIC).WithMany(p => p.ScholasticUnitDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScholasticUnitDetail_ScholasticMaster");

            entity.HasOne(d => d.SCOHUD_UNIT).WithMany(p => p.ScholasticUnitDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScholasticUnitDetail_ExamUnitMaster");
        });

        modelBuilder.Entity<SchoolContactMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SchoolMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EstablishmentYear).IsFixedLength();

            entity.HasOne(d => d.BankCountry).WithMany(p => p.SchoolMasterBankCountries).HasConstraintName("FK_SchoolMaster_BankCountryId");

            entity.HasOne(d => d.Company).WithMany(p => p.SchoolMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SchoolMaster_CompanyMaster");

            entity.HasOne(d => d.Country).WithMany(p => p.SchoolMasterCountries).HasConstraintName("FK_SchoolMaster_CountryId");

            entity.HasOne(d => d.JudistrictionCountry).WithMany(p => p.SchoolMasterJudistrictionCountries).HasConstraintName("FK_SchoolMaster_JudistrictionCountryId");
        });

        modelBuilder.Entity<SectionMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SessionMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<StateMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Country).WithMany(p => p.StateMasters)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StateMaster_CountryID");
        });

        modelBuilder.Entity<StudentAttendanceDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<StudentCommentDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<StudentFeeDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<StudentFeeDetailsHistory>(entity =>
        {
            entity.HasKey(e => e.STUDFEEH_ID).HasName("PK_StudentFeesDetailHistory");
        });

        modelBuilder.Entity<StudentGradeDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<StudentMaster>(entity =>
        {
            entity.Property(e => e.StudentGUID).ValueGeneratedNever();
        });

        modelBuilder.Entity<StudentReportCardMaster>(entity =>
        {
            entity.HasKey(e => e.REPCARD_ID).HasName("PK_StudentReportCardMaster_1");
        });

        modelBuilder.Entity<SubjectCategoryDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SubjectCategoryDetailsHistory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SubjectMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<SupplierMaster>(entity =>
        {
            entity.HasKey(e => e.SUPPLIER_ID).HasName("PK_SUPPLIERMaster");
        });

        modelBuilder.Entity<TeacherClassDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TeacherDocumentDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TeacherMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TeacherQualificationDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TeacherSectionDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TeacherSubjectDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TimeTableDetailsHistory>(entity =>
        {
            entity.HasOne(d => d.TTDETAILH_TEACHER).WithMany(p => p.TimeTableDetailsHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeTableDetailsHistory_TeacherMaster");
        });

        modelBuilder.Entity<TimeTablePeriodMaster>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TimeTableSession>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue("INC");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TimeTableSessionCreatedByNavigations).HasConstraintName("FK_TimeTableSessions_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TimeTableSessionModifiedByNavigations).HasConstraintName("FK_TimeTableSessions_ModifiedBy");
        });

        modelBuilder.Entity<TimeTableSetupDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Status).HasDefaultValue("INC");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TimeTableSetupDetailCreatedByNavigations).HasConstraintName("FK_TimeTableSetupDetails_CreatedBy");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TimeTableSetupDetailModifiedByNavigations).HasConstraintName("FK_TimeTableSetupDetails_ModifiedBy");
        });

        modelBuilder.Entity<TimeTableSubstitutionDetail>(entity =>
        {
            entity.HasOne(d => d.TTSUBSD_TEACHER).WithMany(p => p.TimeTableSubstitutionDetailTTSUBSD_TEACHERs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeTableSubstitutionDetails_EmpMaster");

            entity.HasOne(d => d.TTSUBSD_TEACHER_ID_NEWNavigation).WithMany(p => p.TimeTableSubstitutionDetailTTSUBSD_TEACHER_ID_NEWNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeTableSubstitutionDetails_EmpMasterNewTeacher");
        });

        modelBuilder.Entity<TimeTableSubstitutionDetailsHistory>(entity =>
        {
            entity.HasOne(d => d.TTSUBSDH_TEACHER).WithMany(p => p.TimeTableSubstitutionDetailsHistoryTTSUBSDH_TEACHERs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeTableSubstDetailsHistory_EmpMaster");

            entity.HasOne(d => d.TTSUBSDH_TEACHER_ID_NEWNavigation).WithMany(p => p.TimeTableSubstitutionDetailsHistoryTTSUBSDH_TEACHER_ID_NEWNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TimeTableSubstDetailsHistory_EmpMasterNewTeacher");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).HasDefaultValue("INC");

            entity.HasOne(d => d.Company).WithMany(p => p.UserDetails).HasConstraintName("FK_UserDetails_CompanyID");

            entity.HasOne(d => d.Designation).WithMany(p => p.UserDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDetails_DesignationID");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InverseModifiedByNavigation).HasConstraintName("FK_UserDetails_ModifiedBy");

            entity.HasOne(d => d.School).WithMany(p => p.UserDetails).HasConstraintName("FK_UserDetails_SchoolID");

            entity.HasOne(d => d.UserRole).WithMany(p => p.UserDetails).HasConstraintName("FK_UserDetails_UserRoleID");
        });

        modelBuilder.Entity<VehicleExpenseDetail>(entity =>
        {
            entity.HasOne(d => d.VEHEXP_VEHICEL).WithMany(p => p.VehicleExpenseDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VehicleExpenseDetails_VehicleMaster");
        });

        modelBuilder.Entity<VehicleMaster>(entity =>
        {
            entity.HasKey(e => e.VEH_ID).HasName("PK_Veh");
        });

        modelBuilder.Entity<VisitorMaster>(entity =>
        {
            entity.HasKey(e => e.VM_ID).HasName("PK_VisitorMaster\\");
        });

        OnModelCreatingGeneratedFunctions(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
