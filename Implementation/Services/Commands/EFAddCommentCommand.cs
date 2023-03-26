using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFAddCommentCommand : BaseService, IAddCommentCommand
    {
        private readonly IMapper _mapper;

        public EFAddCommentCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(CommentDTO request)
        {
            var mappingToDto = this._mapper.Map<CommentEntity>(new CommentDTO
            {
                Content = request.Content,
                ParentId = request.ParentId,
                UserId = request.UserId,
            });

            _context.Comments.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
