using Application.Commands;
using Application.DataTransfer;
using Application.FileUpload;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Commands
{
    public class EFAddDishCommand : BaseService, IAddDishCommand
    {
        private readonly IMapper _mapper;
        private readonly IFIleService _fileService;
        public EFAddDishCommand(DBContext context, IMapper mapper, IFIleService fileService) : base(context)
        {
            _mapper = mapper;
            _fileService = fileService;
        }
        public async void Execute(DishDTO request)
        {
            var (Server, FilePath) = await _fileService.Upload(request.Image);

            var mappingToDto = _mapper.Map<DishEntity>(new DishDTO
            {
                Name = request.Name,
                ImagePath = FilePath,
                Description = request.Description,
                Price = request.Price,
                
            });

            _context.Dishes.Add(mappingToDto);
            _context.SaveChanges();
        }
    }
}
