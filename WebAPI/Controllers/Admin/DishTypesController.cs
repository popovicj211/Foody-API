﻿using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.DishType;
using Implementation.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DishTypesController : ControllerBase
    {
        private IAddDishTypeCommand _addDishTypeCommand;
        private IDeleteDishTypeCommand _deleteDishTypeCommand;
        private IGetDishTypeQuery _getDishTypeQuery;
        private IUpdateDishTypeCommand _updateDishTypeCommand;
        private IGetDishTypesQuery _getDishTypesQuery;
        private readonly DBContext _context;

        public DishTypesController(DBContext context, IAddDishTypeCommand addDishTypeCommand, IDeleteDishTypeCommand deleteDishTypeCommand, IGetDishTypeQuery getDishTypeQuery, IUpdateDishTypeCommand updateDishTypeCommand, IGetDishTypesQuery getDishTypesQuery)
        {
            this._addDishTypeCommand = addDishTypeCommand;
            this._deleteDishTypeCommand = deleteDishTypeCommand;
            this._getDishTypeQuery = getDishTypeQuery;
            this._updateDishTypeCommand = updateDishTypeCommand;
            this._getDishTypesQuery = getDishTypesQuery;
            this._context = context;
        }

        // GET: api/<DishTypesController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<DishTypeDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var dishTypes = this._getDishTypesQuery.Execute(request);
                return Ok(dishTypes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/<DishTypesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<DishTypeDTO>> Get(int id)
        {
            try
            {
                var dishType = this._getDishTypeQuery.Execute(id);
                return Ok(dishType);
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // POST api/<DishTypesController>
        [HttpPost]
        [Obsolete]
        [Authorize(Roles = "Admin")]
        public ActionResult Post([FromBody] DishTypeDTO request)
        {
            var validator = new DishTypeFluentValidator(this._context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._addDishTypeCommand.Execute(request);
                return StatusCode(201, "Dish type is succefuly create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // PUT api/<DishTypesController>/5
        [HttpPut("{id}")]
        [Obsolete]
        [Authorize(Roles = "Admin")]
        public ActionResult Put(int id, [FromBody] DishTypeDTO request)
        {
            var validator = new UpdateDishTypeFluentValidator(this._context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._updateDishTypeCommand.Execute(request);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // DELETE api/<DishTypesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                this._deleteDishTypeCommand.Execute(id);
                return StatusCode(204, "Data type is deleted");
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }
    }
}
