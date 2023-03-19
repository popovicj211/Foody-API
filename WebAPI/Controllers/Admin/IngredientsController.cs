using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using FluentValidation;
using Implementation.FluentValidators.DIsh;
using Implementation.FluentValidators.Ingredient;
using Implementation.Formatters;
using Implementation.Services.Queriess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private IAddIngredientCommand _addIngredientCommand;
        private IDeleteIngedientCommand _deleteIngredientCommand;
        private IGetIngredientQuery _getIngredientQuery;
        private IUpdateIngredientCommand _updateIngredientCommand;
        private IGetIngredientsQuery _getIngredientsQuery;
        private readonly DBContext _context;
        public IngredientsController(DBContext context, IAddIngredientCommand addIngredientCommand, IDeleteIngedientCommand deleteIngredientCommand, IGetIngredientQuery getDishQuery, IUpdateIngredientCommand updateIngredientCommand, IGetIngredientsQuery getIngedientsQuery)
        {
            _addIngredientCommand = addIngredientCommand;
            _deleteIngredientCommand = deleteIngredientCommand;
            _getIngredientQuery = getDishQuery;
            _updateIngredientCommand = updateIngredientCommand;
            _getIngredientsQuery = getIngedientsQuery;
            _context = context;
        }

        // GET: api/<IngredientsControllers>
        [HttpGet]
        public ActionResult<IEnumerable<IngredientDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var ingredients = _getIngredientsQuery.Execute(request);
                return Ok(ingredients);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/<IngredientsControllers>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<IngredientDTO>> Get(int id)
        {
            try
            {
                var ingredient = _getIngredientQuery.Execute(id);
                return Ok(ingredient);
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

        // POST api/<IngredientsControllers>
        [HttpPost]
        [Obsolete]
        public ActionResult Post([FromForm] IngredientDTO request)
        {
            var validator = new IngredientFluentValidator(_context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                _addIngredientCommand.Execute(request);
                return StatusCode(201, "Ingredient is succefuly create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // PUT api/<IngredientsControllers>/5
        [HttpPut("{id}")]
        [Obsolete]
        public ActionResult Put(int id, [FromForm] IngredientDTO request)
        {
            var validator = new UpdateIngredientFluentValidator(_context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                _updateIngredientCommand.Execute(request);
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

        // DELETE api/<IngredientsControllers>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteIngredientCommand.Execute(id);
                return StatusCode(204, "Ingredient is deleted");
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
