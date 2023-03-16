using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using EFDataAccess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IGetOrdersQuery _getOrdersQuery;
        public OrdersController( IGetOrdersQuery getOrdersQuery)
        {
            _getOrdersQuery = getOrdersQuery;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var orders = _getOrdersQuery.Execute(request);
                return Ok(orders);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }
    }
}
