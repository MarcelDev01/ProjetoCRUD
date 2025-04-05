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

            List<UserViewModel> l_List = new List<UserViewModel>();
            UserViewModel l_Data = new UserViewModel();

            int l_NumberOption = 0;

            l_List = l_Connection.GetUsers();

            Console.WriteLine("Tabela de usuários");

            Console.WriteLine("");

            foreach (var item in l_List)
            {
                Console.WriteLine(item.ID.ToString() + " - ", item.Name + " - ", item.Email + " -", item.Birthday + " - ", item.Gender + " - ");
            }

            Console.WriteLine("");

            Console.WriteLine("========================================================");

            Console.WriteLine("");

            GetOptions();

            Console.WriteLine("");

            Console.Write("Opção: ");
            l_NumberOption = Console.Read();

            if(l_NumberOption == 0)
            {
                GetOptions();
            }
            else
            {
                if (l_NumberOption == 1)
                {
                    Console.Write("Nome: ");
                    l_Data.Name = Console.ReadLine();

                    Console.Write("Data de nascimento (DD/MM/YYYY): ");
                    string l_Birthday = Console.ReadLine();
                    // Fazer um tratamento ainda
                    //l_Data.Birthday = Convert.ToDateTime(l_Birthday);

                    Console.Write("Email: ");
                    l_Data.Email = Console.ReadLine();

                    Console.Write("Genêro M ou F: ");
                    l_Data.Gender = Console.ReadLine();

                    l_Connection.CreateUser(l_Data);
                }
                if (l_NumberOption == 2)
                {

                }
                if (l_NumberOption == 3)
                {

                }
                if (l_NumberOption == 4)
                {

                }
            }
        }

        static void GetOptions()
        {
            Console.WriteLine("Selecione um número de 1 a 4:");
            Console.WriteLine("");
            Console.WriteLine("1 - Criar um usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Editar um usuário");
            Console.WriteLine("4 - Listar usuários");
        }
    }
}
