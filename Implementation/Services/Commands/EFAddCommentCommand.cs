using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFAddCommentCommand : BaseService, IAddCommentCommand
    {
        private readonly IMapper _mapper;
        public EFAddCommentCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public void Execute(CommentDTO request)
        {
            var mappingToDto = _mapper.Map<CommentEntity>(new CommentDTO
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
