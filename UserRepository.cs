﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjetoCRUD
{
    public class UserRepository 
    {
        private string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateUser(UserViewModel p_Data)
        {

            string l_Sql = "INSERT INTO TB_USER (ID, NAME, BIRTHDAY, EMAIL, GENDER) VALUES(@Id, @Nome, @Nascimento, @Email, @Genero)";

            //Cria a conexão, passando seu endereço.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                //Abrimos a conexão
                connection.Open();
                
                //Reutilizamos a conexão aberta para não ter quer criar uma nova.
                int l_NextId = GetNextID(connection);

                //Passamos a query, na conexão aberta pelo o endereço passado.
                using (SqlCommand cmd = new SqlCommand(l_Sql, connection))
                {
                    //Atribuição nos parametros da query
                    cmd.Parameters.AddWithValue("@Nome", p_Data.Name);
                    cmd.Parameters.AddWithValue("@Nascimento", p_Data.Birthday.ToString());
                    cmd.Parameters.AddWithValue("@Email", p_Data.Email);
                    cmd.Parameters.AddWithValue("@Genero", p_Data.Gender);
                    cmd.Parameters.AddWithValue("@Id", l_NextId);

                    //Executa
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{linhasAfetadas} linha(s) inserida(s).");
                }
            }
        }

        public void DeleteUser(int p_Id)
        {

            string l_Sql = "DELETE FROM TB_USER WHERE ID = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(l_Sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", p_Id);

                    //Executa
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{linhasAfetadas} linha(s) excluida(s).");
                }
            }
        }

        public void UpdateUser(UserViewModel p_Data)
        {
            string l_Sql = "UPDATE TB_USER SET NAME = @Nome, BIRTHDAY = @Nascimento, EMAIL = @Email, GENDER = @Genero WHERE ID = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(l_Sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Nome", p_Data.Name);
                    cmd.Parameters.AddWithValue("@Nascimento", p_Data.Birthday.ToString());
                    cmd.Parameters.AddWithValue("@Email", p_Data.Email);
                    cmd.Parameters.AddWithValue("@Genero", p_Data.Gender);
                    cmd.Parameters.AddWithValue("@Id", p_Data.ID);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{linhasAfetadas} linha(s) atualizada(s).");
                }
            }
        }

        public List<UserViewModel> GetUsers()
        {
            try
            {
                List<UserViewModel> p_List = new List<UserViewModel>();

                string l_Sql = "SELECT * FROM TB_USER";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(l_Sql, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserViewModel user = new UserViewModel
                            {
                                ID = Convert.IsDBNull(reader["ID"]) ? 0 : Convert.ToInt32(reader["ID"]),
                                Name = Convert.IsDBNull(reader["NAME"]) ? string.Empty : reader["NAME"].ToString(),
                                Birthday = Convert.IsDBNull(reader["BIRTHDAY"]) ? DateTime.MinValue : Convert.ToDateTime(reader["BIRTHDAY"]),
                                Email = Convert.IsDBNull(reader["EMAIL"]) ? string.Empty : reader["EMAIL"].ToString(),
                                Gender = Convert.IsDBNull(reader["GENDER"]) ? string.Empty : reader["GENDER"].ToString()
                            };

                            p_List.Add(user);
                        }
                    }
                }

                return p_List;

            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erro ao consultar usuarios: " + ex.Message);

                // Caso de erro, retornamos a lista vazia
                return new List<UserViewModel>();

                // Caso de erro, jogamos para o método que chamou GetUsers, apartir dali ele se virar para tratar o problema.
                //throw; 
            }
        }

        /// <summary>
        /// Método para obter o próximo ID da tabela de usuários
        /// </summary>
        /// <param name="p_Connection"></param>
        /// <returns>Id do usuário</returns>
        public int GetNextID(SqlConnection p_Connection)
        {
            string sql = "SELECT ISNULL(MAX(ID), 0) + 1 FROM TB_USER";

            using (SqlCommand cmd = new SqlCommand(sql, p_Connection))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

    }
}
