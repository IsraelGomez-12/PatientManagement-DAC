using System;
using PatientManagement.Application.DTOs;

namespace PatientManagement.Test.Fixtures
{
	public class LoginFixture
	{
		public LoginFixture()
		{
		}

        public static LoginDTO GetLoginMock()
		{
			return new LoginDTO()
			{
				Username = "johndoe",
				Password = "password123"
			};
		}


    }
}

