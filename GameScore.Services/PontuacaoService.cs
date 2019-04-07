using GameScore.Models;
using GameScore.Repositories;
using System;
using System.Collections.Generic;

namespace GameScore.Services
{
    public class PontuacaoService : IPontuacaoService
    {
        private readonly IPontuacaoRepository _pontuacaoRepository;

        public PontuacaoService(IPontuacaoRepository pontuacaoRepository)
        {
            _pontuacaoRepository = pontuacaoRepository;
        }

        public void Salvar(Pontuacao pontuacao)
        {
            if (pontuacao.DataJogo > DateTime.Now.Date)
                throw new Exception("A data do jogo não pode ser maior que a data de hoje");

            _pontuacaoRepository.Salvar(pontuacao);
        }

        public string PeriodoTemporada(Guid userId)
        {
            return _pontuacaoRepository.PeriodoTemporada(userId);
        }

        public int TotalDeJogosDisputados(Guid userId)
        {
            return _pontuacaoRepository.TotalDeJogosDisputados(userId);
        }

        public int TotalDePontosMarcadosNaTemporado(Guid userId)
        {
            return _pontuacaoRepository.TotalDePontosMarcadosNaTemporado(userId);
        }

        public double MediaDePontosPorJogo(Guid userId)
        {
            return _pontuacaoRepository.MediaDePontosPorJogo(userId);
        }

        public int MaiorPontuacaoEmUmJogo(Guid userId)
        {
            return _pontuacaoRepository.MaiorPontuacaoEmUmJogo(userId);
        }

        public int MenorPontuacaoEmUmJogo(Guid userId)
        {
            return _pontuacaoRepository.MenorPontuacaoEmUmJogo(userId);
        }

        public int QuantidadeDeVezesBateuRecorde(Guid userId)
        {
            return _pontuacaoRepository.QuantidadeDeVezesBateuRecorde(userId);
        }

        public IList<Pontuacao> List(Guid userId)
        {
            return _pontuacaoRepository.List(userId);
        }

        public Pontuacao Get(int id)
        {
            return _pontuacaoRepository.Get(id);
        }

        public void Delete(Pontuacao pontuacao)
        {
            _pontuacaoRepository.Delete(pontuacao);
        }
    }
}
