using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFDeleteDishCommentCommand : BaseService, IDeleteDishCommentCommand
    {
        private readonly IMapper _mapper;
        public EFDeleteDishCommentCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(DishCommentDTO request)
        {
            var dish = _context.Dishes.Include(d => d.DishComments).FirstOrDefault(g => g.Id == request.DishId);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            List<DishCommentEntity> dishComments = new List<DishCommentEntity>();

            foreach (int item in request.CommentIds)
            {
                var comment = dish.DishComments.FirstOrDefault(g => g.CommentId == item);

                if (comment == null)
                {
                    throw new EntityNotFoundException("Comment");
                }

                dishComments.Add(comment);
            }

            foreach (var item in dishComments)
            {
                this._context.Remove(item);
                this._context.SaveChanges();
            }
        }
    }
}
