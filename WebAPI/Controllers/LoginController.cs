using Application.DataTransfer;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Queries;
using EFDataAccess;
using Implementation.FluentValidators.User;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginUserQuery _loginUserQuery;

        public LoginController(ILoginUserQuery loginUserQuery)
        {
            this._loginUserQuery = loginUserQuery;
        }

        // POST api/<LoginController>
        [HttpPost]
        [Obsolete]
        public ActionResult Login([FromForm] LoginDTO value)
        {

            var validator = new LoginFluentValidator();
            var errors = validator.Validate(value);

            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }

            try
            {
                string token = this._loginUserQuery.Execute(value);
                HttpContext
                    .Response
                    .Cookies
                    .Append("token", token, new CookieOptions { HttpOnly = true });

                if (token == null)
                    return Unauthorized();

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
