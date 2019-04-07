using GameScore.Models;
using GameScore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PontuacaoController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPontuacaoService _pontuacaoService;

        public PontuacaoController(UserManager<IdentityUser> userManager, IPontuacaoService pontuacaoService)
        {
            _userManager = userManager;
            _pontuacaoService = pontuacaoService;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Identity/Account");

            Guid guid = new Guid(userId);
            ViewBag.PeriodoTemporada = _pontuacaoService.PeriodoTemporada(guid);
            ViewBag.JogosDisputados = _pontuacaoService.TotalDeJogosDisputados(guid);
            ViewBag.TotalPontosTemporada = _pontuacaoService.TotalDePontosMarcadosNaTemporado(guid);
            ViewBag.MediaPontos = _pontuacaoService.MediaDePontosPorJogo(guid).ToString("F2");
            ViewBag.MaiorPontucao = _pontuacaoService.MaiorPontuacaoEmUmJogo(guid);
            ViewBag.MenorPontuacao = _pontuacaoService.MenorPontuacaoEmUmJogo(guid);
            ViewBag.QuantidadesVezesBateuRecord = _pontuacaoService.QuantidadeDeVezesBateuRecorde(guid);

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Identity/Account");

            PontuacaoModel model = new PontuacaoModel();
            model.DataJogo = DateTime.Now;

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PontuacaoModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                Guid userId = new Guid(_userManager.GetUserId(HttpContext.User));
                Pontuacao pontuacao = new Pontuacao
                {
                    DataJogo = model.DataJogo,
                    QuantidadePontos = model.QuantidadePontos,
                    UserId = userId
                };

                _pontuacaoService.Salvar(pontuacao);

                if (pontuacao.Id > 0)
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string message = string.Format("Atenção: {0}", ex.Message);
                ModelState.AddModelError(string.Empty, message);
            }

            return View(model);
        }

        public IActionResult Lancamentos()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            if (string.IsNullOrWhiteSpace(userId))
                return RedirectToAction("Login", "Identity/Account");

            Guid guid = new Guid(userId);

            IList<PontuacaoModel> list = _pontuacaoService.List(guid).Select(p => new PontuacaoModel
            {
                Id = p.Id,
                DataJogo = p.DataJogo,
                QuantidadePontos = p.QuantidadePontos
            }).ToList();

            return View(list);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            PontuacaoModel model = new PontuacaoModel();

            if (id > 0)
            {
                Pontuacao pontuacao = _pontuacaoService.Get(id);
                model.Id = pontuacao.Id;
                model.DataJogo = pontuacao.DataJogo;
                model.QuantidadePontos = pontuacao.QuantidadePontos;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(PontuacaoModel model)
        {
            try
            {
                if (model.Id != 0)
                {
                    Pontuacao pontuacao = _pontuacaoService.Get(model.Id);

                    _pontuacaoService.Delete(pontuacao);

                    return RedirectToAction("Lancamentos");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
