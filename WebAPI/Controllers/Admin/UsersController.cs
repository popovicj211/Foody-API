using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.User;
using Implementation.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IAddUserCommand _addUserCommand;
        private IDeleteUserCommand _deleteUserCommand;
        private IGetUserQuery _getUserQuery;
        private IUpdateUserCommand _updateUserCommand;
        private IGetUsersQuery _getUsersQuery;
        private readonly DBContext _context;

        public UsersController(DBContext context, IAddUserCommand addUserCommand, IDeleteUserCommand deleteUserCommand, IGetUserQuery getUserQuery, IUpdateUserCommand updateUserCommand, IGetUsersQuery getUsersQuery)
        {
            this._addUserCommand = addUserCommand;
            this._deleteUserCommand = deleteUserCommand;
            this._getUserQuery = getUserQuery;
            this._updateUserCommand = updateUserCommand;
            this._getUsersQuery = getUsersQuery;
            this._context = context;
        }

        // GET: api/admin/<UserControllers>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var users = this._getUsersQuery.Execute(request);
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/admin/<UserControllers>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDTO>> Get(int id)
        {
            try
            {
                var user = this._getUserQuery.Execute(id);
                return Ok(user);
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

        // POST api/admin/<UserControllers>
        [HttpPost]
        [Obsolete]
        [Authorize(Roles = "Admin")]
        public ActionResult Post([FromForm] UserDTO request)
        {
            var validator = new AddUserFluentValidator(this._context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._addUserCommand.Execute(request);
                return StatusCode(201, "User is succefuly create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // PUT api/admin/<UserControllers>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Obsolete]
        public ActionResult Put(int id, [FromForm] UserDTO request)
        {
            var validator = new UpdateUserFluentValidator(this._context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                request.Id = id;
                this._updateUserCommand.Execute(request);
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

        // DELETE api/admin/<UserControllers>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                this._deleteUserCommand.Execute(id);
                return StatusCode(204, "User is deleted");
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
