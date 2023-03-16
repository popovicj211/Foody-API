using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Queriess
{
    public class EFGetCommentQuery : BaseService, IGetCommentQuery
    {
        private readonly IMapper _mapper;
        public EFGetCommentQuery(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public CommentDTO Execute(int id)
        {
            var comment = _context.Comments.Where(u => !u.IsDeleted).ProjectTo<CommentDTO>(_mapper.ConfigurationProvider).Select(u => new CommentDTO
            {
                Id = u.Id,
                Content = u.Content,
                ParentId = u.ParentId,
                UserId = u.UserId
            }).FirstOrDefault(u => u.Id == id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment");
            }

            return comment;
        }
    }
}
