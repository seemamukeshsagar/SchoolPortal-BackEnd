using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherSection;
using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class TeacherSectionService : ITeacherSectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherSectionService> _logger;

        public TeacherSectionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TeacherSectionService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TeacherSectionResponseDto>> GetAllTeacherSectionsAsync()
        {
            var teacherSections = await _unitOfWork.TeacherSections
                .FindAsync(ts => !ts.IsDeleted);
                
            return _mapper.Map<IEnumerable<TeacherSectionResponseDto>>(teacherSections);
        }

        public async Task<TeacherSectionResponseDto?> GetTeacherSectionByIdAsync(Guid id)
        {
            var teacherSection = await _unitOfWork.TeacherSections.GetByIdAsync(id);
            if (teacherSection == null || teacherSection.IsDeleted)
                return null;
                
            return _mapper.Map<TeacherSectionResponseDto>(teacherSection);
        }

        public async Task<IEnumerable<TeacherSectionResponseDto>> GetSectionsByTeacherIdAsync(Guid teacherId)
        {
            var teacherSections = await _unitOfWork.TeacherSections.GetByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<TeacherSectionResponseDto>>(teacherSections);
        }

        public async Task<IEnumerable<TeacherSectionResponseDto>> GetTeachersBySectionIdAsync(Guid sectionId)
        {
            var teacherSections = await _unitOfWork.TeacherSections.GetBySectionIdAsync(sectionId);
            return _mapper.Map<IEnumerable<TeacherSectionResponseDto>>(teacherSections);
        }

        public async Task<TeacherSectionResponseDto?> GetClassTeacherAsync(Guid classId, Guid sectionId)
        {
            var classTeacher = await _unitOfWork.TeacherSections.GetClassTeacherAsync(classId, sectionId);
            return classTeacher == null ? null : _mapper.Map<TeacherSectionResponseDto>(classTeacher);
        }

        public async Task<TeacherSectionResponseDto?> AddTeacherSectionAsync(TeacherSectionDto teacherSectionDto, Guid userId)
        {
            // Check if the teacher is already assigned to this section and subject
            var existingAssignment = await _unitOfWork.TeacherSections.FindAsync(
                ts => ts.TeacherId == teacherSectionDto.TeacherId &&
                     ts.ClassId == teacherSectionDto.ClassId &&
                     ts.SectionId == teacherSectionDto.SectionId &&
                     ts.SubjectId == teacherSectionDto.SubjectId &&
                     !ts.IsDeleted);

            if (existingAssignment.Any())
            {
                throw new InvalidOperationException("This teacher is already assigned to teach this subject in the specified class and section.");
            }

            // If this is a class teacher assignment, ensure there isn't already a class teacher
            if (teacherSectionDto.IsClassTeacher)
            {
                var existingClassTeacher = await _unitOfWork.TeacherSections.GetClassTeacherAsync(
                    teacherSectionDto.ClassId, 
                    teacherSectionDto.SectionId);
                
                if (existingClassTeacher != null && existingClassTeacher.Id != teacherSectionDto.Id)
                {
                    throw new InvalidOperationException("There is already a class teacher assigned to this section.");
                }
            }

            var teacherSection = _mapper.Map<TeacherSectionDetail>(teacherSectionDto);
            teacherSection.Id = Guid.NewGuid();
            teacherSection.CreatedBy = userId;
            teacherSection.CreatedDate = DateTime.UtcNow;
            teacherSection.IsActive = true;
            teacherSection.IsDeleted = false;

            await _unitOfWork.TeacherSections.AddAsync(teacherSection);
            await _unitOfWork.CompleteAsync();

            var createdTeacherSection = await _unitOfWork.TeacherSections.GetByIdAsync(teacherSection.Id);
            return createdTeacherSection == null 
                ? null 
                : _mapper.Map<TeacherSectionResponseDto>(createdTeacherSection);
        }

        public async Task<TeacherSectionResponseDto?> UpdateTeacherSectionAsync(
            Guid id, 
            TeacherSectionDto teacherSectionDto, 
            Guid userId)
        {
            var existingTeacherSection = await _unitOfWork.TeacherSections.GetByIdAsync(id);
            if (existingTeacherSection == null || existingTeacherSection.IsDeleted)
                return null;

            // If this is a class teacher assignment, ensure there isn't already a class teacher
            if (teacherSectionDto.IsClassTeacher && 
                (existingTeacherSection.ClassId != teacherSectionDto.ClassId || 
                 existingTeacherSection.SectionId != teacherSectionDto.SectionId ||
                 !existingTeacherSection.IsClassTeacher))
            {
                var existingClassTeacher = await _unitOfWork.TeacherSections.GetClassTeacherAsync(
                    teacherSectionDto.ClassId, 
                    teacherSectionDto.SectionId);
                
                if (existingClassTeacher != null && existingClassTeacher.Id != id)
                {
                    throw new InvalidOperationException("There is already a class teacher assigned to this section.");
                }
            }

            _mapper.Map(teacherSectionDto, existingTeacherSection);
            existingTeacherSection.ModifiedBy = userId;
            existingTeacherSection.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherSections.Update(existingTeacherSection);
            await _unitOfWork.CompleteAsync();

            var updatedTeacherSection = await _unitOfWork.TeacherSections.GetByIdAsync(id);
            return updatedTeacherSection == null 
                ? null 
                : _mapper.Map<TeacherSectionResponseDto>(updatedTeacherSection);
        }

        public async Task<bool> RemoveTeacherSectionAsync(Guid id, Guid userId)
        {
            var teacherSection = await _unitOfWork.TeacherSections.GetByIdAsync(id);
            if (teacherSection == null || teacherSection.IsDeleted)
                return false;

            // Don't allow removing the class teacher assignment directly
            if (teacherSection.IsClassTeacher)
            {
                throw new InvalidOperationException("Cannot remove a class teacher assignment directly. Please assign a new class teacher first.");
            }

            // Soft delete
            teacherSection.IsDeleted = true;
            teacherSection.ModifiedBy = userId;
            teacherSection.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherSections.Update(teacherSection);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> IsClassTeacherAsync(Guid teacherId, Guid classId, Guid sectionId)
        {
            return await _unitOfWork.TeacherSections.IsClassTeacherAsync(teacherId, classId, sectionId);
        }

        public async Task<bool> TeacherTeachesInSectionAsync(Guid teacherId, Guid sectionId)
        {
            return await _unitOfWork.TeacherSections.TeacherTeachesInSectionAsync(teacherId, sectionId);
        }

        public async Task<bool> SetClassTeacherAsync(Guid teacherId, Guid classId, Guid sectionId, Guid userId)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Get the current class teacher
                var currentClassTeacher = await _unitOfWork.TeacherSections.GetClassTeacherAsync(classId, sectionId);
                
                // If there's already a class teacher, unset them
                if (currentClassTeacher != null)
                {
                    currentClassTeacher.IsClassTeacher = false;
                    currentClassTeacher.ModifiedBy = userId;
                    currentClassTeacher.ModifiedDate = DateTime.UtcNow;
                    _unitOfWork.TeacherSections.Update(currentClassTeacher);
                }

                // Get the teacher's assignment to this section
                var teacherAssignment = (await _unitOfWork.TeacherSections.FindAsync(
                    ts => ts.TeacherId == teacherId &&
                         ts.ClassId == classId &&
                         ts.SectionId == sectionId &&
                         !ts.IsDeleted)).FirstOrDefault();

                if (teacherAssignment == null)
                {
                    throw new InvalidOperationException("The specified teacher is not assigned to this section.");
                }

                // Set as class teacher
                teacherAssignment.IsClassTeacher = true;
                teacherAssignment.ModifiedBy = userId;
                teacherAssignment.ModifiedDate = DateTime.UtcNow;
                _unitOfWork.TeacherSections.Update(teacherAssignment);

                await _unitOfWork.CompleteAsync();
                await transaction.CommitAsync();
                
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
