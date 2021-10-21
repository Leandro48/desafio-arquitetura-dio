using ApiCatalogoNetflix.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoNetflix.Repositories
{
    public interface ISerieRepository : IDisposable
    {
        Task<List<Serie>> Obter(int pagina, int quantidade);
        Task<Serie> Obter(Guid id);
        Task<List<Serie>> Obter(string nome, string produtora);
        Task Inserir(Serie serie);
        Task Atualizar(Serie serie);
        Task Remover(Guid id);
    }
}
