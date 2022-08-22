using GestaoFacil.Businnes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoFacil.Businnes.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId); //Recupera uma lista de produtos por fornecedor. ou seja quais os produtos do forncedor "X".
        Task<IEnumerable<Produto>> ObterProdutosFornecedores(); //Recupera uma lista de produtos e fornecedores daquele produto.
        Task<Produto> ObterProdutoFornecedor(Guid Id); //Recupera o produto e o fornecedor dele no caso apenas um pelo "id" do produto.

    }
}
