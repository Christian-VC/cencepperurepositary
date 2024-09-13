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
    public class PromotorSQL
    {
        ConectionSqlServer conectionSqlServer;

        public PromotorSQL(ConectionSqlServer conectionSqlServer)
        {
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<Promotor> Listar()
        {
            List<Promotor> listaPromotor = new List<Promotor>();
            String ListarPromotorSP = "Persona.ListarPromotorSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ListarPromotorSP);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    listaPromotor.Add(ObtenerDatosPromotor(reader));
                }
                reader.Close();
                return listaPromotor;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método listar. " + error.Message, error);
            }
        }

        public Promotor Buscar(String codigoPromotor)
        {
            Promotor promotor = new Promotor();
            String BuscarPromotorCodigoSP = "Persona.BuscarPromotorCodigoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(BuscarPromotorCodigoSP);
                comando.Parameters.AddWithValue("@CodigoPromotor", codigoPromotor);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    promotor.CodigoPromotor = codigoPromotor;
                    promotor.ApellidoPaternoPromotor = reader.GetString(0);
                    promotor.ApellidoMaternoPromotor = reader.GetString(1);
                    promotor.NombresPromotor = reader.GetString(2);
                    promotor.DniPromotor = reader.GetString(3);
                    promotor.CelularPromotor = reader.GetString(4);
                    promotor.CorreoPromotor = reader.GetString(5);
                    promotor.EstadoPromotor = reader.GetString(6)[0];
                    reader.Close();
                }
                return promotor;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar(Promotor promotor)
        {
            String RegistrarPromotorSP = "Persona.RegistrarPromotorSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(RegistrarPromotorSP);
                comando.Parameters.AddWithValue("@ApellidoPaternoPromotor", promotor.ApellidoPaternoPromotor);
                comando.Parameters.AddWithValue("@ApellidoMaternoPromotor", promotor.ApellidoMaternoPromotor);
                comando.Parameters.AddWithValue("@NombresPromotor", promotor.NombresPromotor);
                comando.Parameters.AddWithValue("@DniPromotor", promotor.DniPromotor);
                comando.Parameters.AddWithValue("@CelularPromotor", promotor.CelularPromotor);
                comando.Parameters.AddWithValue("@CorreoPromotor", promotor.CorreoPromotor);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }
        }

        public void Actualizar(Promotor promotor)
        {
            String ActualizarPromotorSP = "Persona.ActualizarPromotorSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarPromotorSP);
                comando.Parameters.AddWithValue("@CodigoPromotor", promotor.CodigoPromotor);
                comando.Parameters.AddWithValue("@ApellidoPaternoPromotor", promotor.ApellidoPaternoPromotor);
                comando.Parameters.AddWithValue("@ApellidoMaternoPromotor", promotor.ApellidoMaternoPromotor);
                comando.Parameters.AddWithValue("@NombresPromotor", promotor.NombresPromotor);
                comando.Parameters.AddWithValue("@DniPromotor", promotor.DniPromotor);
                comando.Parameters.AddWithValue("@CelularPromotor", promotor.CelularPromotor);
                comando.Parameters.AddWithValue("@CorreoPromotor", promotor.CorreoPromotor);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }

        public void CambiarEstado(Promotor promotor)
        {
            String CambioEstadoSP = "Persona.CambioEstadoPromotorSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(CambioEstadoSP);
                comando.Parameters.AddWithValue("@CodigoPromotor", promotor.CodigoPromotor);
                comando.Parameters.AddWithValue("@EstadoPromotor", promotor.EstadoPromotor);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método cambio de estado. " + error.Message, error);
            }
        }

        private Promotor ObtenerDatosPromotor(SqlDataReader reader)
        {
            Promotor promotor = new Promotor();
            promotor.CodigoPromotor = reader.GetString(0);
            promotor.ApellidoPaternoPromotor = reader.GetString(1);
            promotor.ApellidoMaternoPromotor = reader.GetString(2);
            promotor.NombresPromotor = reader.GetString(3);
            promotor.DniPromotor = reader.GetString(4);
            promotor.CelularPromotor = reader.GetString(5);
            promotor.CorreoPromotor = reader.GetString(6);
            promotor.EstadoPromotor = reader.GetString(7)[0];
            return promotor;
        }
    }
}
