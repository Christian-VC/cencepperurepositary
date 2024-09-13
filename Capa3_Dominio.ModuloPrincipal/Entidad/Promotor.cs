using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.ModuloPrincipal.Entidad
{
    public class Promotor
    {
        private String codigoPromotor;
        private String apellidoPaternoPromotor;
        private String apellidoMaternoPromotor;
        private String nombresPromotor;
        private String dniPromotor;
        private String celularPromotor;
        private String correoPromotor;
        private Char estadoPromotor;

        public String CodigoPromotor { get => codigoPromotor; set => codigoPromotor = value; }
        public String ApellidoPaternoPromotor { get => apellidoPaternoPromotor; set => apellidoPaternoPromotor = value; }
        public String ApellidoMaternoPromotor { get => apellidoMaternoPromotor; set => apellidoMaternoPromotor = value; }
        public String NombresPromotor { get => nombresPromotor; set => nombresPromotor = value; }
        public String DniPromotor { get => dniPromotor; set => dniPromotor = value; }
        public String CelularPromotor { get => celularPromotor; set => celularPromotor = value; }
        public String CorreoPromotor { get => correoPromotor; set => correoPromotor = value; }
        public Char EstadoPromotor { get => estadoPromotor; set => estadoPromotor = value; }

    }
}
