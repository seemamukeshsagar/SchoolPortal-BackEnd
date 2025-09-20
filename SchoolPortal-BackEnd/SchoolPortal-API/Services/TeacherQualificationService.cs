using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.TeacherQualification;
using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class TeacherQualificationService : ITeacherQualificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TeacherQualificationService> _logger;

        public TeacherQualificationService(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<TeacherQualificationService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<TeacherQualificationResponseDto>> GetAllQualificationsAsync()
        {
            var qualifications = await _unitOfWork.TeacherQualifications
                .FindAsync(tq => !tq.IsDeleted);
                
            return _mapper.Map<IEnumerable<TeacherQualificationResponseDto>>(qualifications);
        }

        public async Task<TeacherQualificationResponseDto?> GetQualificationByIdAsync(Guid id)
        {
            var qualification = await _unitOfWork.TeacherQualifications.GetByIdAsync(id);
            if (qualification == null || qualification.IsDeleted)
                return null;
                
            return _mapper.Map<TeacherQualificationResponseDto>(qualification);
        }

        public async Task<IEnumerable<TeacherQualificationResponseDto>> GetQualificationsByTeacherIdAsync(Guid teacherId)
        {
            var qualifications = await _unitOfWork.TeacherQualifications.GetByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<TeacherQualificationResponseDto>>(qualifications);
        }

        public async Task<IEnumerable<TeacherQualificationResponseDto>> GetActiveQualificationsByTeacherIdAsync(Guid teacherId)
        {
            var qualifications = await _unitOfWork.TeacherQualifications.GetActiveQualificationsByTeacherIdAsync(teacherId);
            return _mapper.Map<IEnumerable<TeacherQualificationResponseDto>>(qualifications);
        }

        public async Task<TeacherQualificationResponseDto?> AddQualificationAsync(TeacherQualificationDto qualificationDto, Guid userId)
        {
            // Check if the teacher already has this qualification
            if (await _unitOfWork.TeacherQualifications.TeacherHasQualificationAsync(
                qualificationDto.TeacherId, 
                qualificationDto.QualificationId))
            {
                throw new InvalidOperationException("This teacher already has the specified qualification.");
            }

            var qualification = _mapper.Map<TeacherQualificationDetail>(qualificationDto);
            qualification.Id = Guid.NewGuid();
            qualification.CreatedBy = userId;
            qualification.CreatedDate = DateTime.UtcNow;
            qualification.IsActive = true;
            qualification.IsDeleted = false;

            await _unitOfWork.TeacherQualifications.AddAsync(qualification);
            await _unitOfWork.CompleteAsync();

            var createdQualification = await _unitOfWork.TeacherQualifications.GetByIdAsync(qualification.Id);
            return createdQualification == null 
                ? null 
                : _mapper.Map<TeacherQualificationResponseDto>(createdQualification);
        }

        public async Task<TeacherQualificationResponseDto?> UpdateQualificationAsync(
            Guid id, 
            TeacherQualificationDto qualificationDto, 
            Guid userId)
        {
            var existingQualification = await _unitOfWork.TeacherQualifications.GetByIdAsync(id);
            if (existingQualification == null || existingQualification.IsDeleted)
                return null;

            // Check if updating to a qualification the teacher already has
            if (existingQualification.QualifcationId != qualificationDto.QualificationId &&
                await _unitOfWork.TeacherQualifications.TeacherHasQualificationAsync(
                    qualificationDto.TeacherId, 
                    qualificationDto.QualificationId))
            {
                throw new InvalidOperationException("This teacher already has the specified qualification.");
            }

            _mapper.Map(qualificationDto, existingQualification);
            existingQualification.ModifiedBy = userId;
            existingQualification.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherQualifications.Update(existingQualification);
            await _unitOfWork.CompleteAsync();

            var updatedQualification = await _unitOfWork.TeacherQualifications.GetByIdAsync(id);
            return updatedQualification == null 
                ? null 
                : _mapper.Map<TeacherQualificationResponseDto>(updatedQualification);
        }

        public async Task<bool> RemoveQualificationAsync(Guid id, Guid userId)
        {
            var qualification = await _unitOfWork.TeacherQualifications.GetByIdAsync(id);
            if (qualification == null || qualification.IsDeleted)
                return false;

            // Soft delete
            qualification.IsDeleted = true;
            qualification.ModifiedBy = userId;
            qualification.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.TeacherQualifications.Update(qualification);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> TeacherHasQualificationAsync(Guid teacherId, Guid qualificationId)
        {
            return await _unitOfWork.TeacherQualifications.TeacherHasQualificationAsync(teacherId, qualificationId);
        }
    }
}
