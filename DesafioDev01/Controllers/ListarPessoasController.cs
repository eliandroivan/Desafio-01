using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DesafioDev01.Models;
using System.Web.Script.Serialization;
using System.Collections;

namespace DesafioDev01.Controllers
{
    public class ListarPessoasController : ApiController
    {
        // GET: api/ListarPessoas
        public IEnumerable Get()
        {
            var lista = new PessoaModel().PessoaLista().OrderBy(n => n.Id);
            return lista;
        }

        // GET: api/ListarPessoas/5
        public PessoaModel Get(int id)
        {
            PessoaModel pessoa = new PessoaModel();
            return pessoa.PessoaItem(id);
           
        }

    }
}
