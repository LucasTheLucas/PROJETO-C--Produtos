using System;
using System.ComponentModel;
using System.Data.SQLite;

public class Banco
{
    string enderecoDoArquivo = "C:/banco/";
    string enderecoCompleto = "C:/banco/banco.db";

    string criarTabela = @"CREATE TABLE produto (
                         id INTEGER PRIMARY KEY AUTOINCREMENT,
                         nome TEXT NOT NULL,
                         quantidade INTEGER NOT NULL,
                         preco REAL NOT NULL);";


    public Banco()
    {
        if (!Directory.Exists(enderecoDoArquivo))
        {
            Directory.CreateDirectory(enderecoDoArquivo);
            SQLiteConnection.CreateFile(enderecoCompleto);

            SQLiteConnection conn = new SQLiteConnection($"Data Source={enderecoCompleto};Version=3;");
            conn.Open();

            SQLiteCommand comando = new SQLiteCommand(criarTabela, conn);
            comando.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            if (!File.Exists(enderecoCompleto))
            {
                SQLiteConnection.CreateFile(enderecoCompleto);

                SQLiteConnection conn = new SQLiteConnection($"Data Source={enderecoCompleto};Version=3;");
                conn.Open();

                SQLiteCommand comando = new SQLiteCommand(criarTabela, conn);
                comando.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
    

    public void inserirProduto(string nome, int quantidade, float valor)
    {
        string valorSTR = valor.ToString().Replace(",", ".");
        string insert = $"INSERT INTO produto(nome,quantidade,preco) values('{nome}',{quantidade},{valorSTR})";

        SQLiteConnection conn = new SQLiteConnection($"Data Source={enderecoCompleto};Version=3;");
        conn.Open();

        SQLiteCommand comando = new SQLiteCommand(insert, conn);

        comando.ExecuteNonQuery();
        conn.Close();
    }


    public void deleteCompleto()
    {
        string delete = "DELETE FROM produto";
        SQLiteConnection conn = new SQLiteConnection($"Data Source={enderecoCompleto};Version=3;");
        conn.Open();

        SQLiteCommand comando = new SQLiteCommand(delete, conn);

        comando.ExecuteNonQuery();
        conn.Close();
    }


    public BindingList<Produto> lerBanco()
    {
        string select = "SELECT * FROM produto";
        SQLiteConnection conn = new SQLiteConnection($"Data Source={enderecoCompleto};Version=3;");
        conn.Open();

        SQLiteCommand comando = new SQLiteCommand(select, conn);

        BindingList<Produto> produtos = new BindingList<Produto>();

        SQLiteDataReader leitor = comando.ExecuteReader();
        while (leitor.Read())
        {
            produtos.Add(new Produto(leitor.GetString(1), leitor.GetInt32(2), leitor.GetFloat(3), leitor.GetInt32(0)));
        }

        conn.Close();
        return produtos;
    }


    public void atualizarProduto(int id, string nome, int quantidade, float valor)
    {
        string update = $"UPDATE produto SET nome = '{nome}', quantidade = {quantidade}, preco = {valor.ToString().Replace(",",".")} WHERE id = {id}";
        SQLiteConnection conn = new SQLiteConnection($"Data Source={enderecoCompleto};Version=3;");
        conn.Open();

        SQLiteCommand comando = new SQLiteCommand(update, conn);

        comando.ExecuteNonQuery();
        conn.Close();
    }


    public void deleteProduto(int id)
    {
        string delete = $"DELETE FROM produto WHERE id = {id}";
        SQLiteConnection conn = new SQLiteConnection($"Data Source={enderecoCompleto};Version=3;");
        conn.Open();

        SQLiteCommand comando = new SQLiteCommand(delete, conn);

        comando.ExecuteNonQuery();
        conn.Close();
    }


    public Produto selectUnico(int id) 
    {
        string select = $"SELECT * FROM produto WHERE id = {id}";
        SQLiteConnection conn = new SQLiteConnection($"Data Source={enderecoCompleto};Version=3;");
        conn.Open();

        SQLiteCommand comando = new SQLiteCommand(select, conn);

        SQLiteDataReader leitor = comando.ExecuteReader();
        leitor.Read();
        Produto produto = new Produto(leitor.GetString(1), leitor.GetInt32(2), leitor.GetFloat(3), leitor.GetInt32(0));

        leitor.Close();
        conn.Close();

        return produto;
    }
}
