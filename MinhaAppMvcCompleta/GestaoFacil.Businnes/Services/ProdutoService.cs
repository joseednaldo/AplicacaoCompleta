using GestaoFacil.Businnes.Interfaces;
using GestaoFacil.Businnes.Models;
using GestaoFacil.Businnes.Models.Validations;
using System;
using System.Threading.Tasks;

namespace GestaoFacil.Businnes.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository,
            INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Adcionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
