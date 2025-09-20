using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Class;
using SchoolPortal.Shared.Models;
using System;

namespace SchoolPortal_API.Services
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ClassService> _logger;

        public ClassService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ClassService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ClassResponseDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Classes.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassResponseDto>>(items);
        }

        public async Task<ClassResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _unitOfWork.Classes.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<ClassResponseDto>(item);
        }

        public async Task<IEnumerable<ClassResponseDto>> GetBySchoolIdAsync(Guid schoolId)
        {
            var items = await _unitOfWork.Classes.GetBySchoolIdAsync(schoolId);
            return _mapper.Map<IEnumerable<ClassResponseDto>>(items);
        }

        public async Task<IEnumerable<ClassResponseDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var items = await _unitOfWork.Classes.GetByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<ClassResponseDto>>(items);
        }

        public async Task<ClassResponseDto?> CreateAsync(ClassDto dto, Guid userId)
        {
            // Uniqueness per school + company
            if (await _unitOfWork.Classes.NameExistsAsync(dto.Name, dto.SchoolId, dto.CompanyId))
            {
                throw new InvalidOperationException("A class with the same name already exists for this school and company.");
            }

            var entity = _mapper.Map<ClassMaster>(dto);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = userId;
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsDeleted = false;

            await _unitOfWork.Classes.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var created = await _unitOfWork.Classes.GetByIdAsync(entity.Id);
            return created == null ? null : _mapper.Map<ClassResponseDto>(created);
        }

        public async Task<ClassResponseDto?> UpdateAsync(Guid id, ClassDto dto, Guid userId)
        {
            var existing = await _unitOfWork.Classes.GetByIdAsync(id);
            if (existing == null) return null;

            // If name/school/company changed, enforce uniqueness
            if (!string.Equals(existing.Name, dto.Name, StringComparison.OrdinalIgnoreCase) ||
                existing.SchoolId != dto.SchoolId ||
                existing.CompanyId != dto.CompanyId)
            {
                if (await _unitOfWork.Classes.NameExistsAsync(dto.Name, dto.SchoolId, dto.CompanyId, id))
                {
                    throw new InvalidOperationException("A class with the same name already exists for this school and company.");
                }
            }

            _mapper.Map(dto, existing);
            existing.ModifiedBy = userId;
            existing.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Classes.Update(existing);
            await _unitOfWork.CompleteAsync();

            var updated = await _unitOfWork.Classes.GetByIdAsync(id);
            return updated == null ? null : _mapper.Map<ClassResponseDto>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var existing = await _unitOfWork.Classes.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsDeleted = true;
            existing.ModifiedBy = userId;
            existing.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Classes.Update(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
