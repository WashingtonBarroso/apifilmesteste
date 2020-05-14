using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace wiz.filmes.testes
{
    public class FakeHttpClient : IHttpClientFactory
    {
        public HttpClient CreateClient(string name = "")
        {
            return new HttpClient();
        }
    }
}
