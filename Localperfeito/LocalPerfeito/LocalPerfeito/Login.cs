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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cadastro cadastroForm = new cadastro();
            cadastroForm.Show();
            this.Hide();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            try {
                if(!txtEmail.TabStop.Equals("") && !txtSenha.Text.Equals("")) {
                    usuarios usuario = new usuarios();
                    usuario.Email = txtEmail.Text;
                    usuario.Senha = txtSenha.Text;

                    if (!usuarios.verificarEmail(usuario.Email))
                    {
                        MessageBox.Show("Email inválido. Por favor, insira um email válido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (usuario.Senha.Length < 6 || usuario.Senha.Length == 6)
                    {
                        MessageBox.Show("A senha deve ser maior que 6 caracteres.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (usuario.logar())
                        {
                            MessageBox.Show("Login realizado com sucesso!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            telaprincipal telaprincipalForm = new telaprincipal();
                            telaprincipalForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Email ou senha incorretos.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao tentar logar: " + ex.Message, "Erro - Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
