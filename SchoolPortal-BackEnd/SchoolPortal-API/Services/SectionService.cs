using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Section;
using SchoolPortal.Shared.Models;
using System;

namespace SchoolPortal_API.Services
{
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SectionService> _logger;

        public SectionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SectionService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<SectionResponseDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Sections.GetAllAsync();
            return _mapper.Map<IEnumerable<SectionResponseDto>>(items);
        }

        public async Task<SectionResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _unitOfWork.Sections.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<SectionResponseDto>(item);
        }

        public async Task<IEnumerable<SectionResponseDto>> GetBySchoolIdAsync(Guid schoolId)
        {
            var items = await _unitOfWork.Sections.GetBySchoolIdAsync(schoolId);
            return _mapper.Map<IEnumerable<SectionResponseDto>>(items);
        }

        public async Task<IEnumerable<SectionResponseDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var items = await _unitOfWork.Sections.GetByCompanyIdAsync(companyId);
            return _mapper.Map<IEnumerable<SectionResponseDto>>(items);
        }

        public async Task<SectionResponseDto?> CreateAsync(SectionDto dto, Guid userId)
        {
            if (await _unitOfWork.Sections.NameExistsAsync(dto.Name, dto.SchoolId, dto.CompanyId))
            {
                throw new InvalidOperationException("A section with the same name already exists for this school and company.");
            }

            var entity = _mapper.Map<SectionMaster>(dto);
            entity.Id = Guid.NewGuid();
            entity.CreatedBy = userId;
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsDeleted = false;

            await _unitOfWork.Sections.AddAsync(entity);
            await _unitOfWork.CompleteAsync();

            var created = await _unitOfWork.Sections.GetByIdAsync(entity.Id);
            return created == null ? null : _mapper.Map<SectionResponseDto>(created);
        }

        public async Task<SectionResponseDto?> UpdateAsync(Guid id, SectionDto dto, Guid userId)
        {
            var existing = await _unitOfWork.Sections.GetByIdAsync(id);
            if (existing == null) return null;

            if (!string.Equals(existing.Name, dto.Name, StringComparison.OrdinalIgnoreCase) ||
                existing.SchoolId != dto.SchoolId ||
                existing.CompanyId != dto.CompanyId)
            {
                if (await _unitOfWork.Sections.NameExistsAsync(dto.Name, dto.SchoolId, dto.CompanyId, id))
                {
                    throw new InvalidOperationException("A section with the same name already exists for this school and company.");
                }
            }

            _mapper.Map(dto, existing);
            existing.ModifiedBy = userId;
            existing.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Sections.Update(existing);
            await _unitOfWork.CompleteAsync();

            var updated = await _unitOfWork.Sections.GetByIdAsync(id);
            return updated == null ? null : _mapper.Map<SectionResponseDto>(updated);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var existing = await _unitOfWork.Sections.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsDeleted = true;
            existing.ModifiedBy = userId;
            existing.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Sections.Update(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
