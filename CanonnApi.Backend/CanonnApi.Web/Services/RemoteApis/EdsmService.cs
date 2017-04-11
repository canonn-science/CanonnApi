using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CanonnApi.Web.DatabaseModels;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CanonnApi.Web.Services.RemoteApis
{
	public class EdsmService : IEdsmService
	{
		private readonly ILogger _logger;

		public EdsmService(ILogger<EdsmService> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<IEnumerable<(DatabaseModels.System System, bool Updated, string ErrorMessage)>> FetchSystemIds(IEnumerable<DatabaseModels.System> systems)
		{
			const string baseUrl = "https://www.edsm.net/api-v1/systems?showId=1&systemName=";

			var result = new List<ValueTuple<DatabaseModels.System, bool, string>>();

			foreach (var system in systems)
			{
				var req = WebRequest.Create(baseUrl + system.Name);

				_logger.LogDebug("Trying to fetch data for system {Id}: {Name}", system.Id, system.Name);

				try
				{
					using (var response = await req.GetResponseAsync())
					using (var responseStream = response.GetResponseStream())
					using (var streamReader = new StreamReader(responseStream))
					{
						var data = streamReader.ReadToEnd();
						var returnedSystems = JsonConvert.DeserializeObject<EdsmSystemResponse[]>(data);

						_logger.LogTrace("Received data: {edsmData}", data);
						var edsmSys = returnedSystems.FirstOrDefault(s => s.Name.Equals(system.Name, StringComparison.OrdinalIgnoreCase));

						if (edsmSys != null)
						{
							system.EdsmExtId = edsmSys.Id;
							result.Add(new ValueTuple<DatabaseModels.System, bool, string>(system, true, null));
						}
						else
						{
							result.Add(new ValueTuple<DatabaseModels.System, bool, string>(system, false, "No system found in EDSM."));
						}
					}
				}
				catch (Exception ex)
				{
					result.Add(new ValueTuple<DatabaseModels.System, bool, string>(system, false, "Exception while updating data from EDSM: " + ex.Message));
				}
			}

			return result;
		}

		public async Task<IEnumerable<(Body Body, bool Updated, string ErrorMessage)>> FetchBodyIds(IEnumerable<Body> bodies)
		{
			const string baseUrl = "https://www.edsm.net/api-system-v1/bodies?systemName=";

			var result = new List<ValueTuple<Body, bool, string>>();

			foreach (var body in bodies)
			{
				_logger.LogDebug("Trying to fetch data for body {Id}: {Name}", body.Id, body.Name);
				var req = WebRequest.Create(baseUrl + body.System.Name);

				try
				{
					using (var response = await req.GetResponseAsync())
					using (var responseStream = response.GetResponseStream())
					using (var streamReader = new StreamReader(responseStream))
					{
						var data = streamReader.ReadToEnd();
						_logger.LogTrace("Received data: {edsmData}", data);

						var returnedBodies = JsonConvert.DeserializeObject<EdsmBodyResponse>(data);

						// Edsm bodies have the system name in their own name
						var edsmBodyName = body.System.Name + " " + body.Name;

						var edsmBody = returnedBodies.Bodies.FirstOrDefault(b => b.Name.Equals(edsmBodyName, StringComparison.OrdinalIgnoreCase));
						if (edsmBody != null)
						{
							body.EdsmExtId = edsmBody.Id;
							if (edsmBody.DistanceToArrival >= 0)
							{
								body.Distance = edsmBody.DistanceToArrival;
							}

							result.Add(new ValueTuple<Body, bool, string>(body, true, null));
						}
						else
						{
							result.Add(new ValueTuple<Body, bool, string>(body, false, "No body found in EDSM."));
						}
					}
				}
				catch (Exception ex)
				{
					result.Add(new ValueTuple<Body, bool, string>(body, false, "Exception while updating data from EDSM: " + ex.Message));
				}
			}

			return result;
		}

		#region EDSM Api DTO's

		// ReSharper disable once ClassNeverInstantiated.Local Justification: Will be instanciated by Json deserialization
		private class EdsmSystemResponse
		{
			// ReSharper disable once UnusedAutoPropertyAccessor.Local Justification: Will be set by Json deserialization
			public int Id { get; set; }

			// ReSharper disable once UnusedAutoPropertyAccessor.Local Justification: Will be set by Json deserialization
			public string Name { get; set; }
		}

		// ReSharper disable once ClassNeverInstantiated.Local Justification: Will be instanciated by Json deserialization
		private class EdsmBodyResponse
		{
			public string Name { get; set; }
			public int Id { get; set; }

			// ReSharper disable once UnusedAutoPropertyAccessor.Local Justification: Will be set by Json deserialization
			public EdsmBody[] Bodies { get; set; }

			// ReSharper disable once ClassNeverInstantiated.Local Justification: Will be instanciated by Json deserialization
			public class EdsmBody
			{
				// ReSharper disable once UnusedAutoPropertyAccessor.Local Justification: Will be set by Json deserialization
				public int Id { get; set; }

				// ReSharper disable once UnusedAutoPropertyAccessor.Local Justification: Will be set by Json deserialization
				public string Name { get; set; }

				// ReSharper disable once UnusedAutoPropertyAccessor.Local Justification: Will be set by Json deserialization
				public int DistanceToArrival { get; set; }
			}
		}

		#endregion
	}
}
