using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Implementation.FluentValidators.Order;
using Implementation.Formatters;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IAddOrderCommand _addOrderCommand;
        private IDeleteOrderCommand _deleteOrderCommand;
        private IGetOrderQuery _getOrderQuery;
        private IUpdateOrderCommand _updateOrderCommand;
        private IGetOrdersQuery _getOrdersQuery;
        private readonly DBContext _context;

        public OrdersController(DBContext context, IAddOrderCommand addOrderCommand, IDeleteOrderCommand deleteOrderCommand, IGetOrderQuery getOrderQuery, IUpdateOrderCommand updateOrderCommand, IGetOrdersQuery getOrdersQuery)
        {
            this._addOrderCommand = addOrderCommand;
            this._deleteOrderCommand = deleteOrderCommand;
            this._getOrderQuery = getOrderQuery;
            this._updateOrderCommand = updateOrderCommand;
            this._getOrdersQuery = getOrdersQuery;
            this._context = context;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var orders = this._getOrdersQuery.Execute(request);
                return Ok(orders);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<OrderDTO>> Get(int id)
        {
            try
            {
                var order = this._getOrderQuery.Execute(id);
                return Ok(order);
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

        // POST api/<OrdersController>
        [HttpPost]
        [Obsolete]
        public ActionResult Post([FromBody] OrderDTO request)
        {
            var validator = new OrderFluentValidator(this._context);
            var errors = validator.Validate(request);
            if (!errors.IsValid)
            {
                return UnprocessableEntity(ValidationFormatter.Format(errors));
            }
            try
            {
                this._addOrderCommand.Execute(request);
                return StatusCode(201, "Order is succefuly create.");
            }

            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        [Obsolete]
        public ActionResult Put(int id, [FromBody] OrderDTO request)
        {
            try
            {
                this._updateOrderCommand.Execute(request);
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

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                this._deleteOrderCommand.Execute(id);
                return StatusCode(204, "Order is deleted");
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
