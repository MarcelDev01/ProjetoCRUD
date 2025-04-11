using System;
using System.Collections.Generic;

namespace ProjetoCRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string l_ConnectionString = "Server=MARCEL\\SQLEXPRESS;Database=CRUD;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;";
            UserRepository l_Connection = new UserRepository(l_ConnectionString);

            List<UserViewModel> l_List = new List<UserViewModel>();
            UserViewModel l_Data = new UserViewModel();

            int l_NumberOption = 0;

            l_List = l_Connection.GetUsers();

            GetTemplateListUser(l_List);

            l_NumberOption = GetOptions();

            while (l_NumberOption == (int)UserRepository.MessageType.CREATE || 
                   l_NumberOption == (int)UserRepository.MessageType.DELETE || 
                   l_NumberOption == (int)UserRepository.MessageType.UPDATE)
            {

                switch (l_NumberOption)
                {
                    case (int)UserRepository.MessageType.CREATE:
                        CreateUserData(l_Data, l_Connection);
                        break;
                    case (int)UserRepository.MessageType.DELETE:
                        DeleteUserData(l_Connection, l_Data.ID);
                        break;
                    case (int)UserRepository.MessageType.UPDATE:
                        UpdateUserData(l_Connection, l_Data);
                        break;
                }

                l_List = l_Connection.GetUsers();

                GetTemplateListUser(l_List);

                l_NumberOption = GetOptions();
            }
        }

        static void GetTemplateListUser(List<UserViewModel> p_List)
        {
            Console.WriteLine("Tabela de usuários");
            Console.WriteLine("");

            foreach (var item in p_List)
            {
                Console.WriteLine(item.ID.ToString() + " - " + item.Name + " - " + item.Email + " - " + item.Birthday + " - " + item.Gender);
            }

            Console.WriteLine("");

            Console.WriteLine("========================================================");

            Console.WriteLine("");
        }

        static int GetOptions()
        {
            int l_NumberOption = 0;

            Console.WriteLine("Selecione um número de 1 a 3:");
            Console.WriteLine("");
            Console.WriteLine("1 - Criar um usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Editar um usuário");

            Console.WriteLine("");

            Console.Write("Opção: ");
            string l_NumberSelect = Console.ReadLine();
            int.TryParse(l_NumberSelect, out l_NumberOption);

            Console.WriteLine("");

            return l_NumberOption;
        }

        static void CreateUserData(UserViewModel p_Data, UserRepository l_Connection)
        {
            string l_Birthday = string.Empty;

            Console.Write("Nome: ");
            p_Data.Name = Console.ReadLine();

            Console.Write("Data de nascimento (DD/MM/YYYY): ");
            l_Birthday = Console.ReadLine();
            p_Data.Birthday = Convert.ToDateTime(l_Birthday);

            Console.Write("Email: ");
            p_Data.Email = Console.ReadLine();

            Console.Write("Genêro M ou F: ");
            p_Data.Gender = Console.ReadLine();

            l_Connection.CreateUser(p_Data);
        }

        static void DeleteUserData(UserRepository l_Connection, int p_Id)
        {
            Console.Write("Código de usuário (ID): ");
            string l_NumberSelect = Console.ReadLine();
            int.TryParse(l_NumberSelect, out p_Id);

            l_Connection.DeleteUser(p_Id);
        }

        static void UpdateUserData(UserRepository l_Connection, UserViewModel p_Data)
        {
            string l_Birthday = string.Empty;
            int p_Id = 0;

            Console.Write("Código de usuário (ID): ");
            string l_Id = Console.ReadLine();
            int.TryParse(l_Id, out p_Id);

            p_Data.ID = p_Id;

            Console.Write("Nome: ");
            p_Data.Name = Console.ReadLine();

            Console.Write("Data de nascimento (DD/MM/YYYY): ");
            l_Birthday = Console.ReadLine();
            p_Data.Birthday = Convert.ToDateTime(l_Birthday);

            Console.Write("Email: ");
            p_Data.Email = Console.ReadLine();

            Console.Write("Genêro M ou F: ");
            p_Data.Gender = Console.ReadLine();

            l_Connection.UpdateUser(p_Data);
        }
    }
}
