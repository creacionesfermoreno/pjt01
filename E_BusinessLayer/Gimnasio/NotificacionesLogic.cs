using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
namespace E_BusinessLayer.Gimnasio
{
    public class NotificacionesLogic
    {
        NotificacionesData oNotificacionesData = null;
        public NotificacionesLogic()
		{
            oNotificacionesData = new NotificacionesData();
		}

        public List<ProductoDTO> VerMasStockProductos(int CodigoUnidadNegocio,int TipoBusqueda, int CodigoCategoria, int CodSede)
        {
            return oNotificacionesData.VerMasStockProductos(CodigoUnidadNegocio, TipoBusqueda, CodigoCategoria, CodSede);
        }
        
        public List<ProductoDTO> NotificacionStockProductos(int CodigoUnidadNegocio,int CodSede)
        {
            return oNotificacionesData.NotificacionStockProductos(CodigoUnidadNegocio,CodSede);
        }

     
    }
}
