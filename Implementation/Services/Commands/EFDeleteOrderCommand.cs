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
    public class EFDeleteOrderCommand : BaseService, IDeleteOrderCommand
    {
        private readonly IMapper _mapper;

        public EFDeleteOrderCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(int id)
        {
            var order = _context.Orders.FirstOrDefault(u => u.Id == id);

            if (order == null)
            {
                throw new EntityNotFoundException("Order");
            }

            if (order.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            order.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
