using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private IGetDishesQuery _getDishesQuery;

        public DishesController(IGetDishesQuery getDishesQuery)
        {
            this._getDishesQuery = getDishesQuery;
        }

        // GET: api/<DishesController>
        [HttpGet]
        public ActionResult<IEnumerable<DishDTO>> Get([FromQuery] BaseSearchRequest request)
        {
            try
            {
                var dishes = this._getDishesQuery.Execute(request);
                return Ok(dishes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Server error, try later");
            }
        }
    }
}
