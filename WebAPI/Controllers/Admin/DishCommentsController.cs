using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Azure.Core;
using EFDataAccess;
using Implementation.Services.Commands;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            _addDishCommentCommand = addDishCommentCommand;
            _deleteDishCommentCommand = deleteDishCommentCommand;
            _context = context;
        }

        // POST api/<DishCommentsController>
        [HttpPost]
        public ActionResult Post([FromBody] DishCommentDTO request)
        {
            try
            {
                _addDishCommentCommand.Execute(request);
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
                _deleteDishCommentCommand.Execute(request);
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
