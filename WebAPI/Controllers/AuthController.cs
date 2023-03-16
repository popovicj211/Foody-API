using Application.Commands.User;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
using Application.Queries.Auth;
using Application.Queries.User;
using Azure.Core;
using EFDataAccess;
using Implementation.FluentValidators.User;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using WebAPI.Hashing;
using Application.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILoginUserQuery _loginUserQuery;
        private IRegisterUserQuery _registerUserCommand;
        private IEmailSender _emailService;
        private readonly DBContext _context;

        public AuthController(DBContext context, ILoginUserQuery loginUserQuery, IRegisterUserQuery registerUserCommand, IEmailSender emailService)
        {
            _loginUserQuery = loginUserQuery;
            _registerUserCommand = registerUserCommand;
            _emailService = emailService;
            _context = context;
        }

        // POST api/register/<AuthController>
        [HttpPost]
        [Route("")]
        [Obsolete]
        public ActionResult Register([FromBody] UserDTO request)
        {
            var validator = new RegisterFluentValidator(_context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }

            try
            {
                var token = _registerUserCommand.Execute(request);
                _emailService.Body = "You have succcessfully registered.";
                _emailService.Subject = "Registration mail";
                _emailService.ToEmail = request.Email;
                _emailService.Send();
                //EMAIL

                HttpContext
                    .Response
                    .Cookies
                    .Append("token", token, new CookieOptions { HttpOnly = true });

                return Ok(new { message = "You have succesfully register.", token });
            }
            catch (Exception)
            {
                return StatusCode(500, new { ServerErrorResponse.Message });
            }
        }

        // POST api/login/<AuthController>
        [HttpPost]
        [Route("/login")]
        [Obsolete]
        public ActionResult Login([FromBody] LoginDTO value)
        {

            var validator = new LoginFluentValidator();
            var errors =  validator.Validate(value);

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
