using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFAddDishTypeCommand : BaseService, IAddDishTypeCommand
    {
        private readonly IMapper _mapper;

        public EFAddDishTypeCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(DishTypeDTO request)
        {
            var mappingToDto = this._mapper.Map<DishTypeEntity>(new DishTypeDTO
            {
                Name = request.Name
            });

            _context.DishTypes.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
