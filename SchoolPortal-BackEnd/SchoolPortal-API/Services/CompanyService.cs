using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolPortal_API.Interfaces;
using SchoolPortal_API.ViewModels.Company;
using SchoolPortal.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal_API.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CompanyService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CompanyResponseDto>> GetAllCompaniesAsync()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyResponseDto>>(companies);
        }

        public async Task<CompanyResponseDto?> GetCompanyByIdAsync(Guid id)
        {
            var company = await _unitOfWork.Companies.GetCompanyWithDetailsAsync(id);
            if (company == null)
                return null;
                
            return _mapper.Map<CompanyResponseDto>(company);
        }

        public async Task<CompanyResponseDto?> CreateCompanyAsync(CompanyDto companyDto, Guid userId)
        {
            var company = _mapper.Map<CompanyMaster>(companyDto);
            company.Id = Guid.NewGuid();
            company.CreatedBy = userId;
            company.CreatedDate = DateTime.UtcNow;
            company.IsDeleted = false;

            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.CompleteAsync();

            var createdCompany = await _unitOfWork.Companies.GetCompanyWithDetailsAsync(company.Id);
            return createdCompany == null ? null : _mapper.Map<CompanyResponseDto>(createdCompany);
        }

        public async Task<CompanyResponseDto?> UpdateCompanyAsync(Guid id, CompanyDto companyDto, Guid userId)
        {
            var existingCompany = await _unitOfWork.Companies.GetByIdAsync(id);
            if (existingCompany == null)
                return null;

            _mapper.Map(companyDto, existingCompany);
            existingCompany.ModifiedBy = userId;
            existingCompany.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Companies.Update(existingCompany);
            await _unitOfWork.CompleteAsync();

            var updatedCompany = await _unitOfWork.Companies.GetCompanyWithDetailsAsync(id);
            return updatedCompany == null ? null : _mapper.Map<CompanyResponseDto>(updatedCompany);
        }

        public async Task<bool> DeleteCompanyAsync(Guid id, Guid userId)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company == null)
                return false;

            // Soft delete
            company.IsDeleted = true;
            company.ModifiedBy = userId;
            company.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Companies.Update(company);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<dynamic>> GetCountriesAsync()
        {
            return await _unitOfWork.Companies.GetCountriesAsync();
        }

        public async Task<IEnumerable<dynamic>> GetStatesByCountryIdAsync(Guid countryId)
        {
            return await _unitOfWork.Companies.GetStatesByCountryIdAsync(countryId);
        }

        public async Task<IEnumerable<dynamic>> GetCitiesByStateIdAsync(Guid stateId)
        {
            return await _unitOfWork.Companies.GetCitiesByStateIdAsync(stateId);
        }
    }
}
