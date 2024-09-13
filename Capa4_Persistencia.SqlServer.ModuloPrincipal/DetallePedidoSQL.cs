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
    public class DetallePedidoSQL
    {
        ConectionSqlServer conectionSqlServer;

        public DetallePedidoSQL(ConectionSqlServer conectionSqlServer){
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<DetallePedido> Buscar(String codigoPedido)
        {
            String BuscarDetalleCodigoPedidoSP = "Servicio.BuscarDetalleCodigoPedidoSP";
            List<DetallePedido> listaDetallePedido = new List<DetallePedido>();
            try 
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(BuscarDetalleCodigoPedidoSP);
                comando.Parameters.AddWithValue("@CodigoPedidoDetallePedido", codigoPedido);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    DetallePedido detallePedido = new DetallePedido();
                    detallePedido.PedidoDetallePedido.CodigoPedido = codigoPedido;
                    detallePedido.AlumnoDetallePedido.CodigoAlumno = reader.GetString(0);
                    detallePedido.ModuloDetallePedido.CodigoModulo = reader.GetString(1);
                    listaDetallePedido.Add(detallePedido);
                }
                return listaDetallePedido;
            }catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar (DetallePedido detallePedido)
        {
            String RegistrarDetallePedidoSP = "Servicio.RegistrarDetallePedidoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(RegistrarDetallePedidoSP);
                comando.Parameters.AddWithValue("@CodigoPedidoDetallePedido", detallePedido.PedidoDetallePedido.CodigoPedido);
                comando.Parameters.AddWithValue("@CodigoAlumnoDetallePedido", detallePedido.AlumnoDetallePedido.CodigoAlumno);
                comando.Parameters.AddWithValue("@CodigoModuloDetallePedido", detallePedido.ModuloDetallePedido.CodigoModulo);
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }
        }

        public void Actualizar(DetallePedido detallePedido)
        {
            String ActualizarDetallePedidoSP = "Servicio.ActualizarDetallePedidoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarDetallePedidoSP);
                comando.Parameters.AddWithValue("@CodigoPedidoDetallePedido", detallePedido.PedidoDetallePedido.CodigoPedido);
                comando.Parameters.AddWithValue("@CodigoAlumnoDetallePedido", detallePedido.AlumnoDetallePedido.CodigoAlumno);
                comando.Parameters.AddWithValue("@CodigoModuloDetallePedido", detallePedido.ModuloDetallePedido.CodigoModulo);
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }
    }
}
