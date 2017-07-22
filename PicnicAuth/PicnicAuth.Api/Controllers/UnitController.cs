using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PicnicAuth.Database.DTO.Units;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    public class UnitController : BasePicnicAuthController
    {
        private readonly IUnitService unitService;

        private readonly IUnitValidator unitValidator;

        public UnitController(IMapper autoMapper, IValidatingService validatingService, IOwningService owningService,
            IUnitService unitService, IUnitValidator unitValidator) : base(autoMapper, validatingService, owningService)
        {
            this.unitService = unitService;
            this.unitValidator = unitValidator;
        }

        /// <summary>
        /// Returns all types of unit
        /// </summary>
        /// <returns>An array of units</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<UnitDto>))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get()
        {
            IEnumerable<Unit> units = unitService.Get().ToList();
            IEnumerable<UnitDto> unitDtos = AutoMapper.Map<IEnumerable<Unit>, IEnumerable<UnitDto>>(units);

            return Request.CreateResponse(HttpStatusCode.OK, unitDtos);
        }

        /// <summary>
        /// Get one unit by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One type of unit</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UnitDto))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get(Guid id)
        {
            Unit unit = unitService.Get(id);
            UnitDto unitDto = AutoMapper.Map<Unit, UnitDto>(unit);

            return Request.CreateResponse(HttpStatusCode.OK, unitDto);
        }

        /// <summary>
        /// Create new type of unit
        /// </summary>
        /// <param name="createUnit"></param>
        /// <returns>Created unit</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(UnitDto), Description = "Unit created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        public virtual HttpResponseMessage Post(CreateUnit createUnit)
        {
            if (!ValidatingService.IsValid(unitValidator, createUnit))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(unitValidator, createUnit));

            Unit unit = AutoMapper.Map<CreateUnit, Unit>(createUnit);
            OwningService.AddOwnership(User, unit);
            unitService.Add(unit);

            Unit createdUnit = unitService.Get(unit.Id);
            UnitDto unitDto = AutoMapper.Map<Unit, UnitDto>(createdUnit);

            return Request.CreateResponse(HttpStatusCode.Created, unitDto);
        }

        /// <summary>
        /// Modify existing type of unit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editUnit"></param>
        /// <returns>Modified unit</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UnitDto), Description = "Unit updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(ValidationError), Description = "Provided data was not valid")]
        public virtual HttpResponseMessage Put(Guid id, EditUnit editUnit)
        {
            if (!ValidatingService.IsValid(unitValidator, editUnit))
                return Request.CreateResponse(ValidatingService.GetStatusCode(),
                    ValidatingService.GenerateError(unitValidator, editUnit));

            Unit unit = unitService.Get(id);
            if (unit == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, unit))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenPutError());

            AutoMapper.Map(editUnit, unit);
            unitService.Edit(unit);

            Unit editedUnit = unitService.Get(id);
            UnitDto unitDto = AutoMapper.Map<Unit, UnitDto>(editedUnit);

            return Request.CreateResponse(HttpStatusCode.OK, unitDto);
        }

        /// <summary>
        /// Delete type of unit
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Unit deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(MessageError), Description = "Not logged in")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(MessageError), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(MessageError), Description = "Not found")]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            Unit unit = unitService.Get(id);
            if (unit == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new MessageError(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            if (!OwningService.IsOwnedByUser(User, unit))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    OwningService.GenerateForbiddenDeleteError());

            unitService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
