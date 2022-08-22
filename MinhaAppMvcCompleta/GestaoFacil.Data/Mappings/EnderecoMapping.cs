using GestaoFacil.Businnes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoFacil.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            //Mapeamento
            builder.HasKey(p => p.Id);  //nesse caso nao precisava pois o entity ja entederia mas estamos apenas garantindo....


            builder.Property(p => p.Logradouro)
            .IsRequired()
            .HasColumnType("varchar(200)");

            builder.Property(p => p.Numero)
            .IsRequired()
            .HasColumnType("varchar(50)");

            builder.Property(p => p.Complemento)
            .IsRequired()
            .HasColumnType("varchar(200)");

            builder.Property(p => p.Bairro)
            .IsRequired()
            .HasColumnType("varchar(100)");

            builder.Property(p => p.Cidade)
           .IsRequired()
           .HasColumnType("varchar(100)");

            builder.Property(p => p.Estado)
            .IsRequired()
            .HasColumnType("varchar(50)");


            builder.ToTable("Enderecos");
        }
    }


}






