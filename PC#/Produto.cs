using System;

public class Produto
{
    public string Nome { get; set; }
    public int Quantidade { get; set; }
    public float Preco { get; set; }
    public int id;
    public Produto(string nome, int quantidade, float valor, int id)
    {
        this.Nome = nome;
        this.Quantidade = quantidade;
        this.Preco = valor;
        this.id = id;
    }
}
