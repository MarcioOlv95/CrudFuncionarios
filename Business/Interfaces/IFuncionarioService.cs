using CrudFuncionarios.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IFuncionarioService
    {
        Task<bool> Atualizar(Funcionario funcionario);
        Task<List<Funcionario>> ObterTodos();
        Task<Funcionario> Obter(int id);
    }
}
