using Capa3_Dominio.ModuloPrincipal.Entidad;
using Capa4_Persistencia.SqlServer.ModuloBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capa4_Persistencia.SqlServer.ModuloPrincipal
{
    public class GrupoSQL
    {
        private ConectionSqlServer conectionSqlServer;

        public GrupoSQL(ConectionSqlServer conectionSqlServer)
        {
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<Grupo> Listar()
        {
            String ListarGrupoSP = "Silabo.ListarGrupoSP";
            List<Grupo> listarGrupo = new List<Grupo>();
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ListarGrupoSP);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    listarGrupo.Add(ObtenerDatosGrupo(reader));
                }
                reader.Close();

                for (int i = 0; i < listarGrupo.Count; i++)
                {
                    Grupo grupotemp = listarGrupo.ElementAt(i);
                    AreaSQL areaSQL = new AreaSQL(conectionSqlServer);
                    grupotemp.AreaGrupo = areaSQL.Buscar(grupotemp.AreaGrupo.CodigoArea);
                    listarGrupo[i] = grupotemp;
                }
                return listarGrupo;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método listar. " + error.Message, error);
            }
        }

        public Grupo Buscar(String codigoGrupo)
        {
            Grupo grupo = new Grupo();
            String BuscarGrupoCodigoSP = "Silabo.BuscarGrupoCodigoSP";

            try
            {
                SqlCommand comando;
                comando = conectionSqlServer.GetProcedureCommand(BuscarGrupoCodigoSP);
                comando.Parameters.AddWithValue("@CodigoGrupo", codigoGrupo);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    grupo.CodigoGrupo = codigoGrupo;
                    String codigoAreaGrupo = reader.GetString(0);
                    grupo.NombreGrupo = reader.GetString(1);
                    grupo.EstadoGrupo = reader.GetString(2)[0];
                    reader.Close();

                    // Asignarle el objeto área al grupo
                    AreaSQL areaSQL = new AreaSQL(conectionSqlServer);
                    grupo.AreaGrupo = areaSQL.Buscar(codigoAreaGrupo);
                }
                return grupo;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar(Grupo grupo)
        {
            String RegistrarGrupoSP = "Silabo.RegistrarGrupoSP";
            try
            {
                SqlCommand comando;
                comando = conectionSqlServer.GetProcedureCommand(RegistrarGrupoSP);
                comando.Parameters.AddWithValue("@CodigoAreaGrupo", grupo.AreaGrupo.CodigoArea);
                comando.Parameters.AddWithValue("@NombreGrupo", grupo.NombreGrupo);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }
        }

        public void Actualizar(Grupo grupo)
        {
            String ActualizarGrupoSP = "Silabo.ActualizarGrupoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarGrupoSP);
                comando.Parameters.AddWithValue("@CodigoGrupo", grupo.CodigoGrupo);
                comando.Parameters.AddWithValue("@CodigoAreaGrupo", grupo.AreaGrupo.CodigoArea);
                comando.Parameters.AddWithValue("@NombreGrupo", grupo.NombreGrupo);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }

        public void CambioEstado(Grupo grupo)
        {
            String CambioEstadoSP = "Silabo.CambioEstadoGrupoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(CambioEstadoSP);
                comando.Parameters.AddWithValue("@CodigoGrupo", grupo.CodigoGrupo);
                comando.Parameters.AddWithValue("@EstadoGrupo", grupo.EstadoGrupo);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método cambio de estado. " + error.Message, error);
            }
        }

        private Grupo ObtenerDatosGrupo(SqlDataReader reader)
        {
            Grupo grupo = new Grupo();
            grupo.CodigoGrupo = reader.GetString(0);
            grupo.AreaGrupo.CodigoArea = reader.GetString(1);
            grupo.NombreGrupo = reader.GetString(2);
            grupo.EstadoGrupo = reader.GetString(3)[0];
            return grupo;
        }
    }
}
