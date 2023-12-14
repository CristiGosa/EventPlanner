using AutoMapper;

namespace EventPlanner.Business.Extensions;
public static class MapperExtension
{
	public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source, IMapper _mapper) where TDestination : new()
	{
		return _mapper.Map(source, destination);
	}
}