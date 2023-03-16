using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Queriess
{
    public class EFGetDishQuery : BaseService, IGetDishQuery
    {
        private readonly IMapper _mapper;
        public EFGetDishQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public DishDTO Execute(int id)
        {
            var dish = _context.Dishes.Where(u => !u.IsDeleted).ProjectTo<DishDTO>(_mapper.ConfigurationProvider).Select(u => new DishDTO
            {
                Id = u.Id,
                Name = u.Name
            }).FirstOrDefault(u => u.Id == id);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            return dish;
        }
    }
}
