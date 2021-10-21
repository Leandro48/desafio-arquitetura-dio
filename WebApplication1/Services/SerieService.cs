using ApiCatalogoNetflix.Entities;
using ApiCatalogoNetflix.Exceptions;
using ApiCatalogoNetflix.InputModel;
using ApiCatalogoNetflix.Repositories;
using ApiCatalogoNetflix.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoNetflix.Services
{
    public class SerieService : ISerieService
    {
        private readonly ISerieRepository _serieRepository;

        public SerieService(ISerieRepository serieRepository)
        {
            _serieRepository = serieRepository;
        }

        public async Task<List<SerieViewModel>> Obter(int pagina, int quantidade)
        {
            var series = await _serieRepository.Obter(pagina, quantidade);

            return series.Select(serie => new SerieViewModel
            {
                                    Id = serie.Id,
                                    Nome = serie.Nome,
                                    Produtora = serie.Produtora,
                                    Nota = serie.Nota
            })
                               .ToList();
        }

        public async Task<SerieViewModel> Obter(Guid id)
        {
            var serie = await _serieRepository.Obter(id);

            if (serie == null)
                return null;

            return new SerieViewModel
            {
                Id = serie.Id,
                Nome = serie.Nome,
                Produtora = serie.Produtora,
                Nota = serie.Nota
            };
        }

        public async Task<SerieViewModel> Inserir(SerieInputModel serie)
        {
            var entidadeSerie = await _serieRepository.Obter(serie.Nome, serie.Produtora);

            if (entidadeSerie.Count > 0)
                throw new SerieJaCadastradaException();

            var serieInsert = new Serie
            {
                Id = Guid.NewGuid(),
                Nome = serie.Nome,
                Produtora = serie.Produtora,
                Nota = serie.Nota
            };

            await _serieRepository.Inserir(serieInsert);

            return new SerieViewModel
            {
                Id = serieInsert.Id,
                Nome = serie.Nome,
                Produtora = serie.Produtora,
                Nota = serie.Nota
            };
        }

        public async Task Atualizar(Guid id, SerieInputModel serie)
        {
            var entidadeSerie = await _serieRepository.Obter(id);

            if (entidadeSerie == null)
                throw new SerieNaoCadastradaException();

            entidadeSerie.Nome = serie.Nome;
            entidadeSerie.Produtora = serie.Produtora;
            entidadeSerie.Nota = serie.Nota;

            await _serieRepository.Atualizar(entidadeSerie);
        }

        public async Task Atualizar(Guid id, double nota)
        {
            var entidadeSerie = await _serieRepository.Obter(id);

            if (entidadeSerie == null)
                throw new SerieNaoCadastradaException();

            entidadeSerie.Nota = nota;

            await _serieRepository.Atualizar(entidadeSerie);
        }

        public async Task Remover(Guid id)
        {
            var serie = await _serieRepository.Obter(id);

            if (serie == null)
                throw new SerieNaoCadastradaException();

            await _serieRepository.Remover(id);
        }

        public void Dispose()
        {
            _serieRepository?.Dispose();
        }
    }
}
