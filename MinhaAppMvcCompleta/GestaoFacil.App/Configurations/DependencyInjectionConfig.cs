using GestaoFacil.App.Exrensions;
using GestaoFacil.Businnes.Interfaces;
using GestaoFacil.Businnes.Notificacoes;
using GestaoFacil.Businnes.Services;
using GestaoFacil.Data.Context;
using GestaoFacil.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoFacil.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDepencencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
           
            //regras de negocios e notificações
            services.AddScoped<INotificador,Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}
