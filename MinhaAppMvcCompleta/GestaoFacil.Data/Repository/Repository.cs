using GestaoFacil.Businnes.Interfaces;
using GestaoFacil.Businnes.Models;
using GestaoFacil.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestaoFacil.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext Db;      //Porque o uso do "protected" tanto o repository tanto quem herdade de repository vai ter acesso ao MeuDbContext
        protected readonly DbSet<TEntity> DbSet;//atalho para o dbset.

        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicado)
        {
            //O await espera o resultado acontecer ou seja reotrna do banco.(DbSet.AsNoTracking().Where(predicado).ToListAsync();)
            //await converte a task em um resultado que espero.
            return await DbSet.AsNoTracking().Where(predicado).ToListAsync();
  
        }
        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adcionar(TEntity entidade)
        {
            //db.Set<TEntity>().Add(entidade); //verboso..
            DbSet.Add(entidade);
            await SaveChanges();

        }

        public virtual async Task Atualizar(TEntity entidade)
        {
            DbSet.Update(entidade);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {

            DbSet.Remove(new TEntity { Id = id});
            await SaveChanges();
        }

        public Task<int> SaveChanges()
        {
            return Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

    }
}
