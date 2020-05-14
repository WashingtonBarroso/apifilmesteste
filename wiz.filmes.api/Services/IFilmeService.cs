using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using wiz.filmes.api.ViewModels;

namespace wiz.filmes.api.Services
{
    public interface IFilmeService
    {
        /// <summary>
        ///  Recupera os filmes a serem lançados.
        /// </summary>
        /// <param name="nPage"></param>
        /// <returns></returns>
        Task<PaginatedViewModel> ObterFilmes(int nPage);

        /// <summary>
        /// Recupera os generos dos filmes.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        Task<string> ObterGeneros(HttpClient client);
    }
}