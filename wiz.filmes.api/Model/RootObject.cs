using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wiz.filmes.api.Model
{
    public class RootObject
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Result> results { get; set; }
    }
}
