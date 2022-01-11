using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.API.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        /* Data Base Estatica .NET
        public IEnumerable<Evento> _evento = new Evento[]{

                new Evento(){

                    EventoId=21,
                    Tema="Angular e .Net5",
                    Local="Coimbra (Data Base Estatica .NET)",
                    Lote="1º Lote",
                    QtdPessoas=250,
                    DataEvento=DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                    ImagemURL="Foto.png"
                },
                new Evento(){

                    EventoId=22,
                    Tema="Angular e .Net5",
                    Local="Praia de Mira (Data Base Estatica .NET)",
                    Lote="1º Lote",
                    QtdPessoas=250,
                    DataEvento=DateTime.Now.ToString("dd/MM/yyyy"),
                    ImagemURL="Foto.png"
                }
            
        };*/
       
        public IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService= eventoService;
           
            
        }

        //Metodos HTTP

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try{
                var eventos= await _eventoService.GetAllEventosAsync(true);
                if(eventos == null){
                    return NotFound("Nenhum evento encontrado!");
                }
                return Ok(eventos);

            }catch(Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");

            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try{
                var evento= await _eventoService.GetAllEventoByIdAsync(id, true);
                if(evento == null){
                    return NotFound("Eventos por id não encontrados!");
                }
                return Ok(evento);

            }catch(Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");

            }
            
        }
        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try{
                var evento= await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if(evento == null){
                    return NotFound("Eventos por tema não encontrados!");
                }
                return Ok(evento);

            }catch(Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");

            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try{
                var evento= await _eventoService.AddEventos(model);
                if(evento == null){
                    return BadRequest("Erro ao tentar adicionar eventos!");
                }
                return Ok(evento);

            }catch(Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");

            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try{
                var evento= await _eventoService.UpdateEvento(id, model);
                if(evento == null){
                    return BadRequest("Erro ao tentar atualizar o evento!");
                }
                return Ok(evento);

            }catch(Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar eventos. Erro: {ex.Message}");

            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try{
               
                if(await _eventoService.DeleteEvento(id)){
                    return Ok("Evento Apagado");
                }
                else{
                    return BadRequest("Evento não apagado!");
                }

            }catch(Exception ex){
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro apagar o evento. Erro: {ex.Message}");

            }
            
        }
    }
}
