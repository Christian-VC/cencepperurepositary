using Capa3_Dominio.ModuloPrincipal.Entidad;
using Capa4_Persistencia.SqlServer.ModuloBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Capa4_Persistencia.SqlServer.ModuloPrincipal
{
    public class AlumnoSQL
    {
        ConectionSqlServer conectionSqlServer;

        public AlumnoSQL (ConectionSqlServer conectionSqlServer)
        {
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<Alumno> Listar()
        {
            List<Alumno> listaAlumno = new List<Alumno>();
            String ListarAlumnoSP = "Persona.ListarAlumnoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ListarAlumnoSP);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    listaAlumno.Add(ObtenerDatosAlumno(reader));
                }
                reader.Close();
                return listaAlumno;
            } catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método listar. " + error.Message, error);
            }
        }

        public Alumno Buscar (String codigoAlumno)
        {
            Alumno alumno = new Alumno();
            String BuscarAlumnoCodigoSP = "Persona.BuscarAlumnoCodigoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(BuscarAlumnoCodigoSP);
                comando.Parameters.AddWithValue("@CodigoAlumno", codigoAlumno);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    alumno.CodigoAlumno = codigoAlumno;
                    alumno.ApellidoPaternoAlumno = reader.GetString(0);
                    alumno.ApellidoMaternoAlumno = reader.GetString(1);
                    alumno.NombresAlumno = reader.GetString(2);
                    alumno.DniAlumno = reader.GetString(3);
                    alumno.EstadoAlumno = reader.GetString(4)[0];
                    reader.Close();
                }
                return alumno;
            }
            catch (Exception error) {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar(Alumno alumno)
        {
            String RegistrarAlumnoSP = "Persona.RegistrarAlumnoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(RegistrarAlumnoSP);
                comando.Parameters.AddWithValue("@ApellidoPaternoAlumno", alumno.ApellidoPaternoAlumno);
                comando.Parameters.AddWithValue("@ApellidoMaternoAlumno", alumno.ApellidoMaternoAlumno);
                comando.Parameters.AddWithValue("@NombresAlumno", alumno.NombresAlumno);
                comando.Parameters.AddWithValue("@DniAlumno", alumno.DniAlumno);
                comando.ExecuteNonQuery();
            } catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }
        }

        public void Actualizar(Alumno alumno)
        {
            String ActualizarAlumnoSP = "Persona.ActualizarAlumnoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarAlumnoSP);
                comando.Parameters.AddWithValue("@CodigoAlumno", alumno.CodigoAlumno);
                comando.Parameters.AddWithValue("@ApellidoPaternoAlumno", alumno.ApellidoPaternoAlumno);
                comando.Parameters.AddWithValue("@ApellidoMaternoAlumno", alumno.ApellidoMaternoAlumno);
                comando.Parameters.AddWithValue("@NombresAlumno", alumno.NombresAlumno);
                comando.Parameters.AddWithValue("@DniAlumno", alumno.DniAlumno);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }

        public void CambiarEstado(Alumno alumno)
        {
            String CambioEstadoSP = "Persona.CambioEstadoAlumnoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(CambioEstadoSP);
                comando.Parameters.AddWithValue("@CodigoAlumno", alumno.CodigoAlumno);
                comando.Parameters.AddWithValue("@EstadoAlumno", alumno.EstadoAlumno);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método cambio de estado. " + error.Message, error);
            }
        }

        private Alumno ObtenerDatosAlumno(SqlDataReader reader)
        {
            Alumno alumno = new Alumno();
            alumno.CodigoAlumno = reader.GetString(0);
            alumno.ApellidoPaternoAlumno = reader.GetString(1);
            alumno.ApellidoMaternoAlumno = reader.GetString(2);
            alumno.NombresAlumno = reader.GetString(3);
            alumno.DniAlumno = reader.GetString(4);
            alumno.EstadoAlumno = reader.GetString(5)[0];
            return alumno;
        }
    }
}
