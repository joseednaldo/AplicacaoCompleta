
using GestaoFacil.Businnes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GestaoFacil.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Produto>Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // => vai ser chamado no momento da criação do modelo no banco de dados.


            // => se eu esquecer de maperar alguma coisa como evitar o  nvarhcar max.  é uma garantia caso eu esqueça de mapear.
            /*
             foreach (var property in modelBuilder.Model.GetEntityTypes()
             .SelectMany(e => e.GetProperties()
             .Where(p => p.ClrType == typeof(string))))
                 property.Relational().ColumnType = "varchar(100)";
             */


            /*
                 Esses metodo pega todas as entidades que foram mapeadas no "context" e buscar todas classes herdam do "IEdentityTypeConfiguration".
                 REGISTRANDO CADA MAPPING CRIADO...(PRODUTOMAPPING,FORNECEDORMAPPING,ENDERECOMAPPING).
             */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            //Desativando o delete em cascata...
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
 
            base.OnModelCreating(modelBuilder);
        }
    }
}