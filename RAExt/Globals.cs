
using AutoMapper;

namespace RAExt
{
	public static class Globals
	{
		private static bool IsMapperInitialized = false;
		private static IMapper mapper;
		
		public static IMapper InitMapper()
		{
			MapperConfiguration conf = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ItemDataUnmanaged, ItemData>(MemberList.Source)
					.IgnoreAllPropertiesWithAnInaccessibleSetter();
				cfg.CreateMap<ItemData, ItemDataUnmanaged>(MemberList.Destination);
			});
			conf.AssertConfigurationIsValid();
			mapper = conf.CreateMapper();
			IsMapperInitialized = true;
			return mapper;
		}



		public static IMapper RMapper 
			=> (IsMapperInitialized ? mapper : InitMapper());
	}
}
