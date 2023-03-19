using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Azure.Core;
using EFDataAccess;
using Implementation.FluentValidators.Comment;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private IAddCommentCommand _addCommentCommand;
        private readonly DBContext _context;

        public CommentsController(DBContext context, IAddCommentCommand addCommentCommand)
        {
            _addCommentCommand = addCommentCommand;
            _context = context;
        }

        // POST api/<CommentsController>
        [HttpPost]
        [Obsolete]
        public ActionResult Post([FromForm] CommentDTO request)
        {
            var validator = new CommentFluentValidator(_context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._addCommentCommand.Execute(request);
                return StatusCode(201, "Comment is succesfully create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }
    }
}
