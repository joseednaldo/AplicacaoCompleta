using GestaoFacil.Businnes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoFacil.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            //Mapeamento
            builder.HasKey(p => p.Id);  //nesse caso nao precisava pois o entity ja entederia mas estamos apenas garantindo....

            builder.Property(p => p.Nome)
           .IsRequired()
           .HasColumnType("varchar(200)");

            builder.Property(p => p.Documento)
           .IsRequired()
           .HasColumnType("varchar(14)");

            //criando relacionamento 1 : 1  => fornecedor para endereço / o endereço tem um fornecedor.
            // Ou seja o fornecedor tem apenas um endereço
            builder.HasOne(f => f.Endereco)   // significa que o fornecendor tem um endereco
                .WithOne(e => e.Fornecedor);  // significa que o endereço  tem um fornecedor.


            //1:N ou seja o fornecedor tem varios produtos. 
            builder.HasMany(f => f.Produtos)    // o fornecedor possui muitos produtos.
             .WithOne(p => p.Fornecedor)        // significa que um produto possui 1 fornecedor apenas.
             .HasForeignKey(p => p.FornecedorID);


            builder.ToTable("Fornecedor");
        }
    }


}






