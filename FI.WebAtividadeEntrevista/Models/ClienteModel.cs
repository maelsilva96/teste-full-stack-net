using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebAtividadeEntrevista.Utils.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Cliente
    /// </summary>
    public class ClienteModel
    {
        public long Id { get; set; }
        
        /// <summary>
        /// CEP
        /// </summary>
        [Required]
        public string CEP { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [Required]
        public string Cidade { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string Estado { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        [Required]
        public string Logradouro { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        [Required]
        public string Nacionalidade { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        [Required]
        public string Sobrenome { get; set; }

        /// <summary>
        /// Cpf
        /// </summary>
        [Required]
        [RegularExpression(@"^([0-9]{3})\.([0-9]{3})\.([0-9]{3})\-([0-9]{2})$", ErrorMessage = "Formato do cpf inválido")]
        [CpfValidation(ErrorMessage = "Digite um cpf válido")]
        public string Cpf { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// Beneficiarios
        /// </summary>
        public List<BeneficiarioModel> Beneficiarios { get; set; }

        public Cliente Converter()
        {
            if (this.Beneficiarios == null)
            {
                this.Beneficiarios = new List<BeneficiarioModel>();
            }
            return new Cliente()
            {
                Id = this.Id,
                CEP = this.CEP,
                Cidade = this.Cidade,
                Email = this.Email,
                Estado = this.Estado,
                Logradouro = this.Logradouro,
                Nacionalidade = this.Nacionalidade,
                Nome = this.Nome,
                Sobrenome = this.Sobrenome,
                CPF = String.Join("", Regex.Split(this.Cpf, @"[^\d]")),
                Telefone = this.Telefone,
                Beneficiarios = this.Beneficiarios.ConvertAll((item) =>
                {
                    return item.Converter();
                })
            };
        }

    }    
}