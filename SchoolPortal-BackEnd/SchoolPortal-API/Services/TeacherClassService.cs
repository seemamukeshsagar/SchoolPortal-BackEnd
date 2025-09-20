using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherClass;
using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class TeacherClassService : ITeacherClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherClassService> _logger;

        public TeacherClassService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<TeacherClassService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TeacherClassResponseDto>> GetAllTeacherClassesAsync()
        {
            var teacherClasses = await _unitOfWork.TeacherClasses
                .FindAsync(tc => !tc.IsDeleted);
                
            return _mapper.Map<IEnumerable<TeacherClassResponseDto>>(teacherClasses);
        }

        public async Task<TeacherClassResponseDto?> GetTeacherClassByIdAsync(Guid id)
        {
            var teacherClass = await _unitOfWork.TeacherClasses.GetByIdAsync(id);
            if (teacherClass == null || teacherClass.IsDeleted)
                return null;
                
            return _mapper.Map<TeacherClassResponseDto>(teacherClass);
        }

        public async Task<IEnumerable<TeacherClassResponseDto>> GetClassesByTeacherIdAsync(Guid teacherId)
        {
            var teacherClasses = await _unitOfWork.TeacherClasses.GetByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<TeacherClassResponseDto>>(teacherClasses);
        }

        public async Task<IEnumerable<TeacherClassResponseDto>> GetTeachersByClassIdAsync(Guid classId)
        {
            var teacherClasses = await _unitOfWork.TeacherClasses.GetByClassIdAsync(classId);
            return _mapper.Map<IEnumerable<TeacherClassResponseDto>>(teacherClasses);
        }

        public async Task<IEnumerable<TeacherClassResponseDto>> GetByTeacherAndClassAsync(Guid teacherId, Guid classId)
        {
            var teacherClasses = await _unitOfWork.TeacherClasses.FindAsync(
                tc => tc.TeacherId == teacherId && 
                     tc.ClassId == classId && 
                     !tc.IsDeleted);
            
            return _mapper.Map<IEnumerable<TeacherClassResponseDto>>(teacherClasses);
        }

        public async Task<IEnumerable<TeacherClassResponseDto>> GetByTeacherAndSubjectAsync(Guid teacherId, Guid subjectId)
        {
            var teacherClasses = await _unitOfWork.TeacherClasses.FindAsync(
                tc => tc.TeacherId == teacherId && 
                     tc.SubjectId == subjectId && 
                     !tc.IsDeleted);
            
            return _mapper.Map<IEnumerable<TeacherClassResponseDto>>(teacherClasses);
        }

        public async Task<TeacherClassResponseDto?> AddTeacherClassAsync(TeacherClassDto teacherClassDto, Guid userId)
        {
            // Check if the teacher is already assigned to this class and subject
            var existingAssignment = await _unitOfWork.TeacherClasses.FindAsync(
                tc => tc.TeacherId == teacherClassDto.TeacherId &&
                     tc.ClassId == teacherClassDto.ClassId &&
                     tc.SectionId == teacherClassDto.SectionId &&
                     tc.SubjectId == teacherClassDto.SubjectId &&
                     !tc.IsDeleted);

            if (existingAssignment.Any())
            {
                throw new InvalidOperationException("This teacher is already assigned to teach this subject in the specified class and section.");
            }

            var teacherClass = _mapper.Map<TeacherClassDetail>(teacherClassDto);
            teacherClass.Id = Guid.NewGuid();
            teacherClass.CreatedBy = userId;
            teacherClass.CreatedDate = DateTime.UtcNow;
            teacherClass.IsActive = true;
            teacherClass.IsDeleted = false;

            await _unitOfWork.TeacherClasses.AddAsync(teacherClass);
            await _unitOfWork.CompleteAsync();

            var createdTeacherClass = await _unitOfWork.TeacherClasses.GetByIdAsync(teacherClass.Id);
            return createdTeacherClass == null 
                ? null 
                : _mapper.Map<TeacherClassResponseDto>(createdTeacherClass);
        }

        public async Task<TeacherClassResponseDto?> UpdateTeacherClassAsync(
            Guid id, 
            TeacherClassDto teacherClassDto, 
            Guid userId)
        {
            var existingTeacherClass = await _unitOfWork.TeacherClasses.GetByIdAsync(id);
            if (existingTeacherClass == null || existingTeacherClass.IsDeleted)
                return null;

            // Check if updating to a duplicate assignment
            if (existingTeacherClass.ClassId != teacherClassDto.ClassId ||
                existingTeacherClass.SectionId != teacherClassDto.SectionId ||
                existingTeacherClass.SubjectId != teacherClassDto.SubjectId)
            {
                var duplicateAssignment = await _unitOfWork.TeacherClasses.FindAsync(
                    tc => tc.TeacherId == teacherClassDto.TeacherId &&
                         tc.ClassId == teacherClassDto.ClassId &&
                         tc.SectionId == teacherClassDto.SectionId &&
                         tc.SubjectId == teacherClassDto.SubjectId &&
                         tc.Id != id &&
                         !tc.IsDeleted);

                if (duplicateAssignment.Any())
                {
                    throw new InvalidOperationException("This teacher is already assigned to teach this subject in the specified class and section.");
                }
            }

            _mapper.Map(teacherClassDto, existingTeacherClass);
            existingTeacherClass.ModifiedBy = userId;
            existingTeacherClass.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherClasses.Update(existingTeacherClass);
            await _unitOfWork.CompleteAsync();

            var updatedTeacherClass = await _unitOfWork.TeacherClasses.GetByIdAsync(id);
            return updatedTeacherClass == null 
                ? null 
                : _mapper.Map<TeacherClassResponseDto>(updatedTeacherClass);
        }

        public async Task<bool> RemoveTeacherClassAsync(Guid id, Guid userId)
        {
            var teacherClass = await _unitOfWork.TeacherClasses.GetByIdAsync(id);
            if (teacherClass == null || teacherClass.IsDeleted)
                return false;

            // Soft delete
            teacherClass.IsDeleted = true;
            teacherClass.ModifiedBy = userId;
            teacherClass.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherClasses.Update(teacherClass);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> TeacherTeachesInClassAsync(Guid teacherId, Guid classId)
        {
            return await _unitOfWork.TeacherClasses.TeacherTeachesInClassAsync(teacherId, classId);
        }

        public async Task<bool> TeacherTeachesSubjectAsync(Guid teacherId, Guid subjectId)
        {
            return await _unitOfWork.TeacherClasses.TeacherTeachesSubjectAsync(teacherId, subjectId);
        }
    }
}
