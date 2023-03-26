using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using EFDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishTypeDishesController : ControllerBase
    {
        private IAddDishTypeDishCommand _addDishTypeDishCommand;
        private IDeleteDishTypeDishCommand _deleteDishTypeDishCommand;
        private readonly DBContext _context;

        public DishTypeDishesController(DBContext context, IAddDishTypeDishCommand addDishTypeDishCommand, IDeleteDishTypeDishCommand deleteDishTypeDishCommand)
        {
            this._addDishTypeDishCommand = addDishTypeDishCommand;
            this._deleteDishTypeDishCommand = deleteDishTypeDishCommand;
            this._context = context;
        }

        // POST api/<DishTypeDishesController>
        [HttpPost]
        public ActionResult Post([FromForm] DishTypeDishDTO request)
        {
            try
            {
                this._addDishTypeDishCommand.Execute(request);
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

        // DELETE api/<DishTypeDishesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromForm] DishTypeDishDTO request)
        {
            try
            {
                request.Id = id;
                this._deleteDishTypeDishCommand.Execute(request);
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
