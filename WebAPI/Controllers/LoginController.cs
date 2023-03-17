using Application.DataTransfer;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Queries.Auth;
using EFDataAccess;
using Implementation.FluentValidators.User;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginUserQuery _loginUserQuery;
        private readonly DBContext _context;

        public LoginController(DBContext context, ILoginUserQuery loginUserQuery)
        {
            _loginUserQuery = loginUserQuery;
            _context = context;
        }

        // POST api/<LoginController>
        [HttpPost]
        [Obsolete]
        public ActionResult Login([FromBody] LoginDTO value)
        {

            var validator = new LoginFluentValidator();
            var errors = validator.Validate(value);

            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }

            try
            {
                string token = _loginUserQuery.Execute(value);
                HttpContext
                    .Response
                    .Cookies
                    .Append("token", token, new CookieOptions { HttpOnly = true });

                return Ok(new { message = "You have succesfully logged in.", token });
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
            catch (PasswordNotValidException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ServerErrorResponse.Message }); ;
            }
        }
    }
}
