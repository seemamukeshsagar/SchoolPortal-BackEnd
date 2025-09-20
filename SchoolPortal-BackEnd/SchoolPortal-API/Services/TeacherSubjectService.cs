using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherSubject;
using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class TeacherSubjectService : ITeacherSubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherSubjectService> _logger;

        public TeacherSubjectService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<TeacherSubjectService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TeacherSubjectResponseDto>> GetAllTeacherSubjectsAsync()
        {
            var teacherSubjects = await _unitOfWork.TeacherSubjects
                .FindAsync(ts => !ts.IsDeleted);
                
            return _mapper.Map<IEnumerable<TeacherSubjectResponseDto>>(teacherSubjects);
        }

        public async Task<TeacherSubjectResponseDto?> GetTeacherSubjectByIdAsync(Guid id)
        {
            var teacherSubject = await _unitOfWork.TeacherSubjects.GetByIdAsync(id);
            if (teacherSubject == null || teacherSubject.IsDeleted)
                return null;
                
            return _mapper.Map<TeacherSubjectResponseDto>(teacherSubject);
        }

        public async Task<IEnumerable<TeacherSubjectResponseDto>> GetSubjectsByTeacherIdAsync(Guid teacherId)
        {
            var teacherSubjects = await _unitOfWork.TeacherSubjects.GetByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<TeacherSubjectResponseDto>>(teacherSubjects);
        }

        public async Task<IEnumerable<TeacherSubjectResponseDto>> GetTeachersBySubjectIdAsync(Guid subjectId)
        {
            var teacherSubjects = await _unitOfWork.TeacherSubjects.GetBySubjectIdAsync(subjectId);
            return _mapper.Map<IEnumerable<TeacherSubjectResponseDto>>(teacherSubjects);
        }

        public async Task<IEnumerable<TeacherSubjectResponseDto>> GetByTeacherAndClassAsync(Guid teacherId, Guid classId)
        {
            var teacherSubjects = await _unitOfWork.TeacherSubjects.GetByTeacherAndClassAsync(teacherId, classId);
            return _mapper.Map<IEnumerable<TeacherSubjectResponseDto>>(teacherSubjects);
        }

        public async Task<TeacherSubjectResponseDto?> AddTeacherSubjectAsync(TeacherSubjectDto teacherSubjectDto, Guid userId)
        {
            // Check if the teacher is already assigned to this subject in the same class
            var isAssigned = await _unitOfWork.TeacherSubjects.IsTeacherAssignedToSubjectAsync(
                teacherSubjectDto.TeacherId, 
                teacherSubjectDto.SubjectId, 
                teacherSubjectDto.ClassMasterId);

            if (isAssigned)
            {
                throw new InvalidOperationException("This teacher is already assigned to teach this subject in the specified class.");
            }

            var teacherSubject = _mapper.Map<TeacherSubjectDetail>(teacherSubjectDto);
            teacherSubject.Id = Guid.NewGuid();
            teacherSubject.CreatedBy = userId;
            teacherSubject.CreatedDate = DateTime.UtcNow;
            teacherSubject.IsActive = true;
            teacherSubject.IsDeleted = false;

            await _unitOfWork.TeacherSubjects.AddAsync(teacherSubject);
            await _unitOfWork.CompleteAsync();

            var createdTeacherSubject = await _unitOfWork.TeacherSubjects.GetByIdAsync(teacherSubject.Id);
            return createdTeacherSubject == null 
                ? null 
                : _mapper.Map<TeacherSubjectResponseDto>(createdTeacherSubject);
        }

        public async Task<TeacherSubjectResponseDto?> UpdateTeacherSubjectAsync(
            Guid id, 
            TeacherSubjectDto teacherSubjectDto, 
            Guid userId)
        {
            var existingTeacherSubject = await _unitOfWork.TeacherSubjects.GetByIdAsync(id);
            if (existingTeacherSubject == null || existingTeacherSubject.IsDeleted)
                return null;

            // Check if the teacher is already assigned to this subject in the same class
            if (existingTeacherSubject.SubjectId != teacherSubjectDto.SubjectId ||
                existingTeacherSubject.ClassMasterId != teacherSubjectDto.ClassMasterId ||
                existingTeacherSubject.TeacherId != teacherSubjectDto.TeacherId)
            {
                var isAssigned = await _unitOfWork.TeacherSubjects.IsTeacherAssignedToSubjectAsync(
                    teacherSubjectDto.TeacherId, 
                    teacherSubjectDto.SubjectId, 
                    teacherSubjectDto.ClassMasterId);

                if (isAssigned)
                {
                    throw new InvalidOperationException("This teacher is already assigned to teach this subject in the specified class.");
                }
            }

            _mapper.Map(teacherSubjectDto, existingTeacherSubject);
            existingTeacherSubject.ModifiedBy = userId;
            existingTeacherSubject.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherSubjects.Update(existingTeacherSubject);
            await _unitOfWork.CompleteAsync();

            var updatedTeacherSubject = await _unitOfWork.TeacherSubjects.GetByIdAsync(id);
            return updatedTeacherSubject == null 
                ? null 
                : _mapper.Map<TeacherSubjectResponseDto>(updatedTeacherSubject);
        }

        public async Task<bool> RemoveTeacherSubjectAsync(Guid id, Guid userId)
        {
            var teacherSubject = await _unitOfWork.TeacherSubjects.GetByIdAsync(id);
            if (teacherSubject == null || teacherSubject.IsDeleted)
                return false;

            // Soft delete
            teacherSubject.IsDeleted = true;
            teacherSubject.ModifiedBy = userId;
            teacherSubject.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherSubjects.Update(teacherSubject);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> IsTeacherAssignedToSubjectAsync(Guid teacherId, Guid subjectId, Guid classId)
        {
            return await _unitOfWork.TeacherSubjects.IsTeacherAssignedToSubjectAsync(teacherId, subjectId, classId);
        }
    }
}
