using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolPortal_API.ViewModels.State
{
    public class StateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        
        [Required]
        public Guid CountryId { get; set; }
        
        [StringLength(10)]
        public string StateCode { get; set; }
        
        [StringLength(10)]
        public string Type { get; set; } // e.g., State, Province, Region
        
        [StringLength(10)]
        public string Latitude { get; set; }
        
        [StringLength(10)]
        public string Longitude { get; set; }
        
        public bool IsActive { get; set; } = true;
    }

    public class StateResponseDto : StateDto
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
