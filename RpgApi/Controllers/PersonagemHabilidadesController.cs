using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;
namespace RpgApi.Controllers 
{ 
    [ApiController] 
    [Route("[controller]")] 
    public class PersonagemHabilidadesController : ControllerBase 
    { 
        private readonly DataContext _context;

        public PersonagemHabilidadesController(DataContext context) 
        { 
            _context = context; 
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagemHabilidadeAsync(PersonagemHabilidade novoPersonagemHabilidade)
        {
            try
            {
                Personagem? personagem = await _context.TB_PERSONAGENS
                .Include(p => p. Arma)  
                .Include (p => p. PersonagemHabilidades).ThenInclude (ps => ps. Habilidade) 
                .FirstOrDefaultAsync(p => p.Id == novoPersonagemHabilidade.PersonagemId); 
                if (personagem == null) 
                    throw new System.Exception("Personagem não encontrado para o Id Informado.");

                Habilidade? habilidade = await _context.TB_HABILIDADES.FirstOrDefaultAsync(h => h.Id == novoPersonagemHabilidade.HabilidadeId);

                if(habilidade == null)
                    throw new System.Exception("Habilidade não encontrada para o Id Informado.");

                PersonagemHabilidade ph = new PersonagemHabilidade();
                ph.Personagem = personagem;
                ph.Habilidade = habilidade;
                await _context.TB_PERSONAGENS_HABILIDADES.AddAsync(ph);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch(SystemException ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        //5---------------------
        [HttpGet("{idPersonagem}")]
        public async Task<IActionResult> GetPersonagemHabilidades(int idPersonagem)
        {
            try
            {
                List<PersonagemHabilidade> lista = await _context.TB_PERSONAGENS_HABILIDADES
                    .Where(ph => ph.PersonagemId == idPersonagem).ToListAsync();

                return Ok(lista);
            }
            catch(SystemException ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        //6---------------------
        [HttpGet("GetHabilidades")]
        public async Task<IActionResult> GetHabilidades()
        {
            try
            {
                List<Habilidade> lista = await _context.TB_HABILIDADES.ToListAsync();
                return Ok(lista);
            }
            catch(SystemException ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        //7---------------------
        [HttpPost("DeletePersonagemHabilidade")]
        public async Task<IActionResult> DeletePersonagemHabilidade(PersonagemHabilidade personagemHabilidade)
        {
            try
            {
                PersonagemHabilidade? ph = await _context.TB_PERSONAGENS_HABILIDADES
                    .FirstOrDefaultAsync(ph => ph.PersonagemId == personagemHabilidade.PersonagemId 
                    && ph.HabilidadeId == personagemHabilidade.HabilidadeId);

                _context.TB_PERSONAGENS_HABILIDADES.Remove(ph);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch(SystemException ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

    }
}