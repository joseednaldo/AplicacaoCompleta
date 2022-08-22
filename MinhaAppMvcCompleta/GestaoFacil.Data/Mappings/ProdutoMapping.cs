using  GestaoFacil.Businnes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoFacil.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            //Mapeamento
            builder.HasKey(p=> p.Id);  //nesse caso nao precisava pois o entity ja entederia mas estamos apenas garantindo....
            
            builder.Property(p => p.Nome)
           .IsRequired()
           .HasColumnType("varchar(200)");
            
            builder.Property(p => p.Descricao)
           .IsRequired()
           .HasColumnType("varchar(1000)");


            builder.ToTable("Produtos");
        }
    }


}






