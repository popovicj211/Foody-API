using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using EFDataAccess;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishCommentsController : ControllerBase
    {
        private IAddDishCommentCommand _addDishCommentCommand;
        private IDeleteDishCommentCommand _deleteDishCommentCommand;
        private readonly DBContext _context;

        public DishCommentsController(DBContext context, IAddDishCommentCommand addDishCommentCommand, IDeleteDishCommentCommand deleteDishCommentCommand)
        {
            this._addDishCommentCommand = addDishCommentCommand;
            this._deleteDishCommentCommand = deleteDishCommentCommand;
            this._context = context;
        }

        // POST api/<DishCommentsController>
        [HttpPost]
        public ActionResult Post([FromBody] DishCommentDTO request)
        {
            try
            {
                this._addDishCommentCommand.Execute(request);
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

        // DELETE api/<DishCommentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id, [FromForm] DishCommentDTO request)
        {
            try
            {
                request.Id = id;
                this._deleteDishCommentCommand.Execute(request);
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
