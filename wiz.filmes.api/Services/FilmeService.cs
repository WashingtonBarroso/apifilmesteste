using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using wiz.filmes.api.Model;
using wiz.filmes.api.Settings;
using wiz.filmes.api.ViewModels;

namespace wiz.filmes.api.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IOptions<SettingsAPI> _settings;

        public FilmeService(IHttpClientFactory clientFactory, IOptions<SettingsAPI> settings)
        {
            _clientFactory = clientFactory;
            _settings = settings;
        }


        /// <summary>
        /// Recupera os filmes a serem lançados.
        /// Retorna o nome, data lancamento e genero dos filmes a serem lançados. 
        /// </summary>
        /// <param name="nPage"></param>
        /// <returns>PaginatedViewModel</returns>
        public async Task<PaginatedViewModel> ObterFilmes(int nPage)
        {

            if (nPage <= 0)
                return new PaginatedViewModel();

            var dataHoje = DateTime.Now.ToString("yyyy-MM-dd");

            var req = new HttpRequestMessage(HttpMethod.Get, $"https://api.themoviedb.org/3/discover/movie?api_key={_settings.Value.KeyAPI}&primary_release_date.gte={dataHoje}&sort_by=primary_release_date.asc&page={nPage}");

            var client = _clientFactory.CreateClient();

            var generos = JsonSerializer.Deserialize<RootGenre>(await ObterGeneros(client));

            var resp = await client.SendAsync(req);

            var paginaViewModel = new PaginatedViewModel();

            if (resp.IsSuccessStatusCode)
            {
                var respAPIAsString = JsonSerializer.Deserialize<RootObject>(await resp.Content.ReadAsStringAsync());

                paginaViewModel = new PaginatedViewModel() { Page = respAPIAsString.page, ToralPaginas = respAPIAsString.total_pages, TotalResultados = respAPIAsString.total_results };

                paginaViewModel.Filmes = respAPIAsString.results.Select(o => new FilmeViewModel()
                {
                    Nome = o.title,
                    Genero = o.genre_ids.Select(g_id => new GeneroViewModel() { Id = g_id, Name = generos.genres.SingleOrDefault(g => g.id == g_id)?.name }).ToList(),
                    Dtlancamento = o.release_date
                });
            }

            return paginaViewModel;

        }

        /// <summary>
        /// Recupera os generos dos filmes.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<string> ObterGeneros(HttpClient client)
        {

            var reqGenero = new HttpRequestMessage(HttpMethod.Get, $"https://api.themoviedb.org/3/genre/movie/list?api_key={_settings.Value.KeyAPI}");

            var responseAPIGen = "";

            var respGenero = await client.SendAsync(reqGenero);

            if (respGenero.IsSuccessStatusCode)
            {
                responseAPIGen = await respGenero.Content
            .ReadAsStringAsync();

            }

            return responseAPIGen;

        }
    }

}