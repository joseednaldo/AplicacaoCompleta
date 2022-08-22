using FluentValidation;
using FluentValidation.Results;
using GestaoFacil.Businnes.Interfaces;
using GestaoFacil.Businnes.Models;
using GestaoFacil.Businnes.Notificacoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoFacil.Businnes.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            //Propagar esse erro até a camada de apresentação (front-end).
            _notificador.Handler(new Notificacao(mensagem));
        }

        /*
           Como se cria um metodo "Generico"
         */
        protected bool ExecutarValidacao<TV, TE>(TV validacao,TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if(validator.IsValid) return true;

            Notificar(validator);
            return false;
        }
    
    }
}
