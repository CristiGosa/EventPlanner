using AutoMapper;

using EventPlanner.Domain.Entities;

namespace EventPlanner.Business.UseCases.Login;
public sealed class ExternalAuthenticationMapper : Profile
{
	public ExternalAuthenticationMapper()
	{
		CreateMap<UserInfo, User>()
			.ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(x => x.Surname, opt => opt.MapFrom(src => src.Surname))
			.ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(x => x.UserName, opt => opt.MapFrom(src => src.Email));
	}
}
