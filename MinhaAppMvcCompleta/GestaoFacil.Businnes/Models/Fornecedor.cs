using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestaoFacil.Businnes.Models
{
    public class Fornecedor : Entity
    {

        public string Nome { get; set; }

        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public Endereco Endereco { get; set; }
        public bool Ativo { get; set; }


        /*EF RELATIONs
         * fica facil para o entiry entender a relalçao de um 1:N ou seja 1 fornecedore pode ter varios produtos.
         * ou seja o fornecedor tem ralação como produto.
         */
        public IEnumerable<Produto> Produtos { get; set; }
    }

}
