using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Services.Commands
{
    public class EFAddDishCommentCommand : BaseService, IAddDishCommentCommand
    {
        private readonly IMapper _mapper;

        public EFAddDishCommentCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(DishCommentDTO request)
        {
            var dish = this._context.Dishes.Include(g => g.DishComments).FirstOrDefault(g => g.Id == request.DishId);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            List<CommentEntity> comments = new List<CommentEntity>();
            List<DishCommentEntity> dishComments = new List<DishCommentEntity>();

            foreach (var item in request.CommentIds)
            {
                var commentCheck = this._context.Comments.Find(item);

                if (commentCheck == null)
                {
                    throw new EntityNotFoundException("Comment");
                }
                else
                {
                    bool isContainCommentId = dish.DishComments.Any(g => g.CommentId == item);

                    if (isContainCommentId)
                    {
                        throw new AlreadyExistException();
                    }

                    comments.Add(commentCheck);
                }

                dishComments.Add(new DishCommentEntity
                {
                    DishId = request.Id,
                    CommentId = item
                });
            }

            this._context.DishComments.AddRange(dishComments);
            this._context.SaveChanges();
        }
    }
}
