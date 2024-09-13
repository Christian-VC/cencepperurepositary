using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class Modulo
    {
        private String codigoModulo;
        private Programa programaModulo;
        private String nombreModulo;
        private Char estadoModulo;

        public String CodigoModulo { get => codigoModulo; set => codigoModulo = value; }
        public Programa ProgramaModulo { get => programaModulo; set => programaModulo = value; }
        public String NombreModulo { get => nombreModulo; set => nombreModulo = value; }
        public Char EstadoModulo { get => estadoModulo; set => estadoModulo = value; }

    }
}
