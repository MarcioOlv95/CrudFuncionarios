using Business.Interfaces;
using Business.Models.Validations;
using CrudFuncionarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public Task<bool> Atualizar(Funcionario funcionario)
        {
            var validator = new FuncionarioValidation();
            var result = validator.Validate(funcionario);
            
            if (!result.IsValid)
            {
                return Task.FromResult(false);
            }

            if (funcionario.HabilidadeL != null)
            {
                funcionario.Habilidade = string.Join(",", funcionario.HabilidadeL);
            }

            int idade = DateTime.Now.Year - funcionario.DataNascimento.Year;
            if (DateTime.Now.DayOfYear < funcionario.DataNascimento.DayOfYear)
                idade = idade - 1;

            funcionario.Idade = idade;

            return _funcionarioRepository.Atualizar(funcionario);
        }

        public Task<Funcionario> Obter(int id)
        {
            return _funcionarioRepository.Obter(id);
        }

        public Task<List<Funcionario>> ObterTodos()
        {
            return _funcionarioRepository.ObterTodos();
        }
    }
}
