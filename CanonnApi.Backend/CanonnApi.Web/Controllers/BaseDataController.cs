using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CanonnApi.Web.DatabaseModels;
using CanonnApi.Web.Middlewares;
using CanonnApi.Web.Services.DataAccess;

namespace CanonnApi.Web.Controllers
{
	/// <summary>
	/// The base class for all controllers that provide CRUD operations on base data entities
	/// </summary>
	/// <typeparam name="T">The type of the entity to provide the CRUD operations for</typeparam>
	/// <typeparam name="TDto">The type of the DTO that will be exposed by the deriving controller</typeparam>
	public abstract class BaseDataController<T, TDto> : Controller
		where T : class, IEntity, new()
	{
		/// <summary>
		/// Gets an <see cref="ILogger"/> for logging purposes
		/// </summary>
		protected ILogger Logger { get; }

		/// <summary>
		/// Gets a repository that will do the work on the data storage
		/// </summary>
		protected IBaseDataRepository<T> Repository { get; }

		/// <summary>
		/// Gets the mapper that is used to mapp the entity type to the DTO type
		/// </summary>
		protected IMapper Mapper { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseDataController{T, TDto}"/>
		/// </summary>
		/// <param name="logger">An instance of an <see cref="ILogger"/> used for logging</param>
		/// <param name="repository">An instance of an <see cref="IBaseDataRepository{T}"/> for data storage access</param>
		/// <param name="mapper">An instance of an <see cref="IMapper"/> to map between entity and DTO types</param>
		protected BaseDataController(ILogger logger, IBaseDataRepository<T> repository, IMapper mapper)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			Repository = repository ?? throw new ArgumentNullException(nameof(repository));
			Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		/// <summary>
		/// Gets a collection of all entities from the repository
		/// </summary>
		/// <returns>A representation of all entities from the database</returns>
		protected virtual async Task<IEnumerable<TDto>> GetAll()
		{
			var entities = await Repository.GetAll();

			return Mapper.Map<List<T>, List<TDto>>(entities);
		}

		/// <summary>
		/// Gets a single entity from the repository
		/// </summary>
		/// <param name="id">The id of the entity to fetch</param>
		/// <returns>A representation of the entity</returns>
		protected virtual async Task<TDto> GetById(int id)
		{
			var entry = await Repository.GetById(id);
			if (entry == null)
				throw new HttpNotFoundException();

			return Mapper.Map<T, TDto>(entry);
		}

		/// <summary>
		/// Creates a new entity
		/// </summary>
		/// <param name="data">A representation of the entity to store</param>
		/// <returns>An updated representation of the stored entity</returns>
		protected virtual async Task<TDto> CreateNew(TDto data)
		{
			var entity = Mapper.Map<TDto, T>(data);
			var newEntity = await Repository.Create(entity);

			return Mapper.Map<T, TDto>(newEntity);
		}

		/// <summary>
		/// Creates or updates an entity
		/// </summary>
		/// <param name="data">A representation of the entity to store</param>
		/// <param name="id">The id of the entity to create with or to update</param>
		/// <returns>An updated representation of the stored entity</returns>
		protected virtual async Task<TDto> CreateNewOrUpdateExisting([FromBody] TDto data, int id)
		{
			try
			{
				var entity = Mapper.Map<TDto, T>(data);
				var newEntity = await Repository.CreateOrUpdateById(id, entity);

				return Mapper.Map<T, TDto>(newEntity);
			}
			catch (Exception e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		/// <summary>
		/// Updates an existing entity
		/// </summary>
		/// <param name="data">A representation of the entity to update</param>
		/// <param name="id">The id of the entity to update</param>
		/// <returns>An updated representation of the stored entity</returns>
		protected virtual async Task<TDto> UpdateExisting([FromBody] TDto data, int id)
		{
			try
			{
				var entity = Mapper.Map<TDto, T>(data);
				var newEntity = await Repository.Update(id, entity);

				return Mapper.Map<T, TDto>(newEntity);
			}
			catch (DbUpdateConcurrencyException e)
			{
				throw new HttpException(HttpStatusCode.Conflict, "Conflict", e);
			}
		}

		/// <summary>
		/// Deletes an existing entity
		/// </summary>
		/// <param name="id">The id of the entity to delete</param>
		/// <returns>Whether deletion was successful or not</returns>
		protected virtual async Task<ActionResult> DeleteExisting(int id)
		{
			return (await Repository.DeleteById(id))
				? (ActionResult)Ok()
				: NotFound();
		}
	}
}
