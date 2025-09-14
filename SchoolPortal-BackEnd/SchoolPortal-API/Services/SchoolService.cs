using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal.Shared.Models;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SchoolService> _logger;

        public SchoolService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SchoolService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<SchoolResponseDto>> GetAllSchoolsAsync()
        {
            var schools = await _unitOfWork.Schools.GetAllAsync();
            return _mapper.Map<IEnumerable<SchoolResponseDto>>(schools);
        }

        public async Task<IEnumerable<SchoolResponseDto>> GetSchoolsByCompanyIdAsync(Guid companyId)
        {
            var schools = await _unitOfWork.Schools.GetSchoolsByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<SchoolResponseDto>>(schools);
        }

        public async Task<SchoolResponseDto?> GetSchoolByIdAsync(Guid id)
        {
            var school = await _unitOfWork.Schools.GetSchoolWithDetailsAsync(id);
            if (school == null)
                return null;
                
            return _mapper.Map<SchoolResponseDto>(school);
        }

        public async Task<SchoolResponseDto?> CreateSchoolAsync(SchoolDto schoolDto, Guid userId)
        {
            var school = _mapper.Map<SchoolMaster>(schoolDto);
            school.Id = Guid.NewGuid();
            school.CreatedBy = userId;
            school.CreatedDate = DateTime.UtcNow;
            school.IsDeleted = false;
            school.Status = "Active";

            await _unitOfWork.Schools.AddAsync(school);
            await _unitOfWork.CompleteAsync();

            var createdSchool = await _unitOfWork.Schools.GetSchoolWithDetailsAsync(school.Id);
            return createdSchool == null ? null : _mapper.Map<SchoolResponseDto>(createdSchool);
        }

        public async Task<SchoolResponseDto?> UpdateSchoolAsync(Guid id, SchoolDto schoolDto, Guid userId)
        {
            var existingSchool = await _unitOfWork.Schools.GetByIdAsync(id);
            if (existingSchool == null)
                return null;

            _mapper.Map(schoolDto, existingSchool);
            existingSchool.ModifiedBy = userId;
            existingSchool.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Schools.Update(existingSchool);
            await _unitOfWork.CompleteAsync();

            var updatedSchool = await _unitOfWork.Schools.GetSchoolWithDetailsAsync(id);
            return _mapper.Map<SchoolResponseDto>(updatedSchool);
        }

        public async Task<bool> DeleteSchoolAsync(Guid id, Guid userId)
        {
            var school = await _unitOfWork.Schools.GetByIdAsync(id);
            if (school == null)
                return false;

            // Soft delete
            school.IsDeleted = true;
            school.ModifiedBy = userId;
            school.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Schools.Update(school);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
