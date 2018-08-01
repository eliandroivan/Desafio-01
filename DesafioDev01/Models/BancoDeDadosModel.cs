using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace DesafioDev01.Models
{
    public class BancoDeDadosModel
    {
        public MySqlConnection ConexaoBD()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "localhost";
            conn_string.UserID = "root";
            conn_string.Password = "";
            conn_string.Database = "desafio_dev_01";
            conn_string.SslMode = MySqlSslMode.None;

            return new MySqlConnection(conn_string.ToString());
        }
    }
}