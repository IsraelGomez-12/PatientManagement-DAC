using System;
namespace PatientManagement.Domain.Models
{
	public class Patient
    {
		public Patient()
		{
		}

        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string IDNumber { get; set; }
        public string Comments { get; set; }
    }
}

