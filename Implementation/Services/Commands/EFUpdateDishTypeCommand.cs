using Application.Commands;
using Application.Commands.User;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFUpdateDishTypeCommand : BaseService, IUpdateDishTypeCommand
    {
        private readonly IMapper _mapper;
        public EFUpdateDishTypeCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public void Execute(DishTypeDTO request)
        {
            var dishType = _context.DishTypes.FirstOrDefault(u => u.Id == request.Id);

            if (dishType == null)
            {
                throw new EntityNotFoundException("Dish type");
            }

            if (dishType.Name != request.Name)
            {
                dishType.Name = request.Name;
            }

            _context.SaveChanges();
        }
    }
}
