using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DesafioDev01.Models;
using MySql.Data.MySqlClient;
using X.PagedList;

namespace DesafioDev01.Controllers
{
    public class PessoaController : Controller
    {
        // GET: Pessoa
        public ActionResult Index(string Ordenacao = "", string TextoPesquisa = "", string ColunaPesquisa = "", string FiltroAtual = "", int pagina = 1)
        {
            ViewBag.Ordenacao = Ordenacao;
            ViewBag.ColunaPesquisa = ColunaPesquisa;
            if (TextoPesquisa == null)
            {
                TextoPesquisa = FiltroAtual.ToUpper();
            }
            ViewBag.TextoPesquisa = TextoPesquisa.ToUpper();
            PessoaModel lista = new PessoaModel();
            var listaOrdenada = from s in lista.PessoaLista()
                                select s;
            if (!String.IsNullOrEmpty(TextoPesquisa))
            {
                switch (ColunaPesquisa)
                {
                    case "Nome":
                        listaOrdenada = listaOrdenada.Where(s => s.Nome.Contains(TextoPesquisa.ToUpper()));
                        break;
                    case "CpfCnpj":
                        listaOrdenada = listaOrdenada.Where(s => s.CpfCnpj.Contains(TextoPesquisa.ToUpper()));
                        break;
                    case "Cidade":
                        listaOrdenada = listaOrdenada.Where(s => s.Cidade.Contains(TextoPesquisa.ToUpper()));
                        break;
                }
            }
            switch (Ordenacao)
            {
                case "Id":
                    listaOrdenada = listaOrdenada.OrderByDescending(s => s.Id);
                    break;
                case "Nome":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Nome);
                    break;
                case "CpfCnpj":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.CpfCnpj);
                    break;
                case "Bairro":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Bairro);
                    break;
                case "Cidade":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Cidade);
                    break;
                case "Estado":
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Estado);
                    break;
                default: 
                    listaOrdenada = listaOrdenada.OrderBy(s => s.Id);
                    break;
            }

            return View(listaOrdenada.ToPagedList(pagina, 5));
        }

        // GET: Pessoa/Details/5
        public ActionResult Detalhes(int id)
        {
            try
            {
                PessoaModel pessoa = new PessoaModel();
                return View(pessoa.PessoaItem(id));
            }
            catch (Exception ex)
            {
                ViewBag.mensagemErro = "Erro: " + ex.Message;
                return RedirectToAction("Index", "Pessoa"); ;
            }
            
        }

        // GET: Pessoa/Create
        public ActionResult Cadastro(int id=0)
        {
            try
            {
                if (id > 0)
                {
                    PessoaModel pessoa = new PessoaModel();
                    return View(pessoa.PessoaItem(id));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensagemErro = "Erro: " + ex.Message;
                return View();
            }
            
            
        }

        // POST: Pessoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(PessoaModel Pessoa)
        {
            
            try
            {
                // TODO: Add insert logic here
                //remove caracteres nao numericos
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
                Pessoa.CpfCnpj= reg.Replace(Pessoa.CpfCnpj, string.Empty);
               
                PessoaModel item = new PessoaModel();
                item.PessoaCadastrarEAtualizar(Pessoa);
                return RedirectToAction("Index");
            }
            
            catch (MySqlException ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                    ViewBag.mensagemErro = "CPF/CNPJ ja cadastrado.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.mensagemErro = "Erro: " + ex.Message;                
                return View();
            }
        }

        [HttpPost]
        public ActionResult Deletar(int id)
        {
            try
            {
                PessoaModel pessoa = new PessoaModel();
                pessoa.PessoaDeletar(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.mensagemErro = "Erro: " + ex.Message;
                throw;
            }
            
        }






    }
}
