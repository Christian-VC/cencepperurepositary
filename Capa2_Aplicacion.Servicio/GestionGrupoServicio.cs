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
    public class GestionGrupoServicio
    {
        private ConectionSqlServer conectionSqlServer;
        private GrupoSQL grupoSQL;
        public GestionGrupoServicio()
        {
            conectionSqlServer = new ConectionSqlServer();
            grupoSQL = new GrupoSQL(conectionSqlServer);
        }

        public List<Grupo> ListarGrupo()
        {
            List<Grupo> listaGrupo;
            try
            {
                conectionSqlServer.StartTransaction();
                listaGrupo = grupoSQL.Listar();
                conectionSqlServer.FinishTransaction();
                return listaGrupo;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Grupo BuscarGrupo(String codigo)
        {
            Grupo grupo;
            try
            {
                conectionSqlServer.StartTransaction();
                grupo = grupoSQL.Buscar(codigo);
                conectionSqlServer.FinishTransaction();
                return grupo;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void RegistrarGrupo(Grupo grupo)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                grupoSQL.Registrar(grupo);
                conectionSqlServer.CloseConnection();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void ActualizarGrupo(Grupo grupo)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                grupoSQL.Actualizar(grupo);
                conectionSqlServer.FinishTransaction();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void CambiarEstadoGrupo(Grupo grupo)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                grupoSQL.CambioEstado(grupo);
                conectionSqlServer.FinishTransaction();
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
