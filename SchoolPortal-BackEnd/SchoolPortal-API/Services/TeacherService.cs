using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Teacher;
using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherService> _logger;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TeacherService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TeacherResponseDto>> GetAllTeachersAsync()
        {
            var teachers = await _unitOfWork.Teachers.FindAsync(t => !t.IsDeleted);
            return _mapper.Map<IEnumerable<TeacherResponseDto>>(teachers);
        }

        public async Task<TeacherResponseDto?> GetTeacherByIdAsync(Guid id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null || teacher.IsDeleted)
                return null;
                
            return _mapper.Map<TeacherResponseDto>(teacher);
        }

        public async Task<TeacherResponseDto?> CreateTeacherAsync(TeacherDto teacherDto, Guid userId)
        {
            // Check if email is already in use
            if (!await _unitOfWork.Teachers.IsEmailUniqueAsync(teacherDto.Email))
            {
                throw new InvalidOperationException("Email is already in use by another teacher.");
            }

            var teacher = _mapper.Map<TeacherMaster>(teacherDto);
            teacher.Id = Guid.NewGuid();
            teacher.CreatedBy = userId;
            teacher.CreatedDate = DateTime.UtcNow;
            teacher.IsActive = true;
            teacher.IsDeleted = false;

            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.CompleteAsync();

            var createdTeacher = await _unitOfWork.Teachers.GetByIdAsync(teacher.Id);
            return createdTeacher == null ? null : _mapper.Map<TeacherResponseDto>(createdTeacher);
        }

        public async Task<TeacherResponseDto?> UpdateTeacherAsync(Guid id, TeacherDto teacherDto, Guid userId)
        {
            var existingTeacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (existingTeacher == null || existingTeacher.IsDeleted)
                return null;

            // Check if email is already in use by another teacher
            if (!await _unitOfWork.Teachers.IsEmailUniqueAsync(teacherDto.Email, id))
            {
                throw new InvalidOperationException("Email is already in use by another teacher.");
            }

            _mapper.Map(teacherDto, existingTeacher);
            existingTeacher.ModifiedBy = userId;
            existingTeacher.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Teachers.Update(existingTeacher);
            await _unitOfWork.CompleteAsync();

            var updatedTeacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            return updatedTeacher == null ? null : _mapper.Map<TeacherResponseDto>(updatedTeacher);
        }

        public async Task<bool> DeleteTeacherAsync(Guid id, Guid userId)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null || teacher.IsDeleted)
                return false;

            // Soft delete
            teacher.IsDeleted = true;
            teacher.ModifiedBy = userId;
            teacher.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Teachers.Update(teacher);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<TeacherResponseDto>> GetTeachersBySchoolIdAsync(Guid schoolId)
        {
            var teachers = await _unitOfWork.Teachers.GetBySchoolIdAsync(schoolId);
            return _mapper.Map<IEnumerable<TeacherResponseDto>>(teachers);
        }

        public async Task<IEnumerable<TeacherResponseDto>> GetTeachersByCompanyIdAsync(Guid companyId)
        {
            var teachers = await _unitOfWork.Teachers.GetByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<TeacherResponseDto>>(teachers);
        }

        public async Task<TeacherResponseDto?> GetTeacherByEmailAsync(string email)
        {
            var teacher = await _unitOfWork.Teachers.GetByEmailAsync(email);
            return teacher == null ? null : _mapper.Map<TeacherResponseDto>(teacher);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, Guid? excludeId = null)
        {
            return await _unitOfWork.Teachers.IsEmailUniqueAsync(email, excludeId);
        }

        public async Task<IEnumerable<TeacherResponseDto>> SearchTeachersAsync(string searchTerm, Guid? companyId = null, Guid? schoolId = null)
        {
            var teachers = await _unitOfWork.Teachers.SearchTeachersAsync(searchTerm, companyId, schoolId);
            return _mapper.Map<IEnumerable<TeacherResponseDto>>(teachers);
        }
    }
}
