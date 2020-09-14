using System;
using System.Collections.Generic;

namespace CrudFuncionarios.Models
{
    public class Funcionario : BaseModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public char Sexo { get; set; }
        public int Idade { get; set; }
        public List<string> HabilidadeL { get; set; }
        public string Habilidade { get; set; }
    }
}
