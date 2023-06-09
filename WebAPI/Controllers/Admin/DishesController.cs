﻿using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.DIsh;
using Implementation.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private IAddDishCommand _addDishCommand;
        private IDeleteDishCommand _deleteDishCommand;
        private IGetDishQuery _getDishQuery;
        private IUpdateDishCommand _updateDishCommand;
        private IGetDishesQuery _getDishesQuery;
        private readonly DBContext _context;

        public DishesController(DBContext context, IAddDishCommand addDishCommand, IDeleteDishCommand deleteDishCommand, IGetDishQuery getDishQuery, IUpdateDishCommand updateDishCommand, IGetDishesQuery getDishesQuery)
        {
            this._addDishCommand = addDishCommand;
            this._deleteDishCommand = deleteDishCommand;
            this._getDishQuery = getDishQuery;
            this._updateDishCommand = updateDishCommand;
            this._getDishesQuery = getDishesQuery;
            this._context = context;
        }

        // GET: api/<DishesController>
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public ActionResult<IEnumerable<DishDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var dishes = this._getDishesQuery.Execute(request);
                return Ok(dishes);
            }
            catch (Exception e)
            {
                 return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/<DishesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<DishDTO>> Get(int id)
        {
            try
            {
                var dish = this._getDishQuery.Execute(id);
                return Ok(dish);
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

        // POST api/<DishesController>
        [HttpPost]
        [Obsolete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Post([FromForm] DishDTO request)
        {
            var validator = new DishFluentValidator(this._context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
               await this._addDishCommand.Execute(request);
                return StatusCode(201, "Dish is succesfully create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // PUT api/<DishesController>/5
        [HttpPut("{id}")]
        [Obsolete]
        [Authorize(Roles = "Admin")]
        public ActionResult Put(int id, [FromForm] DishDTO request)
        {
            var validator = new UpdateDishFluentValidator(this._context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._updateDishCommand.Execute(request);
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

        // DELETE api/<DishesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                this._deleteDishCommand.Execute(id);
                return StatusCode(204, "Dish is deleted");
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
