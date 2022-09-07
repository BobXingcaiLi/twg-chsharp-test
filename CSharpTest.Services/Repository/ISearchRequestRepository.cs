using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest.Services.Repository
{
    public interface ISearchRequestRepository
    {
        Task InsertAsync(long rid, string search, char successInd, int hits);
    }
}
