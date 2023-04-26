using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoAgenda
{
    public partial class NovoAgendamento : Form
    {
        public NovoAgendamento()
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

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string valorTexto = txtValor.Text;
            CultureInfo cultureBR = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = cultureBR;
            Thread.CurrentThread.CurrentUICulture = cultureBR;
            float valorNumerico = float.Parse(valorTexto, cultureBR);

            valorTexto = valorTexto.Replace("R$", "").Trim();
            string msg = "Registro inserido com sucesso!!";
            try
            {
                if (ConectaBD())
                {
                    SqlCommand sql;
                    if (txtIdCliente1.Text == "")
                    {
                        sql = new SqlCommand("Insert into Agenda (Data, Evento, Valor, Hora,NomeCliente) values (@Data,@Evento,@Valor,@Hora,@Nome)", Conexao);

                    }
                    else
                    {
                        sql = new SqlCommand("Update Agenda set  NomeCliente=@Nome, Data=@Data, Hora=@Hora, Valor=@Valor, Evento=@Evento where ID=@Id", Conexao);
                        sql.Parameters.AddWithValue("@Id", txtIdCliente1.Text);

                    }

                    sql.Parameters.AddWithValue("@Valor", valorNumerico);
                    sql.Parameters.AddWithValue("@Nome", txtNome.Text);
                    sql.Parameters.AddWithValue("@Data", txtData.Text);
                    sql.Parameters.AddWithValue("@Hora", txtHora.Text);
                    sql.Parameters.AddWithValue("@Evento", txtDescricao.Text);
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
                    if (txtIdCliente1.Text == "")
                    {
                        if (MessageBox.Show(msg + "\n\nAdicionar um novo registro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnLimpar1_Click(sender, e);
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

        private void btnLimpar1_Click(object sender, EventArgs e)
        {
            txtData.Clear();
            txtNome.Clear();
            txtDescricao.Clear();
            txtHora.Clear();
            txtIdCliente1.Clear();
            txtValor.Clear();
        }

        private void txtValor_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }




        private void txtValor_TextChanged(object sender, EventArgs e)
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

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void NovoAgendamento_Load(object sender, EventArgs e)
        {

        }
    }
}
