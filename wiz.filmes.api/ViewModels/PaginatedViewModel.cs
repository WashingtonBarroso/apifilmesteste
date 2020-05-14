using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wiz.filmes.api.ViewModels
{
    public class PaginatedViewModel
    {
        public int Page { get; set; }
        public int TotalResultados { get; set; }
        public int ToralPaginas { get; set; }
        public IEnumerable<FilmeViewModel> Filmes { get; set; }
    }
}
