using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class DetallePedido
    {
        private Pedido pedidoDetallePedido;
        private Alumno alumnoDetallePedido;
        private Modulo moduloDetallePedido;

        public Pedido PedidoDetallePedido { get => pedidoDetallePedido; set => pedidoDetallePedido = value; }
        public Alumno AlumnoDetallePedido { get => alumnoDetallePedido; set => alumnoDetallePedido = value; }
        public Modulo ModuloDetallePedido { get => moduloDetallePedido; set => moduloDetallePedido = value; }

    }
}
