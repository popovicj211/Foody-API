using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private IGetDishesQuery _getDishesQuery;

        public DishesController(IGetDishesQuery getDishesQuery)
        {
            _getDishesQuery = getDishesQuery;
  
        }

        // GET: api/<DishesController>
        [HttpGet]
        public ActionResult<IEnumerable<DishDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var dishes = _getDishesQuery.Execute(request);
                return Ok(dishes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }
    }
}
