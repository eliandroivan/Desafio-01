using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebServiceConsumo.Models;

namespace WebServiceConsumo.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            
            System.Net.WebClient wc = new System.Net.WebClient();
            string str = wc.DownloadString("http://localhost:54821/api/ListarPessoas");
            List<PessoaModelWS> Pessoas = new JavaScriptSerializer().Deserialize<List<PessoaModelWS>>(str);
            return View(Pessoas);            
        }






    }
}
