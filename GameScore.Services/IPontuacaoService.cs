using GameScore.Models;
using System;
using System.Collections.Generic;

namespace GameScore.Services
{
    public interface IPontuacaoService
    {
        int MaiorPontuacaoEmUmJogo(Guid userId);
        double MediaDePontosPorJogo(Guid userId);
        int MenorPontuacaoEmUmJogo(Guid userId);
        string PeriodoTemporada(Guid userId);
        int QuantidadeDeVezesBateuRecorde(Guid userId);
        void Salvar(Pontuacao pontuacao);
        int TotalDeJogosDisputados(Guid userId);
        int TotalDePontosMarcadosNaTemporado(Guid userId);
        IList<Pontuacao> List(Guid userId);
        Pontuacao Get(int id);
        void Delete(Pontuacao pontuacao);
    }
}
