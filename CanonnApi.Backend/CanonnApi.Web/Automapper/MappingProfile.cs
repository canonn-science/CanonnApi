using AutoMapper;
using CanonnApi.Web.Controllers.Models;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.Maps;
using CanonnApi.Web.Services.RuinSites;

namespace CanonnApi.Web.Automapper
{
	/// <summary>
	/// The Automapper mapping profile used for the Canonn API.
	/// </summary>
	public class MappingProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MappingProfile"/>.
		/// </summary>
		public MappingProfile()
		{
			CreateMap<Artifact, ArtifactDto>().ReverseMap();

			CreateMap<Body, BodyDto>().ReverseMap();
			CreateMap<Body, EdsmUpdatedBody>();

			CreateMap<CodexCategory, CodexCategoryDto>().ReverseMap();

			CreateMap<CodexData, CodexDataDto>().ReverseMap();

			CreateMap<Obelisk, ObeliskDto>().ReverseMap();

			CreateMap<ObeliskGroup, ObeliskGroupDto>().ReverseMap();

			CreateMap<RuinType, RuinTypeDto>().ReverseMap();

			CreateMap<DatabaseModels.System, SystemDto>().ReverseMap();
			CreateMap<DatabaseModels.System, EdsmUpdatedSystem>();

			CreateMap<RuinSite, RuinSiteDto>().ReverseMap();
			CreateMap<ObeliskWithActiveState, ObeliskWithActiveStateDto>().ReverseMap();
			CreateMap<ObeliskGroupWithActiveState, ObeliskGroupWithActiveStateDto>().ReverseMap();
			CreateMap<RuinSiteWithObeliskData, RuinSiteWithObeliskDataDto>()
				.ForMember(dto => dto.ObeliskGroups, c => c.MapFrom(m => m.ObeliskGroups))
				.ForMember(dto => dto.Obelisks, c => c.MapFrom(m => m.Obelisks))
				.ReverseMap();

			CreateMap<MapsRuins, MapsRuinDto>();
			CreateMap<MapsSystem, MapsSystemDto>()
				.ForMember(dto => dto.Ruins, c => c.MapFrom(m => m.Ruins));
		}
	}
}
