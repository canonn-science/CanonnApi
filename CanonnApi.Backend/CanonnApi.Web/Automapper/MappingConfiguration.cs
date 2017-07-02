using System;
using System.Net;
using AutoMapper;
using CanonnApi.Web.Controllers.Models;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Services.Maps;
using CanonnApi.Web.Services.RuinSites;
using Microsoft.Extensions.Configuration;

namespace CanonnApi.Web.Automapper
{
	/// <summary>
	/// The Automapper mapping profile used for the Canonn API.
	/// </summary>
	public class MappingConfiguration
	{
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Creates a new instance of the mapping configuration
		/// </summary>
		/// <param name="configuration"></param>
		public MappingConfiguration(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// Configures the AutoMapper
		/// </summary>
		/// <param name="config"></param>
		public void Configure(IMapperConfigurationExpression config)
		{
			config.CreateMap<Artifact, ArtifactDto>().ReverseMap();

			config.CreateMap<Body, BodyDto>().ReverseMap();
			config.CreateMap<Body, EdsmUpdatedBody>();

			config.CreateMap<CodexCategory, CodexCategoryDto>().ReverseMap();

			config.CreateMap<CodexData, CodexDataDto>().ReverseMap();

			config.CreateMap<Obelisk, ObeliskDto>().ReverseMap();

			config.CreateMap<ObeliskGroup, ObeliskGroupDto>().ReverseMap();

			config.CreateMap<RuinType, RuinTypeDto>().ReverseMap();

			config.CreateMap<DatabaseModels.System, SystemDto>().ReverseMap();
			config.CreateMap<DatabaseModels.System, EdsmUpdatedSystem>();
			config.CreateMap<DatabaseModels.System, MapsSystem>()
				.ForMember(ms => ms.SystemId, sys => sys.MapFrom(o => o.Id))
				.ForMember(ms => ms.SystemName, sys => sys.MapFrom(o => o.Name));

			config.CreateMap<RuinSite, MapsRuins>()
				.ForMember(rs => rs.RuinId, mr => mr.MapFrom(r => r.Id))
				.ForMember(rs => rs.BodyName, mr => mr.MapFrom(r => r.Body.Name))
				.ForMember(rs => rs.RuinTypeName, mr => mr.MapFrom(r => r.Ruintype.Name))
				.ForMember(rs => rs.Coordinates, mr => mr.MapFrom(r => new[] { r.Latitude, r.Longitude }))
				.ForMember(rs => rs.EdsmBodyLink, mr => mr.MapFrom(r => r.Body.EdsmExtId.HasValue && r.Body.System.EdsmExtId.HasValue && !String.IsNullOrWhiteSpace(r.Body.Name) && !String.IsNullOrWhiteSpace(r.Body.System.Name)
					? String.Format(_configuration.GetSection("externalLinks:edsmBody").Value, r.Body.System.EdsmExtId, WebUtility.UrlEncode(r.Body.System.Name), r.Body.EdsmExtId, WebUtility.UrlEncode(r.Body.Name))
					: null)
				);

			config.CreateMap<RuinSite, RuinSiteDto>().ReverseMap();
			config.CreateMap<ObeliskWithActiveState, ObeliskWithActiveStateDto>().ReverseMap();
			config.CreateMap<ObeliskGroupWithActiveState, ObeliskGroupWithActiveStateDto>().ReverseMap();
			config.CreateMap<RuinSiteWithObeliskData, RuinSiteWithObeliskDataDto>()
				.ForMember(dto => dto.ObeliskGroups, c => c.MapFrom(m => m.ObeliskGroups))
				.ForMember(dto => dto.Obelisks, c => c.MapFrom(m => m.Obelisks))
				.ReverseMap();

			config.CreateMap<MapsRuins, MapsRuinDto>();
			
			config.CreateMap<MapsSystem, MapsSystemDto>()
				.ForMember(dto => dto.Ruins, c => c.MapFrom(m => m.Ruins))
				.ForMember(dto => dto.EdsmSystemLink, record => record.MapFrom(src => (src.EdsmExtId.HasValue && !String.IsNullOrWhiteSpace(src.SystemName))
					? String.Format(_configuration.GetSection("externalLinks:edsmSystem").Value, src.EdsmExtId, WebUtility.UrlEncode(src.SystemName))
					: null)
				);
		}
	}
}
