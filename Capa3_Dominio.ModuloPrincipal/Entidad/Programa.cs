using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class Programa
    {
        private String codigoPrograma;
        private Grupo grupoPrograma;
        private String nombrePrograma;
        private Char estadoPrograma;

        public String CodigoPrograma { get => codigoPrograma; set => codigoPrograma = value; }
        public Grupo GrupoPrograma { get => grupoPrograma; set => grupoPrograma = value; }
        public String NombrePrograma { get => nombrePrograma; set => nombrePrograma = value; }
        public Char EstadoPrograma { get => estadoPrograma; set => estadoPrograma = value; }
    }
}
