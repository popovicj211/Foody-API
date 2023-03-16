using Application.Searches;
using Domain.Entities;
using EFDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.EFServices
{
    /*  abstract public class BaseService<T, TSearch> where T : BaseEntity where TSearch : BaseSearchRequest
      {
          protected readonly DBContext _context;
          public BaseService(DBContext context)
          {
              _context = context;
          }

          protected abstract IQueryable<T> BuildQuery(IQueryable<T> query, TSearch request);
      }*/
    public abstract class BaseService
    {
        protected readonly DBContext _context;

        public BaseService(DBContext context)
        {
            this._context = context;
        }

    }
}
