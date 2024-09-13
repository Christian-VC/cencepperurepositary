using Capa3_Dominio.ModuloPrincipal.Entidad;
using Capa4_Persistencia.SqlServer.ModuloBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa4_Persistencia.SqlServer.ModuloPrincipal
{
    public class ModuloSQL
    {
        private ConectionSqlServer conectionSqlServer;

        public ModuloSQL(ConectionSqlServer conectionSqlServer)
        {
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<Modulo> Listar()
        {
            String ListarModuloSP = "Silabo.ListarModuloSP";
            List<Modulo> listarModulo = new List<Modulo>();
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ListarModuloSP);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    listarModulo.Add(ObtenerDatosModulo(reader));
                }
                reader.Close();
                for (int i = 0; i < listarModulo.Count; i++)
                {
                    Modulo moduloTemp = listarModulo.ElementAt(i);
                    ProgramaSQL programaSQL = new ProgramaSQL(conectionSqlServer);
                    moduloTemp.ProgramaModulo = programaSQL.Buscar(moduloTemp.ProgramaModulo.CodigoPrograma);
                    listarModulo[i] = moduloTemp;
                }
                return listarModulo;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método listar. " + error.Message, error);
            }
        }

        public Modulo Buscar(String codigoModulo)
        {
            Modulo modulo = new Modulo();
            String BuscarModuloCodigoSP = "Silabo.BuscarModuloCodigoSP";

            try
            {
                SqlCommand comando;
                comando = conectionSqlServer.GetProcedureCommand(BuscarModuloCodigoSP);
                comando.Parameters.AddWithValue("@CodigoModulo", codigoModulo);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    modulo.CodigoModulo = codigoModulo;
                    String codigoProgramaModulo = reader.GetString(0);
                    modulo.NombreModulo = reader.GetString(1);
                    modulo.EstadoModulo = reader.GetString(2)[0];
                    reader.Close();

                    ProgramaSQL programaSQL = new ProgramaSQL(conectionSqlServer);
                    modulo.ProgramaModulo = programaSQL.Buscar(codigoProgramaModulo);
                }
                return modulo;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar(Modulo modulo)
        {
            String RegistrarModuloSP = "Silabo.RegistrarModuloSP";
            try
            {
                SqlCommand comando;
                comando = conectionSqlServer.GetProcedureCommand(RegistrarModuloSP);
                comando.Parameters.AddWithValue("@CodigoProgramaModulo", modulo.ProgramaModulo.CodigoPrograma);
                comando.Parameters.AddWithValue("@NombreModulo", modulo.NombreModulo);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }
        }

        public void Actualizar(Modulo modulo)
        {
            String ActualizarModuloSP = "Silabo.ActualizarModuloSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarModuloSP);
                comando.Parameters.AddWithValue("@CodigoModulo", modulo.CodigoModulo);
                comando.Parameters.AddWithValue("@CodigoProgramaModulo", modulo.ProgramaModulo.CodigoPrograma);
                comando.Parameters.AddWithValue("@NombreModulo", modulo.NombreModulo);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }

        public void CambioEstado(Modulo Modulo)
        {
            String CambioEstadoSP = "Silabo.CambioEstadoModuloSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(CambioEstadoSP);
                comando.Parameters.AddWithValue("@CodigoModulo", Modulo.CodigoModulo);
                comando.Parameters.AddWithValue("@EstadoModulo", Modulo.EstadoModulo);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método cambio de estado. " + error.Message, error);
            }
        }

        private Modulo ObtenerDatosModulo(SqlDataReader reader)
        {
            Modulo Modulo = new Modulo();
            Modulo.CodigoModulo = reader.GetString(0);
            Modulo.ProgramaModulo.CodigoPrograma = reader.GetString(1);
            Modulo.NombreModulo = reader.GetString(2);
            Modulo.EstadoModulo = reader.GetString(3)[0];
            return Modulo;
        }
    }
}
