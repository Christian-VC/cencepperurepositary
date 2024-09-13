using Capa3_Dominio.ModuloPrincipal.Datos;
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
    public class PedidoSQL
    {
        ConectionSqlServer conectionSqlServer;

        public PedidoSQL(ConectionSqlServer conectionSqlServer)
        {
            this.conectionSqlServer = conectionSqlServer;
        }

        public List<Pedido> Listar()
        {
            List<Pedido> listaPedido = new List<Pedido>();
            String ListarPedidoSP = "Servicio.ListarPedidoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ListarPedidoSP);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    listaPedido.Add(ObtenerDatosPedido(reader));
                }
                reader.Close();
                for (int i = 0; i < listaPedido.Count; i++)
                {
                    Pedido pedidoTemp = listaPedido.ElementAt(i);
                    PromotorSQL promotorSQL = new PromotorSQL(conectionSqlServer);
                    pedidoTemp.PromotorPedido = promotorSQL.Buscar(pedidoTemp.PromotorPedido.CodigoPromotor);
                    listaPedido[i] = pedidoTemp;
                }
                return listaPedido;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método listar. " + error.Message, error);
            }
        }

        public Pedido Buscar(String codigoPedido)
        {
            Pedido pedido = new Pedido();
            String BuscarPedidoCodigoSP = "Servicio.BuscarPedidoCodigoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(BuscarPedidoCodigoSP);
                comando.Parameters.AddWithValue("@CodigoPedido", codigoPedido);
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    pedido.CodigoPedido = codigoPedido;
                    String codigoPromotorPedido = reader.GetString(0);
                    pedido.NombrePedido = reader.GetString(1);
                    pedido.FechaPedido = reader.GetDateTime(2);
                    pedido.CantidadPedido = reader.GetInt32(3);
                    pedido.EstadoPedido = reader.GetString(4)[0];
                    reader.Close();

                    PromotorSQL promotorSQL = new PromotorSQL(conectionSqlServer);
                    pedido.PromotorPedido = promotorSQL.Buscar(codigoPromotorPedido);
                }
                return pedido;
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método buscar. " + error.Message, error);
            }
        }

        public void Registrar(Pedido pedido)
        {
            String RegistrarPedidoSP = "Servicio.RegistrarPedidoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(RegistrarPedidoSP);
                comando.Parameters.AddWithValue("@CodigoPromotorPedido", pedido.PromotorPedido.CodigoPromotor);
                comando.Parameters.AddWithValue("@NombrePedido", pedido.NombrePedido);
                comando.Parameters.AddWithValue("@FechaPedido", pedido.FechaPedido);
                comando.Parameters.AddWithValue("@CantidadPedido", pedido.CantidadPedido);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método registrar. " + error.Message, error);
            }
        }

        public void Actualizar(Pedido pedido)
        {
            String ActualizarPedidoSP = "Servicio.ActualizarPedidoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(ActualizarPedidoSP);
                comando.Parameters.AddWithValue("@CodigoPedido", pedido.CodigoPedido);
                comando.Parameters.AddWithValue("@CodigoPromotorPedido", pedido.PromotorPedido.CodigoPromotor);
                comando.Parameters.AddWithValue("@NombrePedido", pedido.NombrePedido);
                comando.Parameters.AddWithValue("@FechaPedido", pedido.FechaPedido);
                comando.Parameters.AddWithValue("@CantidadPedido", pedido.CantidadPedido);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método actualizar. " + error.Message, error);
            }
        }

        public void CambiarEstado(Pedido pedido)
        {
            String CambioEstadoSP = "Servicio.CambioEstadoPedidoSP";
            try
            {
                SqlCommand comando = conectionSqlServer.GetProcedureCommand(CambioEstadoSP);
                comando.Parameters.AddWithValue("@CodigoPedido", pedido.CodigoPedido);
                comando.Parameters.AddWithValue("@EstadoPedido", pedido.EstadoPedido);
                comando.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                throw new Exception("Error: Incoveniente con el método cambio de estado. " + error.Message, error);
            }
        }

        private Pedido ObtenerDatosPedido(SqlDataReader reader)
        {
            Pedido pedido = new Pedido();
            pedido.CodigoPedido = reader.GetString(0);
            pedido.PromotorPedido.CodigoPromotor = reader.GetString(1);
            pedido.NombrePedido = reader.GetString(2);
            pedido.FechaPedido = reader.GetDateTime(3);
            pedido.CantidadPedido = reader.GetInt32(4);
            pedido.EstadoPedido = reader.GetString(5)[0];
            return pedido;
        }
    }
}
