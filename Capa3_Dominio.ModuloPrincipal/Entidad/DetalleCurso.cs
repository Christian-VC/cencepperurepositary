using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class DetalleCurso
    {
        private Alumno alumnoDetalleCurso;
        private Programa programaDetalleCurso;
        private DateTime fechaInicioDetalleCurso;
        private DateTime fechaFinDetalleCurso;

        public Alumno AlumnoDetalleCurso { get => alumnoDetalleCurso; set => alumnoDetalleCurso = value; }
        public Programa ProgramaDetalleCurso { get => programaDetalleCurso; set => programaDetalleCurso = value; }
        public DateTime FechaInicioDetalleCurso { get => fechaInicioDetalleCurso; set => fechaInicioDetalleCurso = value; }
        public DateTime FechaFinDetalleCurso { get => fechaFinDetalleCurso; set => fechaFinDetalleCurso = value; }

    }
}
