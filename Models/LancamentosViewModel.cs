using static desafioStone.Negocio.Enumeradores;

namespace desafioStone.Controllers
{
    public class LancamentosViewModel
    {
        public TipoLancamento Tipo { get; set; }
        public decimal Valor{ get; set; }
        public string Descricao { get; set; }

    }
}