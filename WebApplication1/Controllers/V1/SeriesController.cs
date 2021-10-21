using ApiCatalogoNetflix.Exceptions;
using ApiCatalogoNetflix.InputModel;
using ApiCatalogoNetflix.Services;
using ApiCatalogoNetflix.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoNetflix.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISerieService _serieService;

        public SeriesController(ISerieService serieService)
        {
            _serieService = serieService;
        }

        /// <summary>
        /// Buscar todos as séries de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar as séries sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de séries</response>
        /// <response code="204">Caso não haja séries</response>
        /// 
        //retornar várias séries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SerieViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            throw new Exception();

            var series = await _serieService.Obter(pagina, quantidade);

            if (series.Count() == 0)
                return NoContent();

            return Ok(series);
        }

        /// <summary>
        /// Buscar uma série pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id da série buscada</param>
        /// <response code="200">Retorna a série filtrada</response>
        /// <response code="204">Caso não haja série com este id</response>
        /// 
        //retornar apenas 1 série
        [HttpGet("{idSerie:guid}")]
        public async Task<ActionResult<SerieViewModel>> Obter([FromRoute] Guid idSerie)
        {
            var serie = await _serieService.Obter(idSerie);

            if (serie == null)
                return NoContent();

            return Ok(serie);
        }

        [HttpPost]
        public async Task<ActionResult<SerieViewModel>> InserirSerie([FromBody] SerieInputModel serieInputModel)
        {
            try
            {
                var serie = await _serieService.Inserir(serieInputModel);

                return Ok(serie);
            }
            //catch (SerieJaCadastradaException ex)
            catch (Exception ex)
            {
                return UnprocessableEntity("Já existe uma série com este nome para esta produtora");
            }
        }

        //atualiza o recurso inteiro
        [HttpPut("{idSerie:guid}")]
        public async Task<ActionResult> AtualizarSerie([FromRoute] Guid idSerie, [FromBody] SerieInputModel serieInputModel)
        {
            try
            {
                await _serieService.Atualizar(idSerie, serieInputModel);

                return Ok();
            }
            //catch (JogoNaoCadastradoException ex)
            catch(Exception ex)
            {
                return NotFound("Não existe esta série");
            }
        }

        //atualiza um único recurso
        [HttpPatch("{idSerie:guid}/nota/{nota:double}")]
        public async Task<ActionResult> AtualizarSerie([FromRoute] Guid idSerie, [FromRoute] double nota)
        {
            try
            {
                await _serieService.Atualizar(idSerie, nota);

                return Ok();
            }
            catch (SerieNaoCadastradaException ex)
            {
                return NotFound("Não existe esta serie");
            }
        }

        [HttpDelete("{idSerie:guid}")]
        public async Task<ActionResult> ApagarSerie([FromRoute] Guid idSerie)
        {
            try
            {
                await _serieService.Remover(idSerie);

                return Ok();
            }
            catch (SerieNaoCadastradaException ex)
            {
                return NotFound("Não existe esta serie");
            }
        }
    }
}
