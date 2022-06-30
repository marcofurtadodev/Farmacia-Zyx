﻿using DataAccessLayer;
using Entities;
using Entities.Propriedades;
using Shared;

namespace BusinessLogicalLayer.RegraDePreco
{
    internal  class RegraControleEstoque
    {
        ProdutoDal produtoDal = new ProdutoDal();
        internal Response EstoqueProduto(Entrada Entradas)
        {
            foreach (Item item in Entradas.Items)
            {
                Produto produto = produtoDal.GetByIDEstoque(item.IDProduto).Item;
                double Quantiavelha = produto.Quantia_Estoque;
                double QuantiaNova = item.Qtd + Quantiavelha;
                produtoDal.SetEstoque(QuantiaNova, item.IDProduto);
            }

            return new Response("", false);
        }


    }
}