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
    public class EFAddOrderCommand : BaseService, IAddOrderCommand
    {
        private readonly IMapper _mapper;
        public EFAddOrderCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public void Execute(OrderDTO request)
        {
            var mappingToDto = _mapper.Map<OrderEntity>(new OrderDTO
            {
                TotalPrice = request.TotalPrice,
                Date = DateTime.Now,
                UserId = request.UserId
            });

            _context.Orders.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
