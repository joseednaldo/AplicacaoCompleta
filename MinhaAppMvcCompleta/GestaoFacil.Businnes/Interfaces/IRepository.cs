using GestaoFacil.Businnes.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestaoFacil.Businnes.Interfaces
{

    /*
      conceito de interface genérica
     */
    public interface IRepository<TEntity> : IDisposable where TEntity  : Entity
    {
        Task Adcionar(TEntity entidade);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entidade);
        Task Remover(Guid id);

        /*
         O método Busca:
         -vai receber uma expressão lambda.
         -que vai compara a minha entididade desde que ela retorne um bool que chamamos de predicado.
         - ou seja estamos passando uma expressao lambda para buscar qualquer "entidade" por parametro.
         -Sempre vai retorna uma coleção de entidade
         */
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicado);

        /*
         nao que venha ser utilizado mas sempre bom ter caso precise ser usado...S
         */
        Task<int> SaveChanges();
    }
}
