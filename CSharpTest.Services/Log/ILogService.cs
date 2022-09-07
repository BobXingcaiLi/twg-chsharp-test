using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTest.Models;

namespace CSharpTest.Services.Log
{
    public interface ILogService
    {
        Task<long> LogRequest(char kind);
        Task LogSearchRequest(long rid, string search, char successInd, int hits);

        Task LogSearchTopProducts(long rid, List<TopProduct> topProducts);
    }
}
