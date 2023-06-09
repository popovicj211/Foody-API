﻿using Application.Commands;
using Application.DataTransfer;
using Application.FileUpload;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFAddDishCommand : BaseService, IAddDishCommand
    {
        private readonly IMapper _mapper;
        private readonly IFIleService _fileService;
        public EFAddDishCommand(DBContext context, IMapper mapper, IFIleService fileService) : base(context)
        {
            this._mapper = mapper;
            this._fileService = fileService;
        }
        public async Task Execute(DishDTO request)
        {
            var (Server, FilePath) = await this._fileService.Upload(request.Image);

            var mappingToDto = this._mapper.Map<DishEntity>(new DishDTO
            {
                Name = request.Name,
                ImagePath = Server,
                Description = request.Description,
                Price = request.Price
            });

           await this._context.Dishes.AddAsync(mappingToDto);
           await this._context.SaveChangesAsync();
        }
    }
}
