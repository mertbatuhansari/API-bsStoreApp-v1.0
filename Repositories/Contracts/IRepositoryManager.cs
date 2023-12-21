using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
#pragma warning disable IDE1006 // Naming Styles
        IBookRepository book { get; }
#pragma warning restore IDE1006 // Naming Styles
        void Save();
    }
}
