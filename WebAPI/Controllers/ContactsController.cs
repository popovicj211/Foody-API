using Application.Commands;
using Application.DataTransfer;
using EFDataAccess;
using Implementation.FluentValidators.Contact;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IAddContactCommand _addContactCommand;
        private readonly DBContext _context;

        public ContactsController(DBContext context, IAddContactCommand addContactCommand)
        {
            this._addContactCommand = addContactCommand;
            this._context = context;
        }

        // POST api/<ContactsController>
        [HttpPost]
        [Obsolete]
        public ActionResult Post([FromForm] ContactDTO request)
        {
            var validator = new ContactFluentValidator(this._context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._addContactCommand.Execute(request);
                return StatusCode(201, "Contact is succesfully create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }
    }
}
