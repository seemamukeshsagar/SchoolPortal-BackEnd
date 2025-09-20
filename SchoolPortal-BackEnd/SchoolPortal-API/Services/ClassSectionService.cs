using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.ClassSection;
using SchoolPortal.Shared.Models;
using System;

namespace SchoolPortal_API.Services
{
    public class ClassSectionService : IClassSectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ClassSectionService> _logger;

        public ClassSectionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ClassSectionService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ClassSectionResponseDto>> GetAllAsync()
        {
            var items = await _unitOfWork.ClassSections.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassSectionResponseDto>>(items);
        }

        public async Task<ClassSectionResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _unitOfWork.ClassSections.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<ClassSectionResponseDto>(item);
        }

        public async Task<IEnumerable<ClassSectionResponseDto>> GetBySchoolIdAsync(Guid schoolId)
        {
            var items = await _unitOfWork.ClassSections.GetBySchoolIdAsync(schoolId);
            return _mapper.Map<IEnumerable<ClassSectionResponseDto>>(items);
        }

        public async Task<IEnumerable<ClassSectionResponseDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var items = await _unitOfWork.ClassSections.GetByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<ClassSectionResponseDto>>(items);
        }

        public async Task<IEnumerable<ClassSectionResponseDto>> GetByClassIdAsync(Guid classId)
        {
            var items = await _unitOfWork.ClassSections.GetByClassIdAsync(classId);
            return _mapper.Map<IEnumerable<ClassSectionResponseDto>>(items);
        }

        public async Task<IEnumerable<ClassSectionResponseDto>> GetBySectionIdAsync(Guid sectionId)
        {
            var items = await _unitOfWork.ClassSections.GetBySectionIdAsync(sectionId);
            return _mapper.Map<IEnumerable<ClassSectionResponseDto>>(items);
        }

        public async Task<ClassSectionResponseDto?> CreateAsync(ClassSectionDto dto, Guid userId)
        {
            if (await _unitOfWork.ClassSections.ExistsAsync(dto.ClassMasterId, dto.SectionMasterId, dto.SchoolId, dto.CompanyId))
            {
                throw new InvalidOperationException("This class-section mapping already exists for the specified school and company.");
            }

            var entity = _mapper.Map<ClassSectionDetail>(dto);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = userId;
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsDeleted = false;

            await _unitOfWork.ClassSections.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var created = await _unitOfWork.ClassSections.GetByIdAsync(entity.Id);
            return created == null ? null : _mapper.Map<ClassSectionResponseDto>(created);
        }

        public async Task<ClassSectionResponseDto?> UpdateAsync(Guid id, ClassSectionDto dto, Guid userId)
        {
            var existing = await _unitOfWork.ClassSections.GetByIdAsync(id);
            if (existing == null) return null;

            if (existing.ClassMasterId != dto.ClassMasterId ||
                existing.SectionMasterId != dto.SectionMasterId ||
                existing.SchoolId != dto.SchoolId ||
                existing.CompanyId != dto.CompanyId)
            {
                if (await _unitOfWork.ClassSections.ExistsAsync(dto.ClassMasterId, dto.SectionMasterId, dto.SchoolId, dto.CompanyId, id))
                {
                    throw new InvalidOperationException("This class-section mapping already exists for the specified school and company.");
                }
            }

            _mapper.Map(dto, existing);
            existing.ModifiedBy = userId;
            existing.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.ClassSections.Update(existing);
            await _unitOfWork.CompleteAsync();

            var updated = await _unitOfWork.ClassSections.GetByIdAsync(id);
            return updated == null ? null : _mapper.Map<ClassSectionResponseDto>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var existing = await _unitOfWork.ClassSections.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsDeleted = true;
            existing.ModifiedBy = userId;
            existing.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.ClassSections.Update(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
