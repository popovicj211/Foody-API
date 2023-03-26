using Application.DataTransfer;
using Application.Helpers;
using Application.Pagination;
using Application.Queries;
using Application.Searches;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Queriess
{
    public class EFGetCommentsQuery : BaseService, IGetCommentsQuery
    {
        private readonly IMapper _mapper;

        public EFGetCommentsQuery(DBContext context, IMapper mapper) : base(context)
        {
            this._mapper = mapper;
        }

        public PagedResponse<CommentDTO> Execute(BaseSearchRequest request)
        {
            var comments = _context.Comments.AsQueryable();

            return comments.Where(d => !d.IsDeleted).ProjectTo<CommentDTO>(this._mapper.ConfigurationProvider).Select(u => new CommentDTO
            {
                Id = u.Id,
                Content = u.Content,
                ParentId = u.ParentId,
                UserId = u.UserId
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
