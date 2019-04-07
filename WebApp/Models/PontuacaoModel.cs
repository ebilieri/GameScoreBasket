using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class PontuacaoModel
    {
        public int Id { get; set; }

        [Display(Name = "Data do jogo")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataJogo { get; set; }

        [Display(Name = "Quantidade de pontos você fez")]
        public int QuantidadePontos { get; set; }

    }
}
