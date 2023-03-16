using Application.Commands.User;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.User;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.User;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers.Admin
{
    [Route("api/[controller]")]
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
            _addUserCommand = addUserCommand;
            _deleteUserCommand = deleteUserCommand;
            _getUserQuery = getUserQuery;
            _updateUserCommand = updateUserCommand;
            _getUsersQuery = getUsersQuery;
            _context = context;
        }

        // GET: api/<UserControllers>
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var users = _getUsersQuery.Execute(request);
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/<UserControllers>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<UserDTO>> Get(int id)
        {
            try
            {
                var user = _getUserQuery.Execute(id);
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

        // POST api/<UserControllers>
        [HttpPost]
        [Obsolete]
        public ActionResult Post([FromBody] UserDTO request)
        {
            var validator = new AddUserFluentValidator(_context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                _addUserCommand.Execute(request);
                return StatusCode(201, "User is succefuly create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // PUT api/<UserControllers>/5
        [HttpPut("{id}")]
        [Obsolete]
        public ActionResult Put(int id, [FromBody] UserDTO request)
        {
            var validator = new UpdateUserFluentValidator(_context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                _updateUserCommand.Execute(request);
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

        // DELETE api/<UserControllers>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _deleteUserCommand.Execute(id);
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
