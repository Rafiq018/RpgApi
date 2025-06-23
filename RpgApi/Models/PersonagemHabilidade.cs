using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RpgApi.Models.Enuns;
using RpgApi.Models;

namespace RpgApi.Models
{
    public class PersonagemHabilidade
    {
        public Personagem? Personagem { get; set; } = null!;
        public int PersonagemId { get; set; }
        public int HabilidadeId { get; set; }
        public Habilidade? Habilidade { get; set; } = null!;

    }
    
}