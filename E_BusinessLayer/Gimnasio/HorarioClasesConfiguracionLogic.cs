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
	//Archivo     : HorarioClasesConfiguracionLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/03/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class HorarioClasesConfiguracionLogic: IDisposable
	{
		HorarioClasesConfiguracionData oHorarioClasesConfiguracionData = null;
		public HorarioClasesConfiguracionLogic()
		{
			oHorarioClasesConfiguracionData = new HorarioClasesConfiguracionData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioClasesConfiguracionGetList
		//Objetivo: Retorna una colección de registros de tipo HorarioClasesConfiguracionDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListHorarioClasesConfiguracionDTO HorarioClasesConfiguracionGetList(ReqFilterHorarioClasesConfiguracionDTO oReqFilterHorarioClasesConfiguracionDTO)
		{
		
			RespListHorarioClasesConfiguracionDTO oRespListHorarioClasesConfiguracionDTO = new RespListHorarioClasesConfiguracionDTO();
		
			oRespListHorarioClasesConfiguracionDTO.List = new List<HorarioClasesConfiguracionDTO>();
			oRespListHorarioClasesConfiguracionDTO.User = oReqFilterHorarioClasesConfiguracionDTO.User;
			oRespListHorarioClasesConfiguracionDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterHorarioClasesConfiguracionDTO.User))
            {
                oRespListHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClasesConfiguracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterHorarioClasesConfiguracionDTO.Paging == null)
            {
                oRespListHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListHorarioClasesConfiguracionDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterHorarioClasesConfiguracionDTO.Paging.All && oReqFilterHorarioClasesConfiguracionDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterHorarioClasesConfiguracionDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<HorarioClasesConfiguracionDTO> HorarioClasesConfiguracionDTOList = new List<HorarioClasesConfiguracionDTO>();

                    switch (oReqFilterHorarioClasesConfiguracionDTO.FilterCase)
                    {
                        case filterCaseHorarioClasesConfiguracion.PorCodigo:
                            {
                                HorarioClasesConfiguracionDTOList = oHorarioClasesConfiguracionData.ListarPorCodigoProfesional(oReqFilterHorarioClasesConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseHorarioClasesConfiguracion.Calendario:
                            {
                                HorarioClasesConfiguracionDTOList = oHorarioClasesConfiguracionData.ListarCalendario(oReqFilterHorarioClasesConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseHorarioClasesConfiguracion.uspListarHorarioClasesConfiguracionCalendario_ExportarExcel:
                            {
                                HorarioClasesConfiguracionDTOList = oHorarioClasesConfiguracionData.uspListarHorarioClasesConfiguracionCalendario_ExportarExcel(oReqFilterHorarioClasesConfiguracionDTO.Item);
                            }
                            break;
                        default: {
                                HorarioClasesConfiguracionDTOList = oHorarioClasesConfiguracionData.Listar(oReqFilterHorarioClasesConfiguracionDTO.Item);
                            }
                            break;
                    }

                    oRespListHorarioClasesConfiguracionDTO.List = HorarioClasesConfiguracionDTOList;
                    oRespListHorarioClasesConfiguracionDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListHorarioClasesConfiguracionDTO.Success = false;
                    oRespListHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListHorarioClasesConfiguracionDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioClasesConfiguracionGetItem
		//Objetivo: Retorna un registro de tipo HorarioClasesConfiguracionDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemHorarioClasesConfiguracionDTO HorarioClasesConfiguracionGetItem(ReqFilterHorarioClasesConfiguracionDTO oReqFilterHorarioClasesConfiguracionDTO)
		{
			RespItemHorarioClasesConfiguracionDTO oRespItemHorarioClasesConfiguracionDTO = new RespItemHorarioClasesConfiguracionDTO();

            oRespItemHorarioClasesConfiguracionDTO.Success = false;
            oRespItemHorarioClasesConfiguracionDTO.Item = null;
            oRespItemHorarioClasesConfiguracionDTO.User = oReqFilterHorarioClasesConfiguracionDTO.User;
            oRespItemHorarioClasesConfiguracionDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterHorarioClasesConfiguracionDTO.User))
            {
                oRespItemHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClasesConfiguracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemHorarioClasesConfiguracionDTO.MessageList.Count == 0)
            {
                HorarioClasesConfiguracionDTO oHorarioClasesConfiguracionDTO = null;
                try
                {
                    switch (oReqFilterHorarioClasesConfiguracionDTO.FilterCase)
                    {
                       
                        case filterCaseHorarioClasesConfiguracion.PorCodigo:
                            {
                                oHorarioClasesConfiguracionDTO = new HorarioClasesConfiguracionDTO();
                                oHorarioClasesConfiguracionDTO = oHorarioClasesConfiguracionData.BuscarPorCodigoHorarioClasesConfiguracion(oReqFilterHorarioClasesConfiguracionDTO.Item);
                            }
                            break;
                        default:
                            {
                                oHorarioClasesConfiguracionDTO = new HorarioClasesConfiguracionDTO();
                            }
                            break;
                    }

                    oRespItemHorarioClasesConfiguracionDTO.Item = new HorarioClasesConfiguracionDTO();
                    oRespItemHorarioClasesConfiguracionDTO.Item = oHorarioClasesConfiguracionDTO;
                    oRespItemHorarioClasesConfiguracionDTO.Success = true;
                    oRespItemHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemHorarioClasesConfiguracionDTO.Success = false;
                    oRespItemHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemHorarioClasesConfiguracionDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo HorarioClasesConfiguracionDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespHorarioClasesConfiguracionDTO ExecuteTransac(ReqHorarioClasesConfiguracionDTO oReqHorarioClasesConfiguracionDTO)
		{
			RespHorarioClasesConfiguracionDTO oRespHorarioClasesConfiguracionDTO = new RespHorarioClasesConfiguracionDTO();

            oRespHorarioClasesConfiguracionDTO.MessageList = new List<Mensaje>();
            oRespHorarioClasesConfiguracionDTO.User = oReqHorarioClasesConfiguracionDTO.User;
            
            if (String.IsNullOrEmpty(oReqHorarioClasesConfiguracionDTO.User))
            {
                oRespHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClasesConfiguracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
           
            if (oRespHorarioClasesConfiguracionDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (HorarioClasesConfiguracionDTO item in oReqHorarioClasesConfiguracionDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    if (oHorarioClasesConfiguracionData.uspValidadorHorarioClasesConfiguracion(item) > 0)
                                    {
                                        throw new Exception("Error ya existe un horario");
                                    }
                                    else {
                                        oHorarioClasesConfiguracionData.Registrar(item);
                                    }
                                    break;
                                case Operation.Update:
                                    oHorarioClasesConfiguracionData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oHorarioClasesConfiguracionData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespHorarioClasesConfiguracionDTO.Success = true;
                        oRespHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespHorarioClasesConfiguracionDTO.Success = false;
                        oRespHorarioClasesConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespHorarioClasesConfiguracionDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
