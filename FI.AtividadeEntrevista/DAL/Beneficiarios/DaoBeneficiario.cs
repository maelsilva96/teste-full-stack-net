using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiarios
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui ou Alterar um beneficiario
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        /// <param name="benefi">Objeto de beneficiario</param>
        internal long IncluirOuAlterar(long id_cliente, DML.Beneficiario benefi)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", benefi.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", benefi.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", benefi.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID_CLIENTE", id_cliente));

            DataSet ds = base.Consultar("FI_SP_IncOrAltBenef", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Pequisa beneficiario por CPF
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        /// <param name="cpf">CPF do beneficiario</param>
        internal DML.Beneficiario PesquisarPorCPF (long id_cliente, string cpf)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id_Cliente", id_cliente));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", cpf));

            DataSet ds = base.Consultar("FI_SP_PesqBenefPorCpf", parametros);
            return this.Converter(id_cliente, ds).FirstOrDefault();
        }

        internal bool VerificarExistencia(long id_cliente, string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID_CLIENTE", id_cliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBenef", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Lista todos os beneficiarios
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        internal List<DML.Beneficiario> Listar(long id_cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id_Cliente", id_cliente));

            DataSet ds = base.Consultar("FI_SP_ConsBenef", parametros);
            List<DML.Beneficiario> ben = Converter(id_cliente, ds);

            return ben;
        }

        /// <summary>
        /// Excluir os beneficiarios que não são passados
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        /// <param name="ids">Lista de cpf dos beneficiarios</param>
        internal void ExcluirOsQueNaoSaoDaLista(long id_cliente, List<string> ids)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            string list = string.Join(",", ids);
            parametros.Add(new System.Data.SqlClient.SqlParameter("@ID_CLIENTE", id_cliente));
            parametros.Add(new System.Data.SqlClient.SqlParameter("@CPFS_QUE_NAO_SERAO_EXCLUIDOS", list));

            base.Executar("FI_SP_DelBenef", parametros);
        }

        private List<DML.Beneficiario> Converter (long id_cliente, DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario ben = new DML.Beneficiario();
                    ben.Id = row.Field<long>("Id");
                    ben.CPF = row.Field<string>("CPF");
                    ben.Nome = row.Field<string>("NOME");
                    lista.Add(ben);
                }
            }
            return lista;
        }
    }
}
