using ApiCatalogoNetflix.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoNetflix.Repositories
{
    public class SerieRepository : ISerieRepository 
    { 

    private static Dictionary<Guid, Serie> series = new Dictionary<Guid, Serie>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Serie{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Fifa 21", Produtora = "EA", Nota = 3.5} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Serie{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "Fifa 20", Produtora = "EA", Nota = 4.0} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Serie{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "Fifa 19", Produtora = "EA", Nota = 3.0} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Serie{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Fifa 18", Produtora = "EA", Nota = 7.5} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Serie{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "Street Fighter V", Produtora = "Capcom", Nota = 8.5} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Serie{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Nota = 7.6} }
        };

        public Task<List<Serie>> Obter(int pagina, int nota)
        {
            return Task.FromResult(series.Values.Skip((pagina - 1) * nota).Take(nota).ToList());
        }

        public Task<Serie> Obter(Guid id)
        {
            if (!series.ContainsKey(id))
                return Task.FromResult<Serie>(null);

            return Task.FromResult(series[id]);
        }

        public Task<List<Serie>> Obter(string nome, string produtora)
        {
            return Task.FromResult(series.Values.Where(serie => serie.Nome.Equals(nome) && serie.Produtora.Equals(produtora)).ToList());
        }

        public Task<List<Serie>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Serie>();

            foreach(var serie in series.Values)
            {
                if (serie.Nome.Equals(nome) && serie.Produtora.Equals(produtora))
                    retorno.Add(serie);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Serie serie)
        {
            series.Add(serie.Id, serie);
            return Task.CompletedTask;
        }

        public Task Atualizar(Serie serie)
        {
            series[serie.Id] = serie;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            series.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
