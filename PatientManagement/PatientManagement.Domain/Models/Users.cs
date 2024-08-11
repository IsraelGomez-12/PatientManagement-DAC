using System;
namespace PatientManagement.Domain.Models
{
	public class Users
	{
		public Users()
		{
		}

		public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
		public bool Status { get; set; }

    }
}

