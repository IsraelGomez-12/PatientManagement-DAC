using System;
using AutoMapper;
using PatientManagement.Domain.Models;

namespace PatientManagement.Application.DTOs
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
            CreateMap<Patient, PatientDTO>().ReverseMap();
			CreateMap<Users, LoginDTO>().ReverseMap();
        }
    }
}

