using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DesafioDev01.Models;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace DesafioDev01.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Entrar()
        {
            var nome = User.Identity.Name; // obter nome do usuario
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Entrar(FormCollection inputs)
        {
            inputs["LoginUsuario"].ToUpper();
            inputs["LoginSenha"].ToUpper();
            MD5 md5Hasher = MD5.Create();
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(inputs["LoginSenha"]));
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }
            strBuilder.ToString();

            string query = "select * from usuario WHERE login_usuario= '" + inputs["LoginUsuario"] + "' and login_senha = '" + strBuilder.ToString() + "'";
            BancoDeDadosModel bd = new BancoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            try
            {
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    MySqlDataReader leitor = comando.ExecuteReader();
                    if (leitor.HasRows)
                    {
                        FormsAuthentication.SetAuthCookie(inputs["LoginUsuario"], false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.mensagemErro = "login invalido";
                    }
                    leitor.Close();
                }
                return View();
            }
            catch (MySqlException ex)
            {
                if (ex.Message.Contains("Unable to connect"))
                    ViewBag.mensagemErro = "Nao foi possivel conectar no banco de dados.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.mensagemErro = "Erro: " + ex.Message;
                return View(); ;
            }

        }

        // GET: Login
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Entrar");
        }
    }
}