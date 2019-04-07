using System;

namespace GameScore.Models
{
    public class Pontuacao
    {
        public int Id { get; set; }
        public DateTime DataJogo { get; set; }
        public int QuantidadePontos { get; set; }
        public Guid UserId { get; set; }
    }
}
