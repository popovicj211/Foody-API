using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using Azure.Core;
using EFDataAccess;
using FluentValidation;
using Implementation.FluentValidators.Comment;
using Implementation.FluentValidators.Ingredient;
using Implementation.Formatters;
using Implementation.Services.Commands;
using Implementation.Services.Queriess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers.Admin
{
    [Route("api/[controller]")]
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
            _addCommentCommand = addCommentCommand;
            _deleteCommentCommand = deleteCommentCommand;
            _getCommentQuery = getCommentQuery;
            _updateCommentCommand = updateCommentCommand;
            _getCommentsQuery = getCommentsQuery;
            _context = context;
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

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        [Obsolete]
        public ActionResult Put(int id, [FromForm] CommentDTO request)
        {
            var validator = new UpdateCommentFluentValidator(_context, id);
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
                _deleteCommentCommand.Execute(id);
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
