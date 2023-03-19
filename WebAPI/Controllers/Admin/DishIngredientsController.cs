using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using EFDataAccess;
using Implementation.Services.Commands;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishIngredientsController : ControllerBase
    {
        private IAddDishIngredientCommand _addDishIngredientCommand;
        private IDeleteDishIngredientCommand _deleteDishIngredientCommand;
        private readonly DBContext _context;
        public DishIngredientsController(DBContext context, IAddDishIngredientCommand addDishIngredientCommand, IDeleteDishIngredientCommand deleteDishIngredientCommand)
        {
            _addDishIngredientCommand = addDishIngredientCommand;
            _deleteDishIngredientCommand = deleteDishIngredientCommand;
             _context = context;
        }

        // POST api/<DishIngredientsController>
        [HttpPost]
        public ActionResult Post([FromForm] DishIngredientDTO request)
        {
            try
            {
                _addDishIngredientCommand.Execute(request);
                return StatusCode(201);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (AlreadyExistException e)
            {
                return Conflict(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<DishIngredientsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id , [FromForm] DishIngredientDTO request)
        {
            try
            {
                request.Id = id;
                _deleteDishIngredientCommand.Execute(request);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
