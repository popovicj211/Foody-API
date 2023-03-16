using Application.Commands;
using Application.DataTransfer;
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
    public class EFAddIngredientCommand : BaseService, IAddIngredientCommand
    {
        private readonly IMapper _mapper;
        public EFAddIngredientCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public void Execute(IngredientDTO request)
        {
            var mappingToDto = _mapper.Map<IngredientEntity>(new IngredientDTO
            {
                Name = request.Name
            });

            _context.Ingredients.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
