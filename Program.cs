using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoCRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string l_ConnectionString = "Server=MARCEL\\SQLEXPRESS;Database=SISTEMA_NUTRICAO;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;";

            var l_Connection = new UserRepository(l_ConnectionString);

            l_Connection.GetUser();
        }
    }
}
