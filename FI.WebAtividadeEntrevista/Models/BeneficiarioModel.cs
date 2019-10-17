using FI.AtividadeEntrevista.DML;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebAtividadeEntrevista.Utils.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiario
    /// </summary>
    public class BeneficiarioModel
    {
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Cpf
        /// </summary>
        [Required]
        [RegularExpression(@"^([0-9]{3})\.([0-9]{3})\.([0-9]{3})\-([0-9]{2})$", ErrorMessage = "Formato do cpf inválido")]
        [CpfValidation(ErrorMessage = "Digite um cpf válido no beneficiário!")]
        public string Cpf { get; set; }

        public Beneficiario Converter()
        {
            return new Beneficiario()
            {
                Nome = this.Nome,
                CPF = String.Join("", Regex.Split(this.Cpf, @"[^\d]"))
            };
        }
    }
}