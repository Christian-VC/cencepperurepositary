using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class Area
    {
        private String codigoArea;
        private String nombreArea;
        private Char estadoArea;

        public String CodigoArea { get => codigoArea; set => codigoArea = value; }
        public String NombreArea { get => nombreArea; set => nombreArea = value; }
        public Char EstadoArea { get => estadoArea; set => estadoArea = value; }


    }
}
