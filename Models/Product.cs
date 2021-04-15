using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafioStone.Models
{
    public class Product
    {
        [key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
