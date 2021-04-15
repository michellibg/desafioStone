using System;
using System.ComponentModel.DataAnnotations;

namespace desafioStone.Models
{
    public class FuncionarioViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [MaxLength(11, ErrorMessage = "Número inválido.")]
        [MinLength(11, ErrorMessage = "Número incompleto.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public string Setor { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O salário deve ser maior que zero.")]
        public decimal SalarioBruto { get; set; }

        public DateTime DataAdmissao { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public bool PossuiPlanoSaude { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public bool PossuiPlanoDental { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        public bool PossuiValeTransporte { get; set; }
    }
}
        