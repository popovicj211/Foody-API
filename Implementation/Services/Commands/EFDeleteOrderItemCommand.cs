using Application.Commands;
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
    public class EFDeleteOrderItemCommand : BaseService, IDeleteOrderItemCommand
    {
        private readonly IMapper _mapper;

        public EFDeleteOrderItemCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(int id)
        {
            var orderItem = this._context.OrderItems.FirstOrDefault(orderItem => orderItem.Id == id);

            if(orderItem == null)
            {
                throw new EntityNotFoundException("Order item");
            }

            if (orderItem.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            orderItem.IsDeleted = true;
            this._context.SaveChanges();
        }
    }
}
