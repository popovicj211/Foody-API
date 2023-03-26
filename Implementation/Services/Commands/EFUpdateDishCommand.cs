using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.FileUpload;
using AutoMapper;
using EFDataAccess;
using Implementation.EFServices;

namespace Implementation.Services.Commands
{
    public class EFUpdateDishCommand : BaseService, IUpdateDishCommand
    {
        private readonly IMapper _mapper;
        private readonly IFIleService _fileService;

        public EFUpdateDishCommand(DBContext context, IMapper mapper, IFIleService fileService) : base(context)
        {
            this._mapper = mapper;
            this._fileService = fileService;
        }

        public async void Execute(DishDTO request)
        {
            var dish = _context.Dishes.FirstOrDefault(u => u.Id == request.Id);

            if (dish == null)
            {
                throw new EntityNotFoundException("Dish");
            }

            if (dish.Name != request.Name)
            {
                dish.Name = request.Name;
            }

            if (dish.Description != request.Description)
            {
                dish.Description = request.Description;
            }

            if (dish.Price != request.Price)
            {
                dish.Price = request.Price;
            }

            if (request.Image != null)
            {
                var (Server, FilePath) = await _fileService.Upload(request.Image);
                //Remove previous image in case if user uploaded new image
                await _fileService.Remove(dish.ImagePath);

                dish.ImagePath = FilePath;
            }

            _context.SaveChanges();
        }
    }
}
