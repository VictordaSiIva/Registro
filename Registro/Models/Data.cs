using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Registro.Models
{
    public static class Data
    {
        private static string connectionString = @"Data Source=DESKTOP-GCQGKNO;Initial Catalog=Registro;Integrated Security=True;";

       

        public static void Executar(string nomeProcedure, DynamicParameters param = null)
        {
           

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                con.Execute(nomeProcedure, param, commandType: CommandType.StoredProcedure);

            }
        }

        public static IEnumerable<T> Listar<T>(string nomeProcedure, DynamicParameters param = null)
        {
           
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                return con.Query<T>(nomeProcedure, param, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
