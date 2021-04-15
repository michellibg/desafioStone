using System.Collections.Generic;

namespace desafioStone.Controllers
{
    public class ContrachequeViewModel
    {
        public string MesReferencia { get; set; }
        public int IdFuncionario { get; set; }
        public string NomeFuncionario { get; set; }
        public List<LancamentosViewModel> Lancamentos { get; set; }
        public decimal SalarioBruto { get; set; }
        public decimal TotalDesconto { get; set; }
        public decimal SalarioLiquido { get; set; }

    }
}