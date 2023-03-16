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
    public class EFGetDishTypeQuery : BaseService, IGetDishTypeQuery
    {
        private readonly IMapper _mapper;
        public EFGetDishTypeQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public DishTypeDTO Execute(int id)
        {
            var dishType = _context.DishTypes.Where(u => !u.IsDeleted).ProjectTo<DishTypeDTO>(_mapper.ConfigurationProvider).Select(u => new DishTypeDTO
            {
                Id = u.Id,
                Name = u.Name
            }).FirstOrDefault(u => u.Id == id);

            if (dishType == null)
            {
                throw new EntityNotFoundException("Dish type");
            }

            return dishType;
        }
    }
}
