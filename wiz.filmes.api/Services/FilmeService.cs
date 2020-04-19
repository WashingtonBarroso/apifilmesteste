using System;
using System.Net.Http;
using wiz.filmes.api.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using wiz.filmes.api.Model;
using System.Text.Json;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Options;
using wiz.filmes.api.Settings;

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
            else
            {
                return paginaViewModel;
            }

            return paginaViewModel;

        }

        private async Task<string> ObterGeneros(HttpClient client)
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