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
    public class DetalleCursoSQL
    {
        ConectionSqlServer conectionSqlServer;

        public DetalleCursoSQL(ConectionSqlServer conectionSqlServer)
        {
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<DetalleCurso> Buscar(String codigoAlumno)
        {
            String BuscarDetalleCursoCodigoAlumnoSP = "Servicio.BuscarDetalleCursoCodigoAlumnoSP";
            List<DetalleCurso> listaDetallePedido = new List<DetalleCurso>();
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(BuscarDetalleCursoCodigoAlumnoSP);
                comando.Parameters.AddWithValue("@CodigoAlumnoDetalleCurso", codigoAlumno);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DetalleCurso detalleCurso = new DetalleCurso();
                    detalleCurso.AlumnoDetalleCurso.CodigoAlumno = codigoAlumno;
                    detalleCurso.ProgramaDetalleCurso.CodigoPrograma = reader.GetString(0);
                    detalleCurso.FechaInicioDetalleCurso = reader.GetDateTime(1);
                    detalleCurso.FechaFinDetalleCurso = reader.GetDateTime(2);
                    listaDetallePedido.Add(detalleCurso);
                }
                return listaDetallePedido;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar(DetalleCurso detalleCurso)
        {
            String RegistrarDetalleCursoSP = "Servicio.RegistrarDetalleCursoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(RegistrarDetalleCursoSP);
                comando.Parameters.AddWithValue("@CodigoAlumnoDetalleCurso", detalleCurso.AlumnoDetalleCurso.CodigoAlumno);
                comando.Parameters.AddWithValue("@CodigoProgramaDetalleCurso", detalleCurso.ProgramaDetalleCurso.CodigoPrograma);
                comando.Parameters.AddWithValue("@FechaInicioDetalleCurso", detalleCurso.FechaInicioDetalleCurso);
                comando.Parameters.AddWithValue("@FechaFinDetalleCurso", detalleCurso.FechaFinDetalleCurso);
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }
        }

        public void Actualizar(DetalleCurso detalleCurso)
        {
            String ActualizarDetalleCursoSP = "Servicio.ActualizarDetalleCursoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarDetalleCursoSP);
                comando.Parameters.AddWithValue("@CodigoAlumnoDetalleCurso", detalleCurso.AlumnoDetalleCurso.CodigoAlumno);
                comando.Parameters.AddWithValue("@CodigoProgramaDetalleCurso", detalleCurso.ProgramaDetalleCurso.CodigoPrograma);
                comando.Parameters.AddWithValue("@FechaInicioDetalleCurso", detalleCurso.FechaInicioDetalleCurso);
                comando.Parameters.AddWithValue("@FechaFinDetalleCurso", detalleCurso.FechaFinDetalleCurso);
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }
    }
}
