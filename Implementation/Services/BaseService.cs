using EFDataAccess;

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
