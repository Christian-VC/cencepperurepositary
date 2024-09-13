using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class Alumno
    {
        private String codigoAlumno;
        private String apellidoPaternoAlumno;
        private String apellidoMaternoAlumno;
        private String nombresAlumno;
        private String dniAlumno;
        private Char estadoAlumno;

        public String CodigoAlumno { get => codigoAlumno; set => codigoAlumno = value; }
        public String ApellidoPaternoAlumno { get => apellidoPaternoAlumno; set => apellidoPaternoAlumno = value; }
        public String ApellidoMaternoAlumno { get => apellidoMaternoAlumno; set => apellidoMaternoAlumno = value; }
        public String NombresAlumno { get => nombresAlumno; set => nombresAlumno = value; }
        public String DniAlumno { get => dniAlumno; set => dniAlumno = value; }
        public Char EstadoAlumno { get => estadoAlumno; set => estadoAlumno = value; }
    }
}
