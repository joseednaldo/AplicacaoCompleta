using GestaoFacil.Businnes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFacil.App.Controllers
{
    public abstract class BaseControler : Controller
    {
        private readonly INotificador _nofificador;

        protected BaseControler(INotificador nofificador)
        {
            _nofificador = nofificador;
        }

        protected bool OperacaoValida()
        {
            return !_nofificador.TemNotificacao();
        }
    }

}
