using System;
using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Application.DTOs
{
	public class PatientDTO
	{
		public PatientDTO()
		{
		}
        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string Address { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Social Security Number must be exactly 11 characters.")]
        public string SocialSecurityNumber { get; set; }

        [StringLength(20, ErrorMessage = "ID Number cannot be longer than 20 characters.")]
        public string IDNumber { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot be longer than 225 characters.")]
        public string Comments { get; set; }
    }
}

