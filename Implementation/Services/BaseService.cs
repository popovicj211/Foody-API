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
    public abstract class BaseService
    {
        protected readonly DBContext _context;

        public BaseService(DBContext context)
        {
            this._context = context;
        }
    }
}
