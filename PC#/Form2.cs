using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_
{
    public partial class Form2 : Form
    {
        int id;
        Banco banco = new Banco();

        public Form2(int id)
        {
            InitializeComponent();
            
            Produto produto = banco.selectUnico(id);
            this.id = id;

            textBox1.Text = produto.Nome;
            textBox2.Text = produto.Quantidade.ToString();
            textBox3.Text = produto.Preco.ToString();
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
                MessageBox.Show("O campo nome do produto está vazio", "Nome do produto");
                return;
            }

            if (textBox2 != null && textBox2.Text.Trim() != "")
            {
                try
                {
                    quantidade = int.Parse(textBox2.Text);
                    if (quantidade <= 0)
                    {
                        MessageBox.Show("A quantidade não pode ser zero ou menor que zero", "Quantidade");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("O valor inserido não corresponde a um número inteiro", "Quantidade");
                    return;
                }
            }
            else
            {
                MessageBox.Show("O campo quantidade do produto está vazio", "Quantidade");
                return;
            }

            if (textBox3 != null && textBox3.Text.Trim() != "")
            {
                try
                {
                    valor = float.Parse(textBox3.Text.Trim().Replace(".", ","));
                    if (valor <= 0)
                    {
                        MessageBox.Show("O valor não pode ser zero ou menor que zero", "Preço");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("O valor inserido no campo valor não corresponde a um número decimal", "Preço");
                    textBox3.Clear();
                    return;
                }

            }
            else
            {
                MessageBox.Show("O campo valor do produto está vazio", "Preço");
                return;
            }


            banco.atualizarProduto(id,nome,quantidade,valor);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
