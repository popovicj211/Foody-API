using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IGetOrdersQuery _getOrdersQuery;

        public OrdersController( IGetOrdersQuery getOrdersQuery)
        {
            this._getOrdersQuery = getOrdersQuery;
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
    }
}
