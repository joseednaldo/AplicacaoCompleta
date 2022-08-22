using GestaoFacil.Businnes.Notificacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoFacil.Businnes.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handler(Notificacao notificacao);
    }
}
