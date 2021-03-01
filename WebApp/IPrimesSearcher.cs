using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public interface IPrimesSearcher
    {
        public bool IsPrime(int numb);
        public Task<List<int>> FindPrimesAsync(int from, int to);
    }
}
