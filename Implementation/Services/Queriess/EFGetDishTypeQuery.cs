using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetDishTypeQuery : BaseService, IGetDishTypeQuery
    {
        private readonly IMapper _mapper;

        public EFGetDishTypeQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public DishTypeDTO Execute(int id)
        {
            var dishType = _context.DishTypes.Where(u => !u.IsDeleted).ProjectTo<DishTypeDTO>(this._mapper.ConfigurationProvider).Select(u => new DishTypeDTO
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
