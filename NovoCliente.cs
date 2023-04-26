using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;
using System.Data;
using System.Linq.Expressions;

namespace ProjetoAgenda
{
    public partial class NovoCliente : Form
    {

        public NovoCliente()
        {
            InitializeComponent();
        }

        public static SqlConnectionStringBuilder strConexao;
        public static SqlConnection Conexao;

        public static bool ConectaBD()
        {
            bool conectado = true;
            try
            {
                strConexao = new SqlConnectionStringBuilder();
                strConexao.DataSource = "DESKTOP-JUCA1OL\\SQLEXPRESS";
                strConexao.IntegratedSecurity = true;
                strConexao.InitialCatalog = "BancoTeste";


                Conexao = new SqlConnection(strConexao.ConnectionString);
                Conexao.Open();

            }
            catch (SqlException e)
            {
                conectado = false;
                MessageBox.Show("Erro ao Conectar ao sqlServer");
            }
            return conectado;
        }

        private void NovoCliente_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            string msg = "Registro inserido com sucesso!!";
            try
            {
                if (ConectaBD())
                {
                    SqlCommand sql;
                    if (txtIdCliente.Text == "")
                    {
                        sql = new SqlCommand("Insert into Clientes (NomeCliente, TelefoneCliente) values (@Nome,@Telefone)", Conexao);

                    }
                    else
                    {
                        sql = new SqlCommand("Update Clientes set  NomeCliente=@Nome, TelefoneCliente=@Telefone where IdCliente=@Id", Conexao);
                        sql.Parameters.AddWithValue("@Id", txtIdCliente.Text);

                    }

                    sql.Parameters.AddWithValue("@Nome", txtNome.Text);
                    sql.Parameters.AddWithValue("@Telefone", txtTelefone.Text);
                    sql.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
                if (msg != "")
                {
                    if (txtIdCliente.Text == "")
                    {
                        if (MessageBox.Show(msg + "\n\nAdicionar um novo registro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnLimpar_Click(sender, e);
                            txtNome.Focus();
                        }
                        else
                        {
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Registro Atualizado com sueccso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
        }

        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            Close();
            Inicio inicio = new Inicio();
            inicio.Show();
        }

        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtIdCliente.Clear();
            txtNome.Clear();
            txtTelefone.Clear();
        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label_Click(object sender, EventArgs e)
        {

        }
    }
}
