using Application.Commands;
using Application.DataTransfer;
using Application.Interfaces;
using EFDataAccess;
using Implementation.FluentValidators.User;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IRegisterUserCommand _registerUserCommand;
        private IEmailSender _emailService;
        private readonly DBContext _context;

        public RegisterController(DBContext context, IRegisterUserCommand registerUserCommand, IEmailSender emailService)
        {
            _registerUserCommand = registerUserCommand;
            _emailService = emailService;
            _context = context;
        }

        // POST api/<RegisterController>
        [HttpPost]
        [Obsolete]
        public ActionResult Register([FromForm] UserDTO request)
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
                //_emailService.Body = "You have succcessfully registered.";
                //_emailService.Subject = "Registration mail";
                //_emailService.ToEmail = request.Email;
                //_emailService.Send();
                //EMAIL

                HttpContext
                    .Response
                    .Cookies
                    .Append("token", token, new CookieOptions { HttpOnly = true });

                return Ok(new { message = "You have succesfully register.", token });
            }
            catch (Exception e)
            {
                // return StatusCode(500, new { ServerErrorResponse.Message });
                return StatusCode(500, e);
            }
        }
    }
}
