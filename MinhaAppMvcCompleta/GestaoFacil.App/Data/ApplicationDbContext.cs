using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoFacil.App.ViewModels;

namespace GestaoFacil.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public DbSet<GestaoFacil.App.ViewModels.ProdutoViewModel> ProdutoViewModel { get; set; }
        //public DbSet<GestaoFacil.App.ViewModels.EnderecoViewModel> EnderecoViewModel { get; set; }
    }
}
