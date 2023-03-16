using Application.Commands;
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
    public class EFDeleteCommentCommand : BaseService, IDeleteCommentCommand
    {
        private readonly IMapper _mapper;
        public EFDeleteCommentCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public void Execute(int id)
        {
            var comment = _context.Comments.FirstOrDefault(u => u.Id == id);

            if (comment == null)
            {
                throw new EntityNotFoundException("Comment");
            }

            if (comment.IsDeleted == true)
            {
                throw new AlreadyExistException();
            }

            comment.IsDeleted = true;
            _context.SaveChanges();
        }
    }
}
