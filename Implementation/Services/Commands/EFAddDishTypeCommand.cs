using Application.Commands;
using Application.Commands.User;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces;
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
    public class EFAddDishTypeCommand : BaseService, IAddDishTypeCommand
    {
        private readonly IMapper _mapper;
        public EFAddDishTypeCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public void Execute(DishTypeDTO request)
        {
            var mappingToDto = _mapper.Map<DishTypeEntity>(new DishTypeDTO
            {
                Name = request.Name
            });

            _context.DishTypes.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
