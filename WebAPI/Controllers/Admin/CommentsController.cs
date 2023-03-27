using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.Comment;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private IAddCommentCommand _addCommentCommand;
        private IDeleteCommentCommand _deleteCommentCommand;
        private IGetCommentQuery _getCommentQuery;
        private IUpdateCommentCommand _updateCommentCommand;
        private IGetCommentsQuery _getCommentsQuery;
        private readonly DBContext _context;

        public CommentsController(DBContext context, IAddCommentCommand addCommentCommand, IDeleteCommentCommand deleteCommentCommand, IGetCommentQuery getCommentQuery, IUpdateCommentCommand updateCommentCommand, IGetCommentsQuery getCommentsQuery)
        {
            this._addCommentCommand = addCommentCommand;
            this._deleteCommentCommand = deleteCommentCommand;
            this._getCommentQuery = getCommentQuery;
            this._updateCommentCommand = updateCommentCommand;
            this._getCommentsQuery = getCommentsQuery;
            this._context = context;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        public ActionResult<IEnumerable<CommentDTO>>  Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var comments = this._getCommentsQuery.Execute(request);
                return Ok(comments);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<CommentDTO>> Get(int id)
        {
            try
            {
                var comment = this._getCommentQuery.Execute(id);
                return Ok(comment);
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

        // POST api/<CommentsController>
        [HttpPost]
        [Obsolete]
        public ActionResult Post([FromForm] CommentDTO request)
        {
            var validator = new CommentFluentValidator(this._context);
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

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        [Obsolete]
        public ActionResult Put(int id, [FromForm] CommentDTO request)
        {
            var validator = new UpdateCommentFluentValidator(this._context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._updateCommentCommand.Execute(request);
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

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                this._deleteCommentCommand.Execute(id);
                return StatusCode(204, "Comment is deleted");
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
