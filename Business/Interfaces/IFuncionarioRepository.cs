using CrudFuncionarios.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<bool> Atualizar(Funcionario funcionario);
        Task<List<Funcionario>> ObterTodos();
        Task<Funcionario> Obter(int id);
    }
}
