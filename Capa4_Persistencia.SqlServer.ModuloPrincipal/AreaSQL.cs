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
    public class AreaSQL
    {
        private ConectionSqlServer conectionSqlServer;

        public AreaSQL (ConectionSqlServer conectionSqlServer)
        {
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<Area> Listar()
        {
            String ListarAreaSP = "Silabo.ListarAreaSP";
            List<Area> listarArea = new List<Area>();
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ListarAreaSP);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    listarArea.Add(ObtenerDatosArea(reader));
                }
                return listarArea;
            } catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método listar. " + error.Message, error);
            }
        }

        public Area Buscar(String codigoArea)
        {
            Area area = new Area();
            String BuscarAreaCodigoSP = "Silabo.BuscarAreaCodigoSP";

            try
            {
                SqlCommand comando;
                comando = conectionSqlServer.GetProcedureCommand(BuscarAreaCodigoSP);
                comando.Parameters.AddWithValue("@CodigoArea", codigoArea);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    area.CodigoArea = codigoArea;
                    area.NombreArea = reader.GetString(0);
                    area.EstadoArea = reader.GetString(1)[0];
                    reader.Close();
                }
                return area;
            } catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar(Area area)
        {
            String RegistrarAreaSP = "Silabo.RegistrarAreaSP";
            try
            {
                SqlCommand comando;
                comando = conectionSqlServer.GetProcedureCommand(RegistrarAreaSP);
                comando.Parameters.AddWithValue("@NombreArea", area.NombreArea);
                comando.ExecuteNonQuery();
            } catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }        
        }

        public void Actualizar(Area area)
        {
            String ActualizarAreaSP = "Silabo.ActualizarAreaSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarAreaSP);
                comando.Parameters.AddWithValue("@CodigoArea", area.CodigoArea);
                comando.Parameters.AddWithValue("@NombreArea",area.NombreArea);
                comando.ExecuteNonQuery();
            } catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }

        public void CambioEstado(Area area)
        {
            String CambioEstadoSP = "Silabo.CambioEstadoAreaSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(CambioEstadoSP);
                comando.Parameters.AddWithValue("@CodigoArea", area.CodigoArea);
                comando.Parameters.AddWithValue("@EstadoArea", area.EstadoArea);
                comando.ExecuteNonQuery();
            } catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método cambio de estado. " + error.Message, error);
            }
        }

        private Area ObtenerDatosArea(SqlDataReader reader)
        {
            Area area = new Area();
            area.CodigoArea = reader.GetString(0);
            area.NombreArea = reader.GetString(1);
            area.EstadoArea = reader.GetString(2)[0];
            return area;
        }
    }
}
