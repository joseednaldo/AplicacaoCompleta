using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFacil.Businnes.Models
{ 
    public class Produto : Entity
    {
        public Guid FornecedorID { get; set; }  //fk
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public string Imagem { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        /*
           EF  relation  => relacao um produto tem um forneceddor
           ou seja o entiry vai entender que o produto tem uma relação com o fornecedor 
           1:1    = um produto tem um fornecedor
           precisamos colocar a relação anbaixo para o entity framewrok entender que existe o relacionamento, sendo as propriedades de navegação.
         */

        public Fornecedor Fornecedor { get; set; }

    }
}
