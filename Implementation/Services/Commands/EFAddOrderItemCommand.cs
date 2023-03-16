using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using EFDataAccess;
using Implementation.EFServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace Implementation.Services.Commands
{
    public class EFAddOrderItemCommand : BaseService, IAddOrderItemCommand
    {
        private readonly IMapper _mapper;
        public EFAddOrderItemCommand(DBContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public void Execute(OrderItemDTO request)
        {
            //var mappingToDto = _mapper.Map<OrderEntity>(new OrderItemDTO
            //{
            //    Qty = request.Qty,
            //    OrderId = request.OrderId,
            //    DishId = request.DishId
            //});

            //_context.Orders.Add(mappingToDto);
            //_context.SaveChanges();






            //var game = _context.Orders.Include(g => g.OrderItems)
            //                        .FirstOrDefaultAsync(g => g.Id == gameId);

            //if (game == null)
            //{
            //    throw new EntityNotFoundException("Game");
            //}

            //List<Genre> genres = new List<Genre>();

            //foreach (int genre in dto.Genres)
            //{
            //    Genre platformCheck = _context.Genres.Find(genre);
            //    if (platformCheck == null)
            //    {
            //        throw new EntityNotFoundException("Genre");
            //    }
            //    else
            //    {
            //        bool contains = game.GameGenres.Any(g => g.GenreId == genre);

            //        if (contains)
            //        {
            //            throw new DataAlreadyExistsException("Game already has that genre.");
            //        }

            //        genres.Add(platformCheck);
            //    }
            //}

            //if (dto.Genres.Count != genres.Count)
            //{
            //    throw new Exception("One of genres doesn't exist.");
            //}

            //List<Domain.Relations.GameGenre> pairs = new List<Domain.Relations.GameGenre>();
            //foreach (int genre in dto.Genres)
            //{
            //    pairs.Add(new Domain.Relations.GameGenre
            //    {
            //        GameId = gameId,
            //        GenreId = genre
            //    });
            //}

            //await _context.GameGenres.AddRangeAsync(pairs);

            //await _context.SaveChangesAsync();





        }
    }
}
