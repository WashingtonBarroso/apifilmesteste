using System;
using System.Collections.Generic;

namespace wiz.filmes.api.ViewModels
{
    public class FilmeViewModel
    {
        public string Nome { get; set; }
        public List<GeneroViewModel> Genero { get; set; }
        public string Dtlancamento { get; set; }

    }

}