using Business.Interfaces;
using CrudFuncionarios.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class FuncionarioRepository : Repository, IFuncionarioRepository
    {
        public FuncionarioRepository(IConfiguration configuration) : base(configuration) { }

        public Task<bool> Atualizar(Funcionario funcionario)
        {
            try
            {
                using (var conexao = connection)
                {
                    connection.Execute(@"PRC_UPDATE_FUNCIONARIO 
                                        @Id,
                                        @Nome,
                                        @Sobrenome,
                                        @DataNascimento,
                                        @Email,
                                        @Sexo,
                                        @Idade,
                                        @Habilidade,
                                        @Ativo", funcionario);
                }
                
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }

        public Task<List<Funcionario>> ObterTodos()
        {
            try
            {
                var funcionarioL = new List<Funcionario>();
                var retorno = new List<Funcionario>();
                using (var conexao = connection)
                {
                    retorno = connection.Query<Funcionario>(@"PRC_GET_FUNCIONARIO @IdFuncionario", new { IdFuncionario = 0 }).ToList();
                }

                AgruparHabilidades(funcionarioL, retorno);

                return Task.FromResult(funcionarioL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Funcionario> Obter(int id)
        {
            try
            {
                var funcionarioL = new List<Funcionario>();
                var retorno = new List<Funcionario>();
                using (var conexao = connection)
                {
                    retorno = connection.Query<Funcionario>(@"PRC_GET_FUNCIONARIO @IdFuncionario", new { IdFuncionario = id }).ToList();
                }

                AgruparHabilidades(funcionarioL, retorno);

                return Task.FromResult(funcionarioL.FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AgruparHabilidades(List<Funcionario> funcionarioL, List<Funcionario> retorno)
        {
            var groupL = retorno.GroupBy(x => x.Id).ToList();

            foreach (var group in groupL)
            {
                var item = group.FirstOrDefault(x => x.Id == group.Key);
                if (group.Any(x => x.Habilidade != null))
                {
                    List<string> habilidadeL = new List<string>();

                    group.ToList().ForEach(x =>
                    {
                        habilidadeL.Add(x.Habilidade);
                    });

                    item.Habilidade = string.Join(",", habilidadeL);
                    item.HabilidadeL = habilidadeL;
                }

                funcionarioL.Add(item);
            }
        }
    }
}
