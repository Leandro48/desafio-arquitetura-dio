using ApiCatalogoNetflix.InputModel;
using ApiCatalogoNetflix.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoNetflix.Services
{
    public interface ISerieService : IDisposable
    {
        Task<List<SerieViewModel>> Obter(int pagina, int quantidade);
        Task<SerieViewModel> Obter(Guid id);
        Task<SerieViewModel> Inserir(SerieInputModel jogo);
        Task Atualizar(Guid id, SerieInputModel jogo);
        Task Atualizar(Guid id, double nota);
        Task Remover(Guid id);
    }
}
