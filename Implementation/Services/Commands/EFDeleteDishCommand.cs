using Application.Commands;
using Application.Exceptions;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFDeleteDishCommand : BaseService, IDeleteDishCommand
    {
        private readonly IMapper _mapper;
        public EFDeleteDishCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(int id)
        {
            var dish = _context.Dishes.FirstOrDefault(u => u.Id == id);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            if (dish.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            dish.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
