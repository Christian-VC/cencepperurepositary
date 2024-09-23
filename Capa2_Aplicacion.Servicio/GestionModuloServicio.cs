using Capa3_Dominio.ModuloPrincipal.Entidad;
using Capa4_Persistencia.SqlServer.ModuloBase;
using Capa4_Persistencia.SqlServer.ModuloPrincipal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa2_Aplicacion.Servicio
{
    internal class GestionModuloServicio
    {
        private ConectionSqlServer conectionSqlServer;
        private ModuloSQL moduloSQL;
        public GestionModuloServicio()
        {
            conectionSqlServer = new ConectionSqlServer();
            moduloSQL = new ModuloSQL(conectionSqlServer);
        }

        public List<Modulo> ListarModulo()
        {
            List<Modulo> listaModulo;
            try
            {
                conectionSqlServer.StartTransaction();
                listaModulo = moduloSQL.Listar();
                conectionSqlServer.FinishTransaction();
                return listaModulo;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Modulo BuscarModulo(String codigo)
        {
            Modulo modulo;
            try
            {
                conectionSqlServer.StartTransaction();
                modulo = moduloSQL.Buscar(codigo);
                conectionSqlServer.FinishTransaction();
                return modulo;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void RegistrarModulo(Modulo modulo)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                moduloSQL.Registrar(modulo);
                conectionSqlServer.CloseConnection();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void ActualizarModulo(Modulo modulo)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                moduloSQL.Actualizar(modulo);
                conectionSqlServer.FinishTransaction();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void CambiarEstadoModulo(Modulo modulo)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                moduloSQL.CambioEstado(modulo);
                conectionSqlServer.FinishTransaction();
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
