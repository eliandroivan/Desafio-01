using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace DesafioDev01.Models
{
    public class PessoaModel
    {
        [Key]
        public int Id { get; set; }
                
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Nome")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo excedido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Número do CPF/CNPJ")]
        [CustomValidationCPFCNPJAttribute(ErrorMessage = "CPF/CNPJ inválido")]
        [MaxLength(18, ErrorMessage = "Tamanho máximo excedido")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Insira um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Logradouro", Description = "Digite o nome da rua/avenida.")]
        [MaxLength(200, ErrorMessage = "Tamanho máximo excedido")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Número", Description = "Digite o número do lote.")]
        [MaxLength(10, ErrorMessage = "Tamanho máximo excedido")]
        public string LogradouroNumero { get; set; }

        [Display(Name = "Complemento")]
        [MaxLength(20, ErrorMessage = "Tamanho máximo excedido")]
        public string LogradouroComplemento { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Bairro")]
        [MaxLength(40, ErrorMessage = "Tamanho máximo excedido")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(9, ErrorMessage = "Tamanho máximo excedido")]
        [RegularExpression("^[0-9]{5}-[0-9]{3}$", ErrorMessage = "Você deve digitar o CEP no formato #####-###")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Cidade")]
        [MaxLength(40, ErrorMessage = "Tamanho máximo excedido")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [Display(Name = "Estado")]
        [MaxLength(2, ErrorMessage = "Tamanho máximo excedido, digite a sigla do estado.")]
        public string Estado { get; set; }







        public List<PessoaModel> PessoaLista()
        {
            List<PessoaModel> listaPessoa = new List<PessoaModel>();
            string query = "select * from pessoa";
            
            BancoDeDadosModel bd = new BancoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    PessoaModel pessoa = new PessoaModel();
                    pessoa.Id = leitor.GetInt32("id");
                    pessoa.Nome = leitor.GetString("nome");
                    pessoa.CpfCnpj = leitor.GetString("cpf_cnpj");
                    pessoa.Email = leitor.GetString("email");
                    pessoa.Logradouro = leitor.GetString("logradouro");
                    pessoa.LogradouroNumero = leitor.GetString("logradouro_numero");
                    if (leitor.IsDBNull(6))
                    {
                        pessoa.LogradouroComplemento = null;
                    }
                    else
                    {
                        pessoa.LogradouroComplemento = leitor.GetString("logradouro_complemento");
                    }
                    // dados NULL dá erro:::                        pessoa.LogradouroComplemento = leitor.GetString("logradouro_complemento");
                    pessoa.Bairro = leitor.GetString("bairro");
                    pessoa.CEP = leitor.GetString("cep");
                    pessoa.Cidade = leitor.GetString("cidade");
                    pessoa.Estado = leitor.GetString("estado");
                    listaPessoa.Add(pessoa);
                }
                leitor.Close();
            }
            return listaPessoa;



        }



        public PessoaModel PessoaItem(int id)
        {
            PessoaModel Pessoa = new PessoaModel();
            StringBuilder query = new StringBuilder();

            query.AppendLine("select * ");
            query.AppendLine(" from pessoa");
            query.AppendLine(" where id = ");
            query.AppendLine(id.ToString());
           
            BancoDeDadosModel bd = new BancoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();

            using (MySqlCommand comando = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    
                    Pessoa.Id = leitor.GetInt32("id");
                    Pessoa.Nome = leitor.GetString("nome");
                    Pessoa.CpfCnpj = leitor.GetString("cpf_cnpj");
                    Pessoa.Email = leitor.GetString("email");
                    Pessoa.Logradouro = leitor.GetString("logradouro");
                    Pessoa.LogradouroNumero = leitor.GetString("logradouro_numero");
                    if (leitor.IsDBNull(6))
                    {
                        Pessoa.LogradouroComplemento = null;
                    }
                    else
                    {
                        Pessoa.LogradouroComplemento = leitor.GetString("logradouro_complemento");
                    }
                    // dados NULL dá erro:::                        pessoa.LogradouroComplemento = leitor.GetString("logradouro_complemento");
                    Pessoa.Bairro = leitor.GetString("bairro");
                    Pessoa.CEP = leitor.GetString("cep");
                    Pessoa.Cidade = leitor.GetString("cidade");
                    Pessoa.Estado = leitor.GetString("estado");
                }
                leitor.Close();
            }
            return Pessoa;
        }


        public void PessoaCadastrarEAtualizar(PessoaModel Pessoa)
        {
            BancoDeDadosModel bd = new BancoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            StringBuilder query = new StringBuilder();
            if (Pessoa.Id>0)
            {
                query.AppendLine("UPDATE ");
                query.AppendLine("pessoa ");
                query.AppendLine("SET ");
                query.AppendLine("nome=@nome,cpf_cnpj=@cpf_cnpj,email=@email,logradouro=@logradouro,logradouro_numero=@logradouro_numero,logradouro_complemento=@logradouro_complemento,bairro=@bairro,cidade=@cidade,estado=@estado,cep=@cep ");
                query.AppendLine("WHERE ");
                query.AppendLine("id = ");
                query.AppendLine(Pessoa.Id.ToString());
            }
            else
            {
                query.AppendLine("INSERT INTO ");
                query.AppendLine("pessoa ");
                query.AppendLine("(nome,cpf_cnpj,email,logradouro,logradouro_numero,logradouro_complemento,bairro,cidade,estado,cep) ");
                query.AppendLine("VALUES ");
                query.AppendLine("(@nome,@cpf_cnpj,@email,@logradouro,@logradouro_numero,@logradouro_complemento,@bairro,@cidade,@estado,@cep)");
            }

            using (MySqlCommand comando2 = new MySqlCommand(query.ToString(), conexao))
            {
                conexao.Open();                
                comando2.Parameters.AddWithValue("@nome", Pessoa.Nome.ToUpper());
                comando2.Parameters.AddWithValue("@cpf_cnpj", Pessoa.CpfCnpj);
                comando2.Parameters.AddWithValue("@email", Pessoa.Email.ToLower());
                comando2.Parameters.AddWithValue("@logradouro", Pessoa.Logradouro.ToUpper());
                comando2.Parameters.AddWithValue("@logradouro_numero", Pessoa.LogradouroNumero.ToUpper());
                if (Pessoa.LogradouroComplemento==null)
                {
                    comando2.Parameters.AddWithValue("@logradouro_complemento", "");
                }
                else
                {
                    comando2.Parameters.AddWithValue("@logradouro_complemento", Pessoa.LogradouroComplemento.ToUpper());
                }                
                comando2.Parameters.AddWithValue("@bairro", Pessoa.Bairro.ToUpper());
                comando2.Parameters.AddWithValue("@cidade", Pessoa.Cidade.ToUpper());
                comando2.Parameters.AddWithValue("@estado", Pessoa.Estado.ToUpper());
                comando2.Parameters.AddWithValue("@cep", Pessoa.CEP);
                comando2.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void PessoaDeletar(int id)
        {
        
            BancoDeDadosModel bd = new BancoDeDadosModel();
            MySqlConnection conexao = bd.ConexaoBD();
            string query = "DELETE FROM pessoa WHERE id = " + id.ToString();
            using (MySqlCommand comando2 = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                comando2.ExecuteNonQuery();
                conexao.Close();
            }            
        }
    
    }
}