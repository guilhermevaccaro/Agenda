using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;



namespace ProjetoAgenda
{
    public partial class ConsultaClientes : Form
    {
        public ConsultaClientes()
        {
            InitializeComponent();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Close();
            Inicio inicio = new Inicio();
            inicio.Show();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            NovoCliente cliente = new NovoCliente();
            cliente.Show();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                if (NovoCliente.ConectaBD())
                {
                    SqlCommand sql = new SqlCommand("Select IdCliente, NomeCliente, TelefoneCliente from Clientes Where NomeCliente like @Nome ORDER BY NomeCliente", NovoCliente.Conexao);
                    sql.Parameters.AddWithValue("@Nome", "%" + txtPesquisar.Text + "%");


                    SqlDataAdapter data = new SqlDataAdapter(sql);
                    DataSet tabela = new DataSet();
                    data.Fill(tabela);
                    dataGridView1.DataSource = tabela.Tables[0];

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Width = 300;
                    dataGridView1.Columns[1].HeaderText = "Cliente";
                    dataGridView1.Columns[2].Width = 250;
                    dataGridView1.Columns[2].HeaderText = "Telefone";

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                NovoCliente.Conexao.Close();

            }
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (NovoCliente.ConectaBD())
                {
                    int indice = -1;

                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        indice = dataGridView1.SelectedRows[0].Index;
                    }
                    else
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            indice = dataGridView1.SelectedCells[0].RowIndex;
                        }
                    }

                    if (indice != -1)
                    {
                        DataRowView linha = (DataRowView)dataGridView1.Rows[indice].DataBoundItem;

                        if (MessageBox.Show("Confirma a exclusão " + linha["NomeCliente"].ToString() + "?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            SqlCommand sql = new SqlCommand("DELETE from Clientes Where IdCliente = @Id", NovoCliente.Conexao);
                            sql.Parameters.AddWithValue("@Id", linha["IdCliente"]);
                            sql.ExecuteNonQuery();

                            btnPesquisar_Click(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhum cliente selecionado. Verifique", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                NovoCliente.Conexao.Close();

            }
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            btnPesquisar_Click(sender, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (NovoCliente.ConectaBD())
                {
                    int indice = -1;

                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        indice = dataGridView1.SelectedRows[0].Index;
                    }
                    else
                    {
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            indice = dataGridView1.SelectedCells[0].RowIndex;
                        }
                    }

                    if (indice != -1)
                    {
                        DataRowView linha = (DataRowView)dataGridView1.Rows[indice].DataBoundItem;

                        NovoCliente cliente = new NovoCliente();
                        cliente.txtIdCliente.Text = linha["IdCliente"].ToString();
                        cliente.txtNome.Text = linha["NomeCliente"].ToString();
                        cliente.txtTelefone.Text = linha["TelefoneCliente"].ToString();
                        cliente.Show();


                    }
                    else
                    {
                        MessageBox.Show("Nenhum cliente selecionado. Verifique", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                NovoCliente.Conexao.Close();
                btnPesquisar_Click(sender, e);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


