using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFAddIngredientCommand : BaseService, IAddIngredientCommand
    {
        private readonly IMapper _mapper;

        public EFAddIngredientCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(IngredientDTO request)
        {
            var mappingToDto = this._mapper.Map<IngredientEntity>(new IngredientDTO
            {
                Name = request.Name
            });

            _context.Ingredients.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
