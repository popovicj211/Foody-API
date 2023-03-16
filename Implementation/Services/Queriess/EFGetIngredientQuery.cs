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
    public class EFGetIngredientQuery : BaseService, IGetIngredientQuery
    {
        private readonly IMapper _mapper;
        public EFGetIngredientQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public DishTypeDTO Execute(int id)
        {
            var ingredient = _context.Ingredients.Where(u => !u.IsDeleted).ProjectTo<DishTypeDTO>(_mapper.ConfigurationProvider).Select(u => new DishTypeDTO
            {
                Id = u.Id,
                Name = u.Name
            }).FirstOrDefault(u => u.Id == id);

            if (ingredient == null)
            {
                throw new EntityNotFoundException("Ingredient");
            }

            return ingredient;
        }
    }
}
