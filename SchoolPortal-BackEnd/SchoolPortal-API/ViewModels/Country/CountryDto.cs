using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.Country
{
    public class CountryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        [StringLength(3)]
        public string CountryCode { get; set; }
        
        [StringLength(10)]
        public string PhoneCode { get; set; }
        
        [StringLength(10)]
        public string Currency { get; set; }
        
        [StringLength(10)]
        public string CurrencySymbol { get; set; }
        
        [StringLength(100)]
        public string NativeName { get; set; }
        
        [StringLength(100)]
        public string Region { get; set; }
        
        public bool IsActive { get; set; } = true;
    }

    public class CountryResponseDto : CountryDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
