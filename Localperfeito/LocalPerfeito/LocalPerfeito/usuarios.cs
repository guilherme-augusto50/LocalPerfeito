using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LocalPerfeito
{
    class usuarios
    {
        int id;
        string nome;
        string email;
        string senha;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }
        public static string CriptografarSenha(string Senha)
        {
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Senha));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }

                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível criptografar a senha: " + ex.Message, "Erro - Método Criptografar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
        public static bool verificarEmail(string email)
        {
            string emailValido = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailValido);
            return regex.IsMatch(email);
        }

        public void inserir()
        {
            try
            {
                if (!verificarEmail(Email))
                {
                    MessageBox.Show("Email inválido. Por favor, insira um email válido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string senhaCriptografada = CriptografarSenha(Senha);
                using (MySqlConnection conexao = new ConexaoDb().Conectar())
                {
                    string query = "INSERT INTO usuarios (nome, email, senha) VALUES (@nome, @email, @senha)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", Nome);
                        cmd.Parameters.AddWithValue("@email", Email);
                        cmd.Parameters.AddWithValue("@senha", senhaCriptografada);
                        int resultado = cmd.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            Console.WriteLine("Usuário inserido com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro ao inserir usuário.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir usuário: " + ex.Message);
            }
        }
        public bool logar()
        {
            try
            {
                string senhacriptografada = CriptografarSenha(Senha);
                using (MySqlConnection conexao = new ConexaoDb().Conectar())
                {
                    string query = "SELECT id_usuarios, nome FROM usuarios WHERE email = @Email AND senha = @Senha";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Senha", senhacriptografada);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = reader.GetInt32("id_usuarios");
                                nome = reader.GetString("nome");
                                Console.WriteLine("Usuário logado com sucesso: " + Nome);
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("Usuário ou senha inválidos.");
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao logar usuário: " + ex.Message);
            }
        }
    }
}
