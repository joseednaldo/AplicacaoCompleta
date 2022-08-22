using GestaoFacil.Businnes.Interfaces;
using GestaoFacil.Businnes.Models;
using GestaoFacil.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GestaoFacil.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context){ }
        public async  Task<Endereco> ObterEnderecoPorFornecedor(Guid forncedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                 .FirstOrDefaultAsync( f => f.FornecedorId == forncedorId);
        }
    }
}

