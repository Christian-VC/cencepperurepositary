using Capa3_Dominio.ModuloPrincipal.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class Pedido
    {
        private String codigoPedido;
        private Promotor promotorPedido;
        private String nombrePedido;
        private DateTime fechaPedido;
        private int cantidadPedido;
        private Char estadoPedido;

        public String CodigoPedido { get => codigoPedido; set => codigoPedido = value; }
        public Promotor PromotorPedido { get => promotorPedido; set => promotorPedido = value; }
        public String NombrePedido { get => nombrePedido; set => nombrePedido = value; }
        public DateTime FechaPedido { get => fechaPedido; set => fechaPedido = value; }
        public int CantidadPedido { get => cantidadPedido; set => cantidadPedido = value; }
        public Char EstadoPedido { get => estadoPedido; set => estadoPedido = value; }
    }
}
