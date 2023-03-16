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
    public class EFDeleteIntergrientCommand : BaseService, IDeleteIngedientCommand
    {
        private readonly IMapper _mapper;
        public EFDeleteIntergrientCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(int id)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(u => u.Id == id);

            if (ingredient == null)
            {
                throw new EntityNotFoundException("Ingredient");
            }

            if (ingredient.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            ingredient.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
