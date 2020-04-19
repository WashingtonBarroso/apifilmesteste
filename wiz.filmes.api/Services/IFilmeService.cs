using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wiz.filmes.api.ViewModels;

namespace wiz.filmes.api.Services
{
    public interface IFilmeService 
    {
      Task<PaginatedViewModel> ObterFilmes(int nPage);
    }
}