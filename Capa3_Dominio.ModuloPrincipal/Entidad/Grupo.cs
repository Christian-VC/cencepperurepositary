using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class Grupo
    {
        private String codigoGrupo;
        private Area areaGrupo;
        private String nombreGrupo;
        private Char estadoGrupo;

        public String CodigoGrupo { get => codigoGrupo; set => codigoGrupo = value; }
        public Area AreaGrupo { get => areaGrupo; set => areaGrupo = value; }
        public String NombreGrupo { get => nombreGrupo; set => nombreGrupo = value; }
        public Char EstadoGrupo { get => estadoGrupo; set => estadoGrupo = value; }
    }
}
