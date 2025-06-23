using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRpgEtec.Models.Enuns;

namespace AppRpgEtec.Models
{
    public class Personagem
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public int PontosVida { get; set; }
        public int Froca { get; set; }
        public int Defesa { get; set; }
        public int Inteligencia { get; set; }
        public byte[] FotoPersonagem { get; set; }
        public int Disputas{ get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public ClasseEnum Classe { get; set; }

    }
}
