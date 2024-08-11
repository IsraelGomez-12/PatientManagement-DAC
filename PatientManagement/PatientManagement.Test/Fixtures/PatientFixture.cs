using System;
using System.Threading;
using PatientManagement.Application.DTOs;
using PatientManagement.Domain.Models;

namespace PatientManagement.Test.Fixtures
{
	public class PatientFixture
	{
		public PatientFixture()
        {
        }

        public static IEnumerable<PatientDTO> GetPatientMock()
        {
            return new[]
            {
            new PatientDTO
            {
                  PatientId = 1,
                  FirstName = "John Test",
                  LastName = "Smith",
                  DateOfBirth = DateTime.Now,
                  Email = "test@gmail.com",
                  Phone = "8900987373",
                  Address = "test, test , test ",
                  SocialSecurityNumber = "PUQXYKN/SQZwtOWx7XwIrw==",
                  IDNumber = "402300919682",
                  Comments = "He got pulmonar cancer"
            },
            
            new PatientDTO
            {
                  PatientId = 2,
                  FirstName = "John Test",
                  LastName = "Smith",
                  DateOfBirth = DateTime.Now,
                  Email = "test@gmail.com",
                  Phone = "8900987373",
                  Address = "test, test , test ",
                  SocialSecurityNumber = "787235235NJ",
                  IDNumber = "402300919682",
                  Comments = "He got pulmonar cancer"
            
            },
            new PatientDTO
            {
                  PatientId = 3,
                  FirstName = "John Test Test",
                  LastName = "Smith",
                  DateOfBirth = DateTime.Now,
                  Email = "tegmail.com",
                  Phone = "8900987373",
                  Address = "test, test , test ",
                  SocialSecurityNumber = "787235235NJ",
                  IDNumber = "402300919682",
                  Comments = "He got pulmonar cancer"
            },
      
        };
        }
    }
}

