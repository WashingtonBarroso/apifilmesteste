using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using wiz.filmes.api.Services;
using wiz.filmes.api.Settings;

namespace wiz.filmes.testes
{
    [TestClass]
    public class FilmeTest
    {

        private IFilmeService _filmeService;
        private SettingsAPI settings = new SettingsAPI();

        [TestInitialize]
        public void Initialize()
        {
            settings.KeyAPI = "1c4d7ec540741bc5283368da0bd0a4d1";
            _filmeService = new FilmeService(new FakeHttpClient(), Options.Create(settings));
        }

        [TestMethod]
        public void TestGetFilmesSucess()
        {
            var filmes = _filmeService.ObterFilmes(1);
            Assert.IsNotNull(filmes.Result);
        }

        [TestMethod]
        public void TestGetFilmesError()
        {
            var retorno = _filmeService.ObterFilmes(0);
            Assert.IsTrue(retorno.Result.Filmes == null);
        }

    }
}
