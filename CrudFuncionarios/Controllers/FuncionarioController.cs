using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Business.Interfaces;
using CrudFuncionarios.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FuncionarioController : ControllerBase
    {
        public readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet("{id}")]
        public Task<Funcionario> ObterFuncionario(int id)
        {
            return _funcionarioService.Obter(id);
        }

        [HttpGet]
        public Task<List<Funcionario>> ObterTodos()
        {
            return _funcionarioService.ObterTodos();
        }

        [HttpPost]
        public Task<HttpResponseMessage> AtualizarFuncionario(Funcionario funcionario)
        {
            var retorno = _funcionarioService.Atualizar(funcionario);
            HttpRequestMessage request = new HttpRequestMessage();
            var response = request.CreateResponse(retorno.Result ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            return Task.FromResult(response);
        }
    }
}