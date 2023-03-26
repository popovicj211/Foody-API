using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetCommentQuery : BaseService, IGetCommentQuery
    {
        private readonly IMapper _mapper;

        public EFGetCommentQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public CommentDTO Execute(int id)
        {
            var comment = _context.Comments.Where(u => !u.IsDeleted).ProjectTo<CommentDTO>(this._mapper.ConfigurationProvider).Select(u => new CommentDTO
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
