using Application.Commands;
using Application.DataTransfer;
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
    public class EFUpdateIngredientCommand : BaseService, IUpdateIngredientCommand
    {
        private readonly IMapper _mapper;

        public EFUpdateIngredientCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(IngredientDTO request)
        {
            var ingredient = _context.Ingredients.FirstOrDefault(u => u.Id == request.Id);

            if (ingredient == null)
            {
                throw new EntityNotFoundException("Ingredient");
            }

            if (ingredient.Name != request.Name)
            {
                ingredient.Name = request.Name;
            }

            _context.SaveChanges();
        }
    }
}
