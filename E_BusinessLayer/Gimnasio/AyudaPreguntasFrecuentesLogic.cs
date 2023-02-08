
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
	//Archivo     : AyudaPreguntasFrecuentesLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 19/03/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class AyudaPreguntasFrecuentesLogic: IDisposable
	{
		AyudaPreguntasFrecuentesData oAyudaPreguntasFrecuentesData = null;
		public AyudaPreguntasFrecuentesLogic()
		{
			oAyudaPreguntasFrecuentesData = new AyudaPreguntasFrecuentesData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	AyudaPreguntasFrecuentesGetList
		//Objetivo: Retorna una colección de registros de tipo AyudaPreguntasFrecuentesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListAyudaPreguntasFrecuentesDTO AyudaPreguntasFrecuentesGetList(ReqFilterAyudaPreguntasFrecuentesDTO oReqFilterAyudaPreguntasFrecuentesDTO)
		{
		
			RespListAyudaPreguntasFrecuentesDTO oRespListAyudaPreguntasFrecuentesDTO = new RespListAyudaPreguntasFrecuentesDTO();
		
			oRespListAyudaPreguntasFrecuentesDTO.List = new List<AyudaPreguntasFrecuentesDTO>();
			oRespListAyudaPreguntasFrecuentesDTO.User = oReqFilterAyudaPreguntasFrecuentesDTO.User;
			oRespListAyudaPreguntasFrecuentesDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterAyudaPreguntasFrecuentesDTO.User))
            {
                oRespListAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AyudaPreguntasFrecuentes no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterAyudaPreguntasFrecuentesDTO.Paging == null)
            {
                oRespListAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListAyudaPreguntasFrecuentesDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterAyudaPreguntasFrecuentesDTO.Paging.All && oReqFilterAyudaPreguntasFrecuentesDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterAyudaPreguntasFrecuentesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<AyudaPreguntasFrecuentesDTO> AyudaPreguntasFrecuentesDTOList = new List<AyudaPreguntasFrecuentesDTO>();
                    switch (oReqFilterAyudaPreguntasFrecuentesDTO.FilterCase)
                    {
                        case filterCaseAyudaPreguntasFrecuentes.uspListarAyudaPreguntasFrecuentes:
                            AyudaPreguntasFrecuentesDTOList = oAyudaPreguntasFrecuentesData.uspListarAyudaPreguntasFrecuentes();
                            break;
                        default:
                            break;
                    }

                    oRespListAyudaPreguntasFrecuentesDTO.List = AyudaPreguntasFrecuentesDTOList;
                    oRespListAyudaPreguntasFrecuentesDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListAyudaPreguntasFrecuentesDTO.Success = false;
                    oRespListAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListAyudaPreguntasFrecuentesDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	AyudaPreguntasFrecuentesGetItem
		//Objetivo: Retorna un registro de tipo AyudaPreguntasFrecuentesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemAyudaPreguntasFrecuentesDTO AyudaPreguntasFrecuentesGetItem(ReqFilterAyudaPreguntasFrecuentesDTO oReqFilterAyudaPreguntasFrecuentesDTO)
		{
			RespItemAyudaPreguntasFrecuentesDTO oRespItemAyudaPreguntasFrecuentesDTO = new RespItemAyudaPreguntasFrecuentesDTO();

            oRespItemAyudaPreguntasFrecuentesDTO.Success = false;
            oRespItemAyudaPreguntasFrecuentesDTO.Item = null;
            oRespItemAyudaPreguntasFrecuentesDTO.User = oReqFilterAyudaPreguntasFrecuentesDTO.User;
            oRespItemAyudaPreguntasFrecuentesDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterAyudaPreguntasFrecuentesDTO.User))
            {
                oRespItemAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AyudaPreguntasFrecuentes no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemAyudaPreguntasFrecuentesDTO.MessageList.Count == 0)
            {
                AyudaPreguntasFrecuentesDTO oAyudaPreguntasFrecuentesDTO = null;
                try
                {
                    switch (oReqFilterAyudaPreguntasFrecuentesDTO.FilterCase)
                    {
                        default:
                            {
                                oAyudaPreguntasFrecuentesDTO = new AyudaPreguntasFrecuentesDTO();
                            }
                            break;
                    }

                    oRespItemAyudaPreguntasFrecuentesDTO.Item = new AyudaPreguntasFrecuentesDTO();
                    oRespItemAyudaPreguntasFrecuentesDTO.Item = oAyudaPreguntasFrecuentesDTO;
                    oRespItemAyudaPreguntasFrecuentesDTO.Success = true;
                    oRespItemAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemAyudaPreguntasFrecuentesDTO.Success = false;
                    oRespItemAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemAyudaPreguntasFrecuentesDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo AyudaPreguntasFrecuentesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespAyudaPreguntasFrecuentesDTO ExecuteTransac(ReqAyudaPreguntasFrecuentesDTO oReqAyudaPreguntasFrecuentesDTO)
		{
			RespAyudaPreguntasFrecuentesDTO oRespAyudaPreguntasFrecuentesDTO = new RespAyudaPreguntasFrecuentesDTO();

            oRespAyudaPreguntasFrecuentesDTO.MessageList = new List<Mensaje>();
            oRespAyudaPreguntasFrecuentesDTO.User = oReqAyudaPreguntasFrecuentesDTO.User;
            
            if (String.IsNullOrEmpty(oReqAyudaPreguntasFrecuentesDTO.User))
            {
                oRespAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AyudaPreguntasFrecuentes no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespAyudaPreguntasFrecuentesDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (AyudaPreguntasFrecuentesDTO item in oReqAyudaPreguntasFrecuentesDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oAyudaPreguntasFrecuentesData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oAyudaPreguntasFrecuentesData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oAyudaPreguntasFrecuentesData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespAyudaPreguntasFrecuentesDTO.Success = true;
                        oRespAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespAyudaPreguntasFrecuentesDTO.Success = false;
                        oRespAyudaPreguntasFrecuentesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespAyudaPreguntasFrecuentesDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
