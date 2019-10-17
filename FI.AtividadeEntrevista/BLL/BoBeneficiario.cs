
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui ou alterar um novo beneficiario
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long IncluirOuAlterar (long id_cliente, DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            if (beneficiario.Id > 0 && this.VerificarExistencia(id_cliente, beneficiario.CPF))
            {
                DML.Beneficiario ben = this.PesquisarPorCPF(id_cliente, beneficiario.CPF);
                if (ben.Id > 0) beneficiario.Id = ben.Id;
            }
            return benef.IncluirOuAlterar(id_cliente, beneficiario);
        }

        /// <summary>
        /// Pesquisar beneficiario por CPF
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        /// <param name="cpf">CPF do beneficiario</param>
        /// <returns></returns>
        public DML.Beneficiario PesquisarPorCPF (long id_cliente, string cpf)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.PesquisarPorCPF(id_cliente, cpf);
        }

        /// <summary>
        /// Excluir os beneficiarios que não da lista
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        /// <param name="beneficiarios">Lista de beneficiarios</param>
        /// <returns></returns>
        public void ExcluirOsQueNaoSaoDaLista(long id_cliente, List<DML.Beneficiario> beneficiarios)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            List<string> lista = this.Converter(beneficiarios);
            benef.ExcluirOsQueNaoSaoDaLista(id_cliente, lista);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        public List<DML.Beneficiario> Listar(long id_cliente)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Listar(id_cliente);
        }

        /// <summary>
        /// Verifica a Existencia
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(long id_cliente, string CPF)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.VerificarExistencia(id_cliente, CPF);
        }

        private List<string> Converter (List<DML.Beneficiario> beneficiarios)
        {
            List<string> lista = new List<string>();
            beneficiarios.ForEach((item) =>
            {
                if (item.Id > 0)
                {
                    lista.Add(item.CPF);
                }
            });
            return lista;
        }
    }
}
