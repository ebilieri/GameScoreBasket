using GameScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameScore.Repositories
{
    public class PontuacaoRepository : IPontuacaoRepository
    {
        private readonly GameScoreDBContext _gameScoreDBContext;

        public PontuacaoRepository(GameScoreDBContext gameScoreDBContext)
        {
            _gameScoreDBContext = gameScoreDBContext;
        }

        public void Salvar(Pontuacao pontuacao)
        {
            _gameScoreDBContext.Pontuacao.Add(pontuacao);
            _gameScoreDBContext.SaveChanges();
        }

        public string PeriodoTemporada(Guid userId)
        {
            var dataInicio = _gameScoreDBContext.Pontuacao.OrderBy(p => p.DataJogo).FirstOrDefault(p => p.UserId == userId);

            if (dataInicio != null)
            {
                var dataFim = _gameScoreDBContext.Pontuacao.OrderBy(p => p.DataJogo).LastOrDefault(p => p.UserId == userId);

                return $"{dataInicio.DataJogo.ToShortDateString()} até {dataFim.DataJogo.ToShortDateString()}";
            }
            else
            {
                return "Nenhum resultado lançado.";
            }


        }

        public int TotalDeJogosDisputados(Guid userId)
        {
            return _gameScoreDBContext.Pontuacao.Count(p => p.UserId == userId);
        }

        public int TotalDePontosMarcadosNaTemporado(Guid userId)
        {
            return _gameScoreDBContext.Pontuacao.Where(p => p.UserId == userId).Sum<Pontuacao>(p => p.QuantidadePontos);
        }

        public double MediaDePontosPorJogo(Guid userId)
        {
            if (TotalDeJogosDisputados(userId) > 0)
                return _gameScoreDBContext.Pontuacao.Where(p => p.UserId == userId).Average<Pontuacao>(p => p == null ? 0 : p.QuantidadePontos);
            else return 0;
        }

        public int MaiorPontuacaoEmUmJogo(Guid userId)
        {
            if (TotalDeJogosDisputados(userId) > 0)
                return _gameScoreDBContext.Pontuacao.Where(p => p.UserId == userId).Max<Pontuacao>(p => p.QuantidadePontos);
            else return 0;
        }

        public int MenorPontuacaoEmUmJogo(Guid userId)
        {
            if (TotalDeJogosDisputados(userId) > 0)
                return _gameScoreDBContext.Pontuacao.Where(p => p.UserId == userId).Min<Pontuacao>(p => p.QuantidadePontos);
            else return 0;
        }

        public int QuantidadeDeVezesBateuRecorde(Guid userId)
        {
            try
            {
                if (TotalDeJogosDisputados(userId) > 0)
                {
                    int record = _gameScoreDBContext.Pontuacao.OrderBy(p => p.DataJogo).OrderBy(p => p.QuantidadePontos)
                    .Where(p => p.UserId == userId && p.QuantidadePontos > 0).FirstOrDefault().QuantidadePontos;

                    var recordAtual = _gameScoreDBContext.Pontuacao.OrderBy(p => p.DataJogo).OrderBy(p => p.QuantidadePontos)
                    .Where(p => p.UserId == userId && p.QuantidadePontos > record).FirstOrDefault();

                    if (recordAtual != null)
                        return _gameScoreDBContext.Pontuacao
                            .Where(p => p.UserId == userId && p.QuantidadePontos >= recordAtual.QuantidadePontos)
                            .OrderBy(p => p.DataJogo).OrderBy(p => p.QuantidadePontos)
                            .Select(p => p.QuantidadePontos).Distinct().ToList().Count();
                    else
                        return 0;
                }
                else return 0;
            }
            catch { return 0; }
        }

        public IList<Pontuacao> List(Guid userId)
        {
            return _gameScoreDBContext.Pontuacao.Where(p => p.UserId == userId).OrderBy(p => p.DataJogo).ToList();
        }

        public Pontuacao Get(int id)
        {
            return _gameScoreDBContext.Pontuacao.Find(id);
        }

        public void Delete(Pontuacao pontuacao)
        {
            _gameScoreDBContext.Pontuacao.Remove(pontuacao);
            _gameScoreDBContext.SaveChanges();
        }
    }
}