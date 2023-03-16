using Application.Commands;
using Application.Commands.User;
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
    public class EFDeleteDishTypeCommand : BaseService, IDeleteDishTypeCommand
    {
        private readonly IMapper _mapper;
        public EFDeleteDishTypeCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(int id)
        {
            var dishType = _context.DishTypes.FirstOrDefault(u => u.Id == id);

            if (dishType == null)
            {
                throw new EntityNotFoundException("Dish type");
            }

            if (dishType.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            dishType.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
