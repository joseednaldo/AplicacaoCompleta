using AutoMapper;
using GestaoFacil.App.ViewModels;
using GestaoFacil.Businnes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFacil.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {

        /*
            -Estou usando o .ReverseMap() para fazer o inverso desde que seja igual.
            sem construtor parametrizado etc..  
            caso seja diferente o mapemento inverso deve ser criado em outra classe.
            FornecedorViewModel, Fornecedor
         */
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap(); ;
            CreateMap<Produto, ProdutoViewModel>().ReverseMap(); ;
        }
    }
}
