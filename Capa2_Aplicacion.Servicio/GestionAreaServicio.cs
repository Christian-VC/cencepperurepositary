﻿using Capa4_Persistencia.SqlServer.ModuloBase;
using Capa4_Persistencia.SqlServer.ModuloPrincipal;
using Capa3_Dominio.ModuloPrincipal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa3_Dominio.ModuloPrincipal.Entidad;

namespace Capa2_Aplicacion.Servicio
{
    public class GestionAreaServicio
    {
        private ConectionSqlServer conectionSqlServer;
        private AreaSQL areaSQL;
        public GestionAreaServicio() {
            conectionSqlServer = new ConectionSqlServer();
            areaSQL = new AreaSQL(conectionSqlServer);
        }

        public List<Area> ListarArea()
        {
            List<Area> listaArea;
            try
            {
                conectionSqlServer.StartTransaction();
                listaArea = areaSQL.Listar();
                conectionSqlServer.FinishTransaction();
                return listaArea;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Area BuscarArea(String codigo)
        {
            Area area;
            try
            {
                conectionSqlServer.StartTransaction();
                area = areaSQL.Buscar(codigo);
                conectionSqlServer.FinishTransaction();
                return area;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void RegistrarArea(Area area)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                areaSQL.Registrar(area);
                conectionSqlServer.CloseConnection();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void ActualizarArea(Area area)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                areaSQL.Actualizar(area);
                conectionSqlServer.FinishTransaction();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public void CambiarEstadoArea(Area area)
        {
            try
            {
                conectionSqlServer.StartTransaction();
                areaSQL.CambioEstado(area);
                conectionSqlServer.FinishTransaction();
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
