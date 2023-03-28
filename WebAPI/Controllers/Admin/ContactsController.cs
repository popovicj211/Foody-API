using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.Contact;
using Implementation.Formatters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IAddContactCommand _addContactCommand;
        private IDeleteContactCommand _deleteContactCommand;
        private IGetContactQuery _getContactQuery;
        private IUpdateContactCommand _updateContactCommand;
        private IGetContactsQuery _getContactsQuery;
        private readonly DBContext _context;

        public ContactsController(DBContext context, IAddContactCommand addContactCommand, IDeleteContactCommand deleteContactCommand, IGetContactQuery getContactQuery, IUpdateContactCommand updateContactCommand, IGetContactsQuery getContactsQuery)
        {
            this._addContactCommand = addContactCommand;
            this._deleteContactCommand = deleteContactCommand;
            this._getContactQuery = getContactQuery;
            this._updateContactCommand = updateContactCommand;
            this._getContactsQuery = getContactsQuery;
            this._context = context;
        }

        // GET: api/admin/<ContactsController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<ContactDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var contacts = this._getContactsQuery.Execute(request);
                return Ok(contacts);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/admin/<ContactsController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<ContactDTO>> Get(int id)
        {
            try
            {
                var contact = this._getContactQuery.Execute(id);
                return Ok(contact);
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

        // POST api/admin/<ContactsController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        // PUT api/admin/<ContactsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [Obsolete]
        public ActionResult Put(int id, [FromForm] ContactDTO request)
        {
            var validator = new UpdateContactFluentValidator(this._context, id);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._updateContactCommand.Execute(request);
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

        // DELETE api/admin/<ContactsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                this._deleteContactCommand.Execute(id);
                return StatusCode(204, "Contact is deleted");
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
