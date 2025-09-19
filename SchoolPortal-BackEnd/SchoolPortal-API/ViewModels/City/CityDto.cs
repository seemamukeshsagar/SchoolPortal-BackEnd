using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.City
{
    public class CityDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        public Guid StateId { get; set; }
        
        [StringLength(10)]
        public string CityCode { get; set; }
        
        [StringLength(10)]
        public string Latitude { get; set; }
        
        [StringLength(10)]
        public string Longitude { get; set; }
        
        [StringLength(10)]
        public string Timezone { get; set; }
        
        public bool IsActive { get; set; } = true;
    }

    public class CityResponseDto : CityDto
    {
        public Guid Id { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
