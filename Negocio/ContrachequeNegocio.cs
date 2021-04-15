using desafioStone.Controllers;
using desafioStone.Data;
using desafioStone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static desafioStone.Negocio.Enumeradores;

namespace desafioStone.Negocio
{
    public class ContrachequeNegocio
    {
        private static void AdicionarLancamento(List<LancamentosViewModel> list, decimal Valor, string Descricao, TipoLancamento Tipo)
        {
            LancamentosViewModel lancamento = new LancamentosViewModel();
            lancamento.Tipo = Tipo;
            lancamento.Valor = Valor;
            lancamento.Descricao = Descricao;
            list.Add(lancamento);
        }

        public static decimal RetornarAliquotaINSS(decimal ValorSalario)
        {
            if (RetonarValorMenorQue(ValorSalario, 1045m))
            {
                return 7.5m;
            }

            if (RetonarValorEstaEntre(ValorSalario, 1045.01m, 2089.60m))
            {
                return 9m;
            }

            if (RetonarValorEstaEntre(ValorSalario, 2089.61m, 3134.40m))
            {
                return 12m;
            }

            if (RetonarValorEstaEntre(ValorSalario, 3134.41m, 6101.06m))
            {
                return 14m;
            }
            return 0;
        }

        public static bool RetonarValorEstaEntre(decimal ValorTestado, decimal Valor1, decimal Valor2)
        {
            var result1 = decimal.Compare(ValorTestado, Valor1);
            var result2 = decimal.Compare(ValorTestado, Valor2);

            if ((result1 > 0) || (result2 < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RetonarValorMenorQue(decimal ValorTestado, decimal Valor)
        {
            if (decimal.Compare(ValorTestado, Valor) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static decimal RetornarImpostoRendaRetido(decimal ValorSalario)
        {
            return 0;
        }

        public static decimal RetornarAliquotaIR(decimal ValorSalario, out decimal TetoAliquota)
        {
            if (RetonarValorMenorQue(ValorSalario, 1903.98m))
            {
                TetoAliquota = 0m;
                return 0m;
            }

            if (RetonarValorEstaEntre(ValorSalario, 1903.99m, 2826.65m))
            {
                TetoAliquota = 142.80m;
                return 7.5m;
            }

            if (RetonarValorEstaEntre(ValorSalario, 2826.66m, 3751.05m))
            {
                TetoAliquota = 354.80m;
                return 15m;
            }

            if (RetonarValorEstaEntre(ValorSalario, 3751.06m, 4664.68m))
            {
                TetoAliquota = 636.13m;
                return 22.5m;
            }

            if (RetonarValorMaiorQue(ValorSalario, 4664.68m))
            {
                TetoAliquota = 869.36m;
                return 22.5m;
            }

            TetoAliquota = 0m;
            return 0;
        }

        public static bool RetonarValorMaiorQue(decimal ValorTestado, decimal Valor)
        {
            if (decimal.Compare(ValorTestado, Valor) < 0)
            {
                return true;
            }
            return false;
        }

        public static decimal RetornarDescontoINSS(decimal ValorSalario, List<LancamentosViewModel> list)
        {
            var descricao = "Desconto INSS";
            var valor = (ValorSalario * RetornarAliquotaINSS(ValorSalario)) / 100;
            AdicionarLancamento(list, valor, descricao, Enumeradores.TipoLancamento.Desconto);
            return valor;
        }

        public static decimal RetornarDescontoIR(decimal ValorSalario, List<LancamentosViewModel> list)
        {
            decimal valor;
            var descricao = "Desconto do Imposto de Renda";
            decimal TetoAliquota;
            var aliquota = RetornarAliquotaIR(ValorSalario, out TetoAliquota);
            var resultadoCalculo = (ValorSalario * aliquota) / 100;            

            if (RetonarValorMenorQue(resultadoCalculo, TetoAliquota))
            {
                valor = resultadoCalculo;
            }
            valor = TetoAliquota;
           
            AdicionarLancamento(list, valor, descricao, Enumeradores.TipoLancamento.Desconto);
            return valor;
        }

        public static decimal RetornarDescontoPlanoSaude(bool PossuiPlanoSaude, List<LancamentosViewModel> list)
        {
            decimal valor = 0;
            var descricao = "Desconto do Plano de Saúde";

            if (PossuiPlanoSaude)
            {
                valor = 10m;
            }
            AdicionarLancamento(list, valor, descricao, Enumeradores.TipoLancamento.Desconto);
            return valor;
        }

        public static decimal RetornarDescontoPlanoDental(bool PossuiPlanoDental, List<LancamentosViewModel> list)
        {
            decimal valor = 0;
            var descricao = "Desconto do Plano Dendal";
            if (PossuiPlanoDental)
            {
                valor = 5m;
            }
            AdicionarLancamento(list, valor, descricao, Enumeradores.TipoLancamento.Desconto);
            return valor;
        }

        public static decimal RetornarDescontoValeTransporte(decimal SalarioBruto, bool PossuiValeTransporte, List<LancamentosViewModel> list)
        {
            decimal valor = 0;
            var descricao = "Desconto do Vale Transporte";

            if (PossuiValeTransporte)
            {
                if (RetonarValorMenorQue(SalarioBruto, 1500m))
                {
                    valor = 0;
                }
                else
                {
                    valor = (SalarioBruto * 6) / 100;
                }
            }
            
            AdicionarLancamento(list, valor, descricao, Enumeradores.TipoLancamento.Desconto);
            return valor;
        }

        public static decimal RetornarDescontoFGTS(decimal SalarioBruto, List<LancamentosViewModel> list)
        {
            var descricao = "Desconto FGTS";
            var valor = (SalarioBruto * 8) / 100;
            AdicionarLancamento(list, valor, descricao, Enumeradores.TipoLancamento.Desconto);
            return valor;
        }



        public ContrachequeViewModel GerarContracheque([FromServices] DesafioStoneEntities context, int IdFuncionario, string MesReferencia)
        {
            var model = new ContrachequeViewModel();
            var funcionario = context.Funcionarios.Find(IdFuncionario);
            List<LancamentosViewModel> list = new List<LancamentosViewModel>();

            model.IdFuncionario = IdFuncionario;
            model.MesReferencia = MesReferencia;
            model.NomeFuncionario = funcionario.Nome;
            model.SalarioBruto = funcionario.SalarioBruto;
            model.TotalDesconto = RetornarTotalDescontos(funcionario, list);
            model.SalarioLiquido = funcionario.SalarioBruto + model.TotalDesconto;
            model.Lancamentos = list;
            return model;
        }

        private static decimal RetornarTotalDescontos(FuncionarioViewModel funcionario, List<LancamentosViewModel> list)
        {
            var DescontoINSS = RetornarDescontoINSS(funcionario.SalarioBruto, list);
            var DescontoIR = RetornarDescontoIR(funcionario.SalarioBruto, list);
            var DescontoValePlanoSaude = RetornarDescontoPlanoSaude(funcionario.PossuiPlanoSaude, list);
            var DescontoValePlanoDental = RetornarDescontoPlanoDental(funcionario.PossuiPlanoDental, list);
            var DescontoValeTransporte = RetornarDescontoValeTransporte(funcionario.SalarioBruto, funcionario.PossuiValeTransporte, list);
            var DescontoFGTS = RetornarDescontoFGTS(funcionario.SalarioBruto, list);

            return -(DescontoINSS + DescontoIR + DescontoValePlanoSaude + DescontoValePlanoDental 
                + DescontoValeTransporte + DescontoFGTS); 
        }

    }
}
