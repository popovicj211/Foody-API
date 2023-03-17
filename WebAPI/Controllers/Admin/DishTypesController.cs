using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.DishType;
using Implementation.FluentValidators.Ingredient;
using Implementation.Formatters;
using Implementation.Services.Commands;
using Implementation.Services.Queriess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            _addDishTypeCommand = addDishTypeCommand;
            _deleteDishTypeCommand = deleteDishTypeCommand;
            _getDishTypeQuery = getDishTypeQuery;
            _updateDishTypeCommand = updateDishTypeCommand;
            _getDishTypesQuery = getDishTypesQuery;
            _context = context;
        }

        // GET: api/<DishTypesController>
        [HttpGet]
        public ActionResult<IEnumerable<DishTypeDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var dishTypes = _getDishTypesQuery.Execute(request);
                return Ok(dishTypes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/<DishTypesController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<DishTypeDTO>> Get(int id)
        {
            try
            {
                var dishType = _getDishTypeQuery.Execute(id);
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
        public ActionResult Post([FromBody] DishTypeDTO request)
        {
            var validator = new DishTypeFluentValidator(_context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                _addDishTypeCommand.Execute(request);
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
        public ActionResult Put(int id, [FromBody] DishTypeDTO request)
        {
            var validator = new UpdateDishTypeFluentValidator(_context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                _updateDishTypeCommand.Execute(request);
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
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteDishTypeCommand.Execute(id);
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
