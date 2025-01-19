using System.ComponentModel;

namespace PC_
{
    public partial class Form1 : Form
    {
        BindingList<Produto> produtos;
        Banco banco = new Banco();
        
        public Form1()
        {
            InitializeComponent();
            produtos = banco.lerBanco();
            dataGridView1.DataSource = produtos;
            dataGridView1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome;
            int quantidade;
            float valor;

            if (textBox1 != null && textBox1.Text.Trim() != "")
            {
                nome = textBox1.Text.Trim();
            }
            else
            {
                MessageBox.Show("O campo nome do produto est� vazio", "Nome do produto");
                return;
            }

            if (textBox2 != null && textBox2.Text.Trim() != "")
            {
                try
                {
                    quantidade = int.Parse(textBox2.Text);
                    if (quantidade <= 0)
                    {
                        MessageBox.Show("A quantidade n�o pode ser zero ou menor que zero", "Quantidade");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("O valor inserido n�o corresponde a um numero inteiro", "Quantidade");
                    return;
                }
            }
            else
            {
                MessageBox.Show("O campo quantidade do produto est� vazio", "Quantidade");
                return;
            }

            if (textBox3 != null && textBox3.Text.Trim() != "")
            {
                try
                {
                    valor = float.Parse(textBox3.Text.Trim().Replace(".", ","));
                    if (valor <= 0)
                    {
                        MessageBox.Show("O pre�o n�o pode ser zero ou menor que zero", "Pre�o");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("O valor inserido no campo valor n�o corresponde a um n�mero decimal", "Pre�o");
                    return;
                }

            }
            else
            {
                MessageBox.Show("O campo pre�o do produto est� vazio", "Pre�o");
                return;
            }

            dataGridView1.DataSource = null;
            banco.inserirProduto(nome, quantidade, valor);
            produtos = banco.lerBanco();
            dataGridView1.DataSource = produtos;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Deseja realmente limpar os dados? (essa a��o � irrevers�vel!)", "Limpar dados", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                banco.deleteCompleto();
                produtos = new BindingList<Produto>();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = produtos;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            {
                banco.deleteProduto(produtos[dataGridView1.CurrentCell.RowIndex].id);
                dataGridView1.DataSource = null;
                produtos = banco.lerBanco();
                dataGridView1.DataSource = produtos;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null) 
            {
                Form2 form2 = new Form2(produtos[dataGridView1.CurrentCell.RowIndex].id);
                if(form2.ShowDialog() == DialogResult.OK) 
                {
                    dataGridView1.DataSource = null;
                    produtos = banco.lerBanco();
                    dataGridView1.DataSource = produtos;
                }
            }
        }
    }
}
