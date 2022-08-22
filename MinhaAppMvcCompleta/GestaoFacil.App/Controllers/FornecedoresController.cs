using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestaoFacil.App.ViewModels;
using GestaoFacil.Businnes.Interfaces;
using AutoMapper;
using GestaoFacil.Businnes.Models;
using Microsoft.AspNetCore.Authorization;
using GestaoFacil.App.Exrensions;

namespace GestaoFacil.App.Controllers
{
    //preciso pra todos
    //[Route("admin/fornecedores")]

    [Authorize]
    public partial class FornecedoresController : BaseControler
    {

        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        //private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository, 
                                     IMapper mapper, 
                                     IFornecedorService fornecedorService,
                                     INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
            //_enderecoRepository = enderecoRepository;
        }

        [AllowAnonymous]
        [Route("lista-de-fornecedores")]  //rotas exclusivas
        public async Task<IActionResult> Index()
        {
            //convertendo a lista de fornecedores em uma lista   do tipo "FornecedorViewModel".
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
        }

        [AllowAnonymous]
        [Route("dados-do-fornecedor/{id:guid}")]  //rotas exclusivas
        public async Task<IActionResult> Details(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("novo-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            //await _fornecedorRepository.Adcionar(fornecedor);
            await _fornecedorService.Adicionar(fornecedor);

            if (!OperacaoValida()) return View(fornecedorViewModel);

            return RedirectToAction("Index");

        }

        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }
            return View(fornecedorViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();

           
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Atualizar(fornecedor);

            /*
              return RedirectToAction(nameof(Index));  = return RedirectToAction("Index");
            */
            if (!OperacaoValida()) return View(await ObterFornecedorProdutosEndereco(id));
            return RedirectToAction("Index");

        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            await _fornecedorService.Remover(id);

            if (!OperacaoValida()) return View(fornecedor);
            return RedirectToAction("Index");

        }

        [AllowAnonymous]
        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor == null) return NotFound();

            return PartialView("_DetalhesEndereco", fornecedor);

        }


        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });


        }

        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel FornecedorViewModel)
        {

            //Removendo dois itens da model state pois nao precisa nesse caso  pois esotu atualizano apenas o endereco.
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", FornecedorViewModel);

            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(FornecedorViewModel.Endereco));

            if (!OperacaoValida()) return PartialView("_AtualizarEndereco", FornecedorViewModel);

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = FornecedorViewModel.Endereco.FornecedorId });
            return Json(new { success = true,url});


        }

        private async Task<FornecedorViewModel>ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}
