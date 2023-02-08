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
	//-------------------------------------------------------------------
	//Archivo     : PedidosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 15/10/2015
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class PedidosLogic: IDisposable
	{
		PedidosData oPedidosData = null;
		public PedidosLogic()
		{
			oPedidosData = new PedidosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PedidosGetList
		//Objetivo: Retorna una colección de registros de tipo PedidosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListPedidosDTO PedidosGetList(ReqFilterPedidosDTO oReqFilterPedidosDTO)
		{
		
			RespListPedidosDTO oRespListPedidosDTO = new RespListPedidosDTO();
		
			oRespListPedidosDTO.List = new List<PedidosDTO>();
			oRespListPedidosDTO.User = oReqFilterPedidosDTO.User;
			oRespListPedidosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPedidosDTO.User))
            {
                oRespListPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Pedidos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPedidosDTO.Paging == null)
            {
                oRespListPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPedidosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    if (!oReqFilterPedidosDTO.Paging.All && oReqFilterPedidosDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPedidosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<PedidosDTO> PedidosDTOList = new List<PedidosDTO>();

                    switch (oReqFilterPedidosDTO.FilterCase)
                    {                                          
                        default:
                            PedidosDTOList = oPedidosData.Listar(oReqFilterPedidosDTO.Item);
                            break;
                    }

                    oRespListPedidosDTO.List = PedidosDTOList;
                    oRespListPedidosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPedidosDTO.Success = false;
                    oRespListPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListPedidosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PedidosGetItem
		//Objetivo: Retorna un registro de tipo PedidosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemPedidosDTO PedidosGetItem(ReqFilterPedidosDTO oReqFilterPedidosDTO)
		{
			RespItemPedidosDTO oRespItemPedidosDTO = new RespItemPedidosDTO();

            oRespItemPedidosDTO.Success = false;
            oRespItemPedidosDTO.Item = null;
            oRespItemPedidosDTO.User = oReqFilterPedidosDTO.User;
            oRespItemPedidosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPedidosDTO.User))
            {
                oRespItemPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Pedidos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPedidosDTO.MessageList.Count == 0)
            {
                PedidosDTO oPedidosDTO = null;
                try
                {
                    switch (oReqFilterPedidosDTO.FilterCase)
                    {                                              
                        default:
                            {
                                oPedidosDTO = new PedidosDTO();
                            }
                            break;
                    }

                    oRespItemPedidosDTO.Item = new PedidosDTO();
                    oRespItemPedidosDTO.Item = oPedidosDTO;
                    oRespItemPedidosDTO.Success = true;
                    oRespItemPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPedidosDTO.Success = false;
                    oRespItemPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPedidosDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo PedidosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespPedidosDTO ExecuteTransac(ReqPedidosDTO oReqPedidosDTO)
		{
			RespPedidosDTO oRespPedidosDTO = new RespPedidosDTO();
            
            oRespPedidosDTO.MessageList = new List<Mensaje>();
            oRespPedidosDTO.User = oReqPedidosDTO.User;
            
            if (String.IsNullOrEmpty(oReqPedidosDTO.User))
            {
                oRespPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Pedidos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPedidosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {

                        if (oReqPedidosDTO.List[0].ListaDetalle == null)
                        {
                            foreach (PedidosDTO item in oReqPedidosDTO.List)
                            {
                                switch (item.Operation)
                                {
                                      
                                }
                            }    
                        }
                        else
                        {
                            var codigoTP = 2;
                            foreach (PedidosDTO item in oReqPedidosDTO.List[0].ListaDetalle)
                            {
                                switch (item.Operation)
                                {
                                    case Operation.Create:
                                        oPedidosData.Registrar(item);
                                        break;
                                    case Operation.Update:
                                        oPedidosData.Actualizar(item);
                                        break;                                  
                                }
                                

                                ConfiguracionData oConfiguracionData = new ConfiguracionData();
                                ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO.Codigo = 1;
                                oConfiguracionDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                oConfiguracionDTO = oConfiguracionData.BuscarPorCodigoConfiguracion(oConfiguracionDTO);
                                if (oConfiguracionDTO.GenerarSerie && codigoTP == 2)
                                {
                                    SeriesData oSeriesData = new SeriesData();
                                    SeriesDTO oSeriesDTO = new SeriesDTO();
                                    oSeriesDTO.TipoDocumento = 4;
                                    oSeriesDTO.CodigoSede = item.CodigoSede;
                                    oSeriesDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oSeriesData.ActualizarSerieAumentar(oSeriesDTO);
                                    codigoTP = 1;
                                }
                                
                            }
                            
                                                          
                        }
                                         
                        tx.Complete();
                        oRespPedidosDTO.Success = true;
                        oRespPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPedidosDTO.Success = false;
                        oRespPedidosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPedidosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
