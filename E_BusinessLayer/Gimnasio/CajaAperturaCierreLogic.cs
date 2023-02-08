
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
	//Archivo     : CajaAperturaCierreLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 12/10/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class CajaAperturaCierreLogic: IDisposable
	{
		CajaAperturaCierreData oCajaAperturaCierreData = null;
		public CajaAperturaCierreLogic()
		{
			oCajaAperturaCierreData = new CajaAperturaCierreData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	CajaAperturaCierreGetList
		//Objetivo: Retorna una colección de registros de tipo CajaAperturaCierreDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListCajaAperturaCierreDTO CajaAperturaCierreGetList(ReqFilterCajaAperturaCierreDTO oReqFilterCajaAperturaCierreDTO)
		{
		
			RespListCajaAperturaCierreDTO oRespListCajaAperturaCierreDTO = new RespListCajaAperturaCierreDTO();
		
			oRespListCajaAperturaCierreDTO.List = new List<CajaAperturaCierreDTO>();
			oRespListCajaAperturaCierreDTO.User = oReqFilterCajaAperturaCierreDTO.User;
			oRespListCajaAperturaCierreDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterCajaAperturaCierreDTO.User))
            {
                oRespListCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de CajaAperturaCierre no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterCajaAperturaCierreDTO.Paging == null)
            {
                oRespListCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListCajaAperturaCierreDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterCajaAperturaCierreDTO.Paging.All && oReqFilterCajaAperturaCierreDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterCajaAperturaCierreDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<CajaAperturaCierreDTO> CajaAperturaCierreDTOList = new List<CajaAperturaCierreDTO>();

                    switch (oReqFilterCajaAperturaCierreDTO.FilterCase)
                    {
                        case filterCaseCajaAperturaCierre.uspListarAperturaCaja_Paginacion:
                            if (!oReqFilterCajaAperturaCierreDTO.Paging.All && oReqFilterCajaAperturaCierreDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterCajaAperturaCierreDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarAperturaCaja_Paginacion"]);
                            }
                            CajaAperturaCierreDTOList = oCajaAperturaCierreData.uspListarAperturaCaja_Paginacion(oReqFilterCajaAperturaCierreDTO.Item, oReqFilterCajaAperturaCierreDTO.Paging);
                        break;
                    }

                    oRespListCajaAperturaCierreDTO.List = CajaAperturaCierreDTOList;
                    oRespListCajaAperturaCierreDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListCajaAperturaCierreDTO.Success = false;
                    oRespListCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListCajaAperturaCierreDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	CajaAperturaCierreGetItem
		//Objetivo: Retorna un registro de tipo CajaAperturaCierreDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemCajaAperturaCierreDTO CajaAperturaCierreGetItem(ReqFilterCajaAperturaCierreDTO oReqFilterCajaAperturaCierreDTO)
		{
			RespItemCajaAperturaCierreDTO oRespItemCajaAperturaCierreDTO = new RespItemCajaAperturaCierreDTO();

            oRespItemCajaAperturaCierreDTO.Success = false;
            oRespItemCajaAperturaCierreDTO.Item = null;
            oRespItemCajaAperturaCierreDTO.User = oReqFilterCajaAperturaCierreDTO.User;
            oRespItemCajaAperturaCierreDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterCajaAperturaCierreDTO.User))
            {
                oRespItemCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de CajaAperturaCierre no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemCajaAperturaCierreDTO.MessageList.Count == 0)
            {
                CajaAperturaCierreDTO oCajaAperturaCierreDTO = null;
                try
                {
                    switch (oReqFilterCajaAperturaCierreDTO.FilterCase)
                    {


                        case filterCaseCajaAperturaCierre.uspBuscarAperturaCaja:
                            {
                                oCajaAperturaCierreDTO = new CajaAperturaCierreDTO();
                                oCajaAperturaCierreDTO = oCajaAperturaCierreData.uspBuscarAperturaCaja(oReqFilterCajaAperturaCierreDTO.Item);
                            }
                            break;
                         case filterCaseCajaAperturaCierre.uspInformacionGeneralAbrirCaja:
                            {
                                oCajaAperturaCierreDTO = new CajaAperturaCierreDTO();
                                oCajaAperturaCierreDTO = oCajaAperturaCierreData.uspInformacionGeneralAbrirCaja(oReqFilterCajaAperturaCierreDTO.Item);
                            }
                            break;
                        case filterCaseCajaAperturaCierre.uspInformacionGeneralAbrirCaja_otrasformaspago:
                            {
                                oCajaAperturaCierreDTO = new CajaAperturaCierreDTO();
                                oCajaAperturaCierreDTO = oCajaAperturaCierreData.uspInformacionGeneralAbrirCaja_otrasformaspago(oReqFilterCajaAperturaCierreDTO.Item);
                            }
                            break;
                        case filterCaseCajaAperturaCierre.uspListarAperturaCaja_NumeroRegistros:
                            {
                                oCajaAperturaCierreDTO = new CajaAperturaCierreDTO();
                                oCajaAperturaCierreDTO = oCajaAperturaCierreData.uspListarAperturaCaja_NumeroRegistros(oReqFilterCajaAperturaCierreDTO.Item);
                            }
                            break;
                    }

                    oRespItemCajaAperturaCierreDTO.Item = new CajaAperturaCierreDTO();
                    oRespItemCajaAperturaCierreDTO.Item = oCajaAperturaCierreDTO;
                    oRespItemCajaAperturaCierreDTO.Success = true;
                    oRespItemCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemCajaAperturaCierreDTO.Success = false;
                    oRespItemCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemCajaAperturaCierreDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo CajaAperturaCierreDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespCajaAperturaCierreDTO ExecuteTransac(ReqCajaAperturaCierreDTO oReqCajaAperturaCierreDTO)
		{
			RespCajaAperturaCierreDTO oRespCajaAperturaCierreDTO = new RespCajaAperturaCierreDTO();

            oRespCajaAperturaCierreDTO.MessageList = new List<Mensaje>();
            oRespCajaAperturaCierreDTO.User = oReqCajaAperturaCierreDTO.User;
            
            if (String.IsNullOrEmpty(oReqCajaAperturaCierreDTO.User))
            {
                oRespCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de CajaAperturaCierre no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespCajaAperturaCierreDTO.MessageList.Count == 0)
            {
                string Mensaje = "";
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (CajaAperturaCierreDTO item in oReqCajaAperturaCierreDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.uspRegistrarAbrirCaja:
                                    oCajaAperturaCierreData.uspRegistrarAbrirCaja(item);
                                    break;

                                case Operation.UpdateAbrirCaja:
                                    oCajaAperturaCierreData.UpdateAbrirCaja(item);
                                    break;

                            }
                        }
                        tx.Complete();
                        oRespCajaAperturaCierreDTO.Success = true;
                        oRespCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = Mensaje,
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespCajaAperturaCierreDTO.Success = false;
                        oRespCajaAperturaCierreDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespCajaAperturaCierreDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
