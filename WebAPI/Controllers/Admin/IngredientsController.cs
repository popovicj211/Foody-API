using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.Ingredient;
using Implementation.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            this._addIngredientCommand = addIngredientCommand;
            this._deleteIngredientCommand = deleteIngredientCommand;
            this._getIngredientQuery = getDishQuery;
            this._updateIngredientCommand = updateIngredientCommand;
            this._getIngredientsQuery = getIngedientsQuery;
            this._context = context;
        }

        // GET: api/<IngredientsControllers>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<IngredientDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var ingredients = this._getIngredientsQuery.Execute(request);
                return Ok(ingredients);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/<IngredientsControllers>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<IngredientDTO>> Get(int id)
        {
            try
            {
                var ingredient = this._getIngredientQuery.Execute(id);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Post([FromForm] IngredientDTO request)
        {
            var validator = new IngredientFluentValidator(this._context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._addIngredientCommand.Execute(request);
                return StatusCode(201, "Ingredient is succefuly create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // PUT api/<IngredientsControllers>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Obsolete]
        public ActionResult Put(int id, [FromForm] IngredientDTO request)
        {
            var validator = new UpdateIngredientFluentValidator(this._context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._updateIngredientCommand.Execute(request);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                this._deleteIngredientCommand.Execute(id);
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
