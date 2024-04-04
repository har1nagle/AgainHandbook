using AgainHandbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Again.DataAccess.Repository.IRepository
{
    public interface ILibraryRepository : IRepository<Library>
    {
        void Update(Library obj);

    }
}
