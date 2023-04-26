using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoAgenda
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {

           
            ConsultaClientes clientes = new ConsultaClientes();
            clientes.Show();
            this.Hide();
        }

     

        private void btnNovoCliente_Click(object sender, EventArgs e)
        {
            NovoCliente novoCliente = new NovoCliente();
            novoCliente.Show();
            this.Hide();
        }

        private void btnAgenda_Click(object sender, EventArgs e)
        {
            ConsultaAgenda calendario = new ConsultaAgenda();
            calendario.Show();
            this.Hide();
        }
    }
}
