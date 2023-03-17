using Application.DataTransfer;
using Application.Exceptions;
using Application.Helpers;
using Application.Interfaces;
using Application.Queries.Auth;
using EFDataAccess;
using Implementation.FluentValidators.User;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IRegisterUserQuery _registerUserCommand;
        private IEmailSender _emailService;
        private readonly DBContext _context;

        public RegisterController(DBContext context, IRegisterUserQuery registerUserCommand, IEmailSender emailService)
        {
            _registerUserCommand = registerUserCommand;
            _emailService = emailService;
            _context = context;
        }

        // POST api/<RegisterController>
        [HttpPost]
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
    }
}
