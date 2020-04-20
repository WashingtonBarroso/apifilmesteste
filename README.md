# apifilmeswiz

API que retorna os filmes a serem lançados a partir da data atual. O serviço retorna um objeto com as seguintes informações, pagina atual, total de resultados, total de paginas, além de uma lista de filmes com Nome, Genero e Data Lançamento. Ao utilizar o enpoint é necessário informar o número de página, como o requisito dizia "A lista não deve se limitar a mostrar apenas os primeiros 20 filmes retornados pela API", sem mais detalhes, optei por essa solução imaginando que dependendo da quantidade registros a consulta poderia ficar inviável.

* Nesse projeto usei o padrão de injenção de dependência atualmente muito usado em projetos dotnet core pela facilidade de implementação fornecida pelo framework, a injeção de dependencia facilita no emprego de inversão de controle o que traz diminuição do acomplamento e melhor manutenção para as aplicações.

* Swagger é uma biblioteca para documentação de api's através dela é possível testar e documentar com rapidez, é fácil de usar e fornece uma integração muito boa com postman.
