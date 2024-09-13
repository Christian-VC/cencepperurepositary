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
    public class ProgramaSQL
    {
        private ConectionSqlServer conectionSqlServer;

        public ProgramaSQL(ConectionSqlServer conectionSqlServer)
        {
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<Programa> Listar()
        {
            String ListarProgramaSP = "Silabo.ListarProgramaSP";
            List<Programa> listarPrograma = new List<Programa>();
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ListarProgramaSP);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    listarPrograma.Add(ObtenerDatosPrograma(reader));
                }
                reader.Close();
                for (int i = 0; i < listarPrograma.Count; i++)
                {
                    Programa programaTemp = listarPrograma.ElementAt(i);
                    GrupoSQL grupoSQL = new GrupoSQL(conectionSqlServer);
                    programaTemp.GrupoPrograma = grupoSQL.Buscar(programaTemp.GrupoPrograma.CodigoGrupo);
                    listarPrograma[i] = programaTemp;
                }
                return listarPrograma;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método listar. " + error.Message, error);
            }
        }

        public Programa Buscar(String codigoPrograma)
        {
            Programa programa = new Programa();
            String BuscarProgramaCodigoSP = "Silabo.BuscarProgramaCodigoSP";

            try
            {
                SqlCommand comando;
                comando = conectionSqlServer.GetProcedureCommand(BuscarProgramaCodigoSP);
                comando.Parameters.AddWithValue("@CodigoPrograma", codigoPrograma);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    programa.CodigoPrograma = codigoPrograma;
                    String codigoGrupoPrograma = reader.GetString(0);
                    programa.NombrePrograma = reader.GetString(1);
                    programa.EstadoPrograma = reader.GetString(2)[0];
                    reader.Close();

                    // Asignarle el grupo al programa 
                    GrupoSQL grupoSQL = new GrupoSQL(conectionSqlServer);
                    programa.GrupoPrograma = grupoSQL.Buscar(codigoGrupoPrograma);
                }
                return programa;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar(Programa programa)
        {
            String RegistrarProgramaSP = "Silabo.RegistrarProgramaSP";
            try
            {
                SqlCommand comando;
                comando = conectionSqlServer.GetProcedureCommand(RegistrarProgramaSP);
                comando.Parameters.AddWithValue("@CodigoGrupoPrograma", programa.GrupoPrograma.CodigoGrupo);
                comando.Parameters.AddWithValue("@NombrePrograma", programa.NombrePrograma);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }
        }

        public void Actualizar(Programa programa)
        {
            String ActualizarProgramaSP = "Silabo.ActualizarProgramaSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarProgramaSP);
                comando.Parameters.AddWithValue("@CodigoPrograma", programa.CodigoPrograma);
                comando.Parameters.AddWithValue("@CodigoGrupoPrograma", programa.GrupoPrograma.CodigoGrupo);
                comando.Parameters.AddWithValue("@NombrePrograma", programa.NombrePrograma);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }

        public void CambioEstado(Programa programa)
        {
            String CambioEstadoSP = "Silabo.CambioEstadoProgramaSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(CambioEstadoSP);
                comando.Parameters.AddWithValue("@CodigoPrograma", programa.CodigoPrograma);
                comando.Parameters.AddWithValue("@EstadoPrograma", programa.EstadoPrograma);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método cambio de estado. " + error.Message, error);
            }
        }

        private Programa ObtenerDatosPrograma(SqlDataReader reader)
        {
            Programa programa = new Programa();
            programa.CodigoPrograma = reader.GetString(0);
            programa.GrupoPrograma.CodigoGrupo = reader.GetString(1);
            programa.NombrePrograma = reader.GetString(2);
            programa.EstadoPrograma = reader.GetString(3)[0];
            return programa;
        }
    }
}
