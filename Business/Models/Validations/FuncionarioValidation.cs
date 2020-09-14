using CrudFuncionarios.Models;
using FluentValidation;
using System.Data;

namespace Business.Models.Validations
{
    public class FuncionarioValidation : AbstractValidator<Funcionario>
    {
        public FuncionarioValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo nome deve precisa ser fornecido");

            RuleFor(x => x.Sobrenome)
                .NotEmpty().WithMessage("O campo sobrenome deve precisa ser fornecido");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("O campo data de nascimento precisa ser fornecido");

            RuleFor(x => x.Sexo)
                .NotEmpty().WithMessage("O campo sexo deve precisa ser fornecido");

            RuleFor(x => x.HabilidadeL)
                .NotEmpty().WithMessage("O campo habilidade precisa ser fornecido");
        }
    }
}
