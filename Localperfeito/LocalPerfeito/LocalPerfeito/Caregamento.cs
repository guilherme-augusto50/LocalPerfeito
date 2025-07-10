namespace LocalPerfeito
{
    public partial class carregamento : Form
    {
        public carregamento()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        bool jaAbriu = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!jaAbriu)
            {
                jaAbriu = true;
                timer1.Stop();

                Escolha escolha = new Escolha();
                escolha.Show();
                this.Hide();

            }
        }
    }
}
