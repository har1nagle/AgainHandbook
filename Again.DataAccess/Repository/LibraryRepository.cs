using Again.DataAccess.Repository.IRepository;
using Again.DataAcess.Data;
using AgainHandbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Again.DataAccess.Repository
{
    public class LibraryRepository : Repository<Library>, ILibraryRepository
    {
        private ApplicationDbContext _db;
        public LibraryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Library obj)
        {
            _db.Libraries.Update(obj);
        }
    }
}
