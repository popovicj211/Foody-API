using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;

namespace Implementation.Services.Commands
{
    public class EFDeleteDishCommentCommand : BaseService, IDeleteDishCommentCommand
    {
        public EFDeleteDishCommentCommand(DBContext context) : base(context)
        {
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
