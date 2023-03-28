using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using EFDataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            this._addDishIngredientCommand = addDishIngredientCommand;
            this._deleteDishIngredientCommand = deleteDishIngredientCommand;
            this._context = context;
        }

        // POST api/<DishIngredientsController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Post([FromForm] DishIngredientDTO request)
        {
            try
            {
                this._addDishIngredientCommand.Execute(request);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id , [FromForm] DishIngredientDTO request)
        {
            try
            {
                request.Id = id;
                this._deleteDishIngredientCommand.Execute(request);
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
