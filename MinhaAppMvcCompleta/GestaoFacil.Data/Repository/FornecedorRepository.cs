using GestaoFacil.Businnes.Interfaces;
using GestaoFacil.Businnes.Models;
using GestaoFacil.Data.Context;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestaoFacil.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>,IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext context) : base(context)
        {

        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking().Include(c => c.Endereco).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            // include de produto e include de endereco (include = inner join)
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Produtos)
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c=> c.Id == id);
        }
    }
}
