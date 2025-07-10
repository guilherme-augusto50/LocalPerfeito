using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LocalPerfeito
{
    public partial class cadastro : Form
    {
        public cadastro()
        {
            InitializeComponent();
        }

        private void btncadastrese_Click(object sender, EventArgs e)
        {
            try {
                if (!txtNome.Text.Equals("") && !txtEmail.Text.Equals("") && !txtSenha.Text.Equals(""))
                {
                    usuarios usuario = new usuarios();
                    usuario.Nome = txtNome.Text;
                    usuario.Email = txtEmail.Text;
                    usuario.Senha = txtSenha.Text;
                    if (!usuarios.verificarEmail(usuario.Email))
                    {
                        MessageBox.Show("Email inválido. Por favor, insira um email válido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (usuario.Senha.Length < 6 || usuario.Senha.Length ==6)
                    {
                        MessageBox.Show("A senha deve ser maior que 6 caracteres.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                    else
                    {
                        usuario.inserir();
                        MessageBox.Show("Usuário cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Login login = new Login();
                        login.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Preencha todos os campos.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message, "Erro - Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
