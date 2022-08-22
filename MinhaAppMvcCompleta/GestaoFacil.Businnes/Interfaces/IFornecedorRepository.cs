using GestaoFacil.Businnes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoFacil.Businnes.Interfaces
{

    /*
     conceitos de interfaces especializadas...
     */
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        //Métodos especializados ou seja além de usar os metodos herdados da classe pai ou ter métodos especificos.
        //da minha entidade.
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);          //Vou obter o fornecedor e o endereço em um unico objeto
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);   //Vou obter o fornecedor, a lista de produto e o endereço em uma unica chamada.
    }

}
