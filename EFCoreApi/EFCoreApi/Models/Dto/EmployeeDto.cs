using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApi.Models.Dto
{
    public class EmployeeDto
    {
        
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public int? Age { get; set; }
    }
}
