using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjetoAgenda
{
    public partial class ConsultaAgenda : Form
    {
        public ConsultaAgenda()
        {
            InitializeComponent();
        }

        private void Agenda_Load(object sender, EventArgs e)
        {
            btnTeste_Click(sender, e);
        }

        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            Close();
            Inicio inicio = new Inicio();
            inicio.Show();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {


            textBox1.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }



        public void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void btnTeste_Click(object sender, EventArgs e)
        {
            try
            {
                if (NovoCliente.ConectaBD())
                    if (textBox1.Text == "")

                    {

                        SqlCommand sql = new SqlCommand("SELECT ID, NomeCliente, Data, CONVERT(varchar(5), Hora, 108) AS Hora," +
                                                        " Valor, Evento FROM Agenda", NovoCliente.Conexao);



                        SqlDataAdapter data = new SqlDataAdapter(sql);
                        DataSet tabela = new DataSet();
                        data.Fill(tabela);
                        dataGridView1.DataSource = tabela.Tables[0];

                        DataGridViewTextBoxColumn colunaValor = dataGridView1.Columns["Valor"] as DataGridViewTextBoxColumn;
                        colunaValor.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].Width = 100;
                        dataGridView1.Columns[1].HeaderText = "Cliente";
                        dataGridView1.Columns[2].Width = 100;
                        dataGridView1.Columns[2].HeaderText = "Data";
                        dataGridView1.Columns[3].Width = 50;
                        dataGridView1.Columns[3].HeaderText = "Hora";
                        dataGridView1.Columns[4].DefaultCellStyle.Format = "N2";
                        dataGridView1.Columns[4].Width = 70;
                        dataGridView1.Columns[4].HeaderText = "Valor";
                        dataGridView1.Columns[5].Width = 150;
                        dataGridView1.Columns[5].HeaderText = "Descrição";

                        double total = tabela.Tables[0].Compute("SUM(Valor)", "") as double? ?? 0;
                        txtTotalValor1.Text = "Total recebido: R$" + total.ToString("N2");


                        SqlCommand sqlCount = new SqlCommand("SELECT COUNT(*) FROM Agenda " , NovoCliente.Conexao);
                        int numeroLinhas = (int)sqlCount.ExecuteScalar();
                        txtTotalServicos.Text = "Total de serviços: " + numeroLinhas.ToString();


                    }
                    else
                    {


                        SqlCommand sql = new SqlCommand("Select ID,NomeCliente, Data,Hora, Valor, " +
                                                        "Evento from Agenda where Data =  @Data", NovoCliente.Conexao);
                        sql.Parameters.AddWithValue("@Data", textBox1.Text);




                        SqlDataAdapter data = new SqlDataAdapter(sql);
                        DataSet tabela = new DataSet();
                        data.Fill(tabela);
                        dataGridView1.DataSource = tabela.Tables[0];

                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].Width = 100;
                        dataGridView1.Columns[1].HeaderText = "Cliente";
                        dataGridView1.Columns[2].Width = 100;
                        dataGridView1.Columns[2].HeaderText = "Data";
                        dataGridView1.Columns[3].Width = 50;
                        dataGridView1.Columns[3].HeaderText = "Hora";
                        dataGridView1.Columns[4].Width = 50;
                        dataGridView1.Columns[4].HeaderText = "Valor";
                        dataGridView1.Columns[5].Width = 200;
                        dataGridView1.Columns[5].HeaderText = "Descrição";


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

        }

        private void btnNovoAgendamento_Click(object sender, EventArgs e)
        {
            NovoAgendamento novoAgendamento = new NovoAgendamento();
            novoAgendamento.Show();
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

                        NovoAgendamento agendamento = new NovoAgendamento();
                        agendamento.txtIdCliente1.Text = linha["ID"].ToString();
                        agendamento.txtNome.Text = linha["NomeCliente"].ToString();
                        agendamento.txtData.Text = linha["Data"].ToString();
                        agendamento.txtHora.Text = linha["Hora"].ToString();
                        agendamento.txtValor.Text = linha["Valor"].ToString();
                        agendamento.txtDescricao.Text = linha["Evento"].ToString();


                        agendamento.Show();


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
                btnTeste_Click(sender, e);

            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
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
                            SqlCommand sql = new SqlCommand("DELETE from Agenda Where ID = @Id", NovoCliente.Conexao);
                            sql.Parameters.AddWithValue("@Id", linha["ID"]);
                            sql.ExecuteNonQuery();

                            btnTeste_Click(sender, e);
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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            btnTeste_Click(sender, e);
            
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            DateTime dataInicial = dtInicial.Value;
            DateTime dataFinal = dtFinal.Value;
            string dataInicialFormatada = dataInicial.ToString("yyyy-MM-dd");
            string dataFinalFormatada = dataFinal.ToString("yyyy-MM-dd");

            try
            {
                if (NovoCliente.ConectaBD())

                {
                    SqlCommand sql = new SqlCommand("SELECT ID, NomeCliente, Data, CONVERT(varchar(5), Hora, 108) AS Hora, " +
                                                    "Valor, Evento FROM Agenda WHERE Data BETWEEN '"
                                                    + dataInicialFormatada + "' AND '" + dataFinalFormatada + "'", NovoCliente.Conexao);
                    SqlDataAdapter data = new SqlDataAdapter(sql);
                    DataSet tabela = new DataSet();
                    data.Fill(tabela);
                    dataGridView1.DataSource = tabela.Tables[0];

                    tabela.Tables[0].Columns["Valor"].DataType = typeof(double);
                    dataGridView1.DataSource = tabela.Tables[0];

                    // Exibe o total em um rótulo
                    double total = tabela.Tables[0].Compute("SUM(Valor)", "") as double? ?? 0;
                    txtTotalValor1.Text = "Total recebido: R$" + total.ToString("N2");


                    SqlCommand sqlCount = new SqlCommand("SELECT COUNT(*) FROM Agenda WHERE Data BETWEEN '"
                                              + dataInicialFormatada + "' AND '" + dataFinalFormatada + "'", NovoCliente.Conexao);
                    int numeroLinhas = (int)sqlCount.ExecuteScalar();
                    txtTotalServicos.Text = "Total de serviços: " + numeroLinhas.ToString();

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
    }
}

