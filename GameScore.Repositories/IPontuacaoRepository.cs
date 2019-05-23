using System;
using System.Collections.Generic;
using GameScore.Models;

namespace GameScore.Repositories
{
    public interface IPontuacaoRepository
    {
        int MaiorPontuacaoEmUmJogo(Guid userId);
        double MediaDePontosPorJogo(Guid userId);
        int MenorPontuacaoEmUmJogo(Guid userId);
        Pontuacao PeriodoTemporadaInicio(Guid userId);
        Pontuacao PeriodoTemporadaFim(Guid userId);
        int Record(Guid userId);
        Pontuacao RecordAtual(Guid userId, int record);
        int QuantidadeDeVezesBateuRecorde(Guid userId, int recordAtual);
        void Salvar(Pontuacao pontuacao);
        int TotalDeJogosDisputados(Guid userId);
        int TotalDePontosMarcadosNaTemporado(Guid userId);
        IList<Pontuacao> List(Guid userId);
        Pontuacao Get(int id);
        void Delete(Pontuacao pontuacao);
    }
}