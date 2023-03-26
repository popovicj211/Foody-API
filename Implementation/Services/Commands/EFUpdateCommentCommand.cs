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
    public class EFUpdateCommentCommand : BaseService, IUpdateCommentCommand
    {
        private readonly IMapper _mapper;

        public EFUpdateCommentCommand(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public void Execute(CommentDTO request)
        {
            var comment = _context.Comments.FirstOrDefault(u => u.Id == request.Id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment");
            }

            if (comment.Content != request.Content)
            {
                comment.Content = request.Content;
            }

            if (comment.ParentId != request.ParentId)
            {
                comment.ParentId = request.ParentId;
            }

            _context.SaveChanges();
        }
    }
}
