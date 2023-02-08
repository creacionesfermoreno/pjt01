
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
	//Archivo     : HorarioClasesLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/03/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class HorarioClasesLogic: IDisposable
	{
		HorarioClasesData oHorarioClasesData = null;
		public HorarioClasesLogic()
		{
			oHorarioClasesData = new HorarioClasesData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioClasesGetList
		//Objetivo: Retorna una colección de registros de tipo HorarioClasesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListHorarioClasesDTO HorarioClasesGetList(ReqFilterHorarioClasesDTO oReqFilterHorarioClasesDTO)
		{
		
			RespListHorarioClasesDTO oRespListHorarioClasesDTO = new RespListHorarioClasesDTO();
		
			oRespListHorarioClasesDTO.List = new List<HorarioClasesDTO>();
			oRespListHorarioClasesDTO.User = oReqFilterHorarioClasesDTO.User;
			oRespListHorarioClasesDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterHorarioClasesDTO.User))
            {
                oRespListHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClases no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterHorarioClasesDTO.Paging == null)
            {
                oRespListHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListHorarioClasesDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterHorarioClasesDTO.Paging.All && oReqFilterHorarioClasesDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterHorarioClasesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<HorarioClasesDTO> HorarioClasesDTOList = new List<HorarioClasesDTO>();

                    switch (oReqFilterHorarioClasesDTO.FilterCase)
                    {
                        case filterCaseHorarioClases.ListarCalendarioDiario:
                            {
                                HorarioClasesDTOList = oHorarioClasesData.ListarCalendarioDelDia(oReqFilterHorarioClasesDTO.Item);
                            }
                            break;
                        case filterCaseHorarioClases.ListarTodos:
                            {
                                HorarioClasesDTOList = oHorarioClasesData.Listar(oReqFilterHorarioClasesDTO.Item);
                            }
                            break;
                        case filterCaseHorarioClases.ListarPorProfesional:
                            {
                                HorarioClasesDTOList = oHorarioClasesData.Listar(oReqFilterHorarioClasesDTO.Item);
                            }
                            break;
                        case filterCaseHorarioClases.ListarPorFecha:
                            {
                                HorarioClasesDTOList = oHorarioClasesData.ListarHorarioPorFecha(oReqFilterHorarioClasesDTO.Item);
                            }
                            break;
                    }

                    oRespListHorarioClasesDTO.List = HorarioClasesDTOList;
                    oRespListHorarioClasesDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListHorarioClasesDTO.Success = false;
                    oRespListHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListHorarioClasesDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioClasesGetItem
		//Objetivo: Retorna un registro de tipo HorarioClasesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemHorarioClasesDTO HorarioClasesGetItem(ReqFilterHorarioClasesDTO oReqFilterHorarioClasesDTO)
		{
			RespItemHorarioClasesDTO oRespItemHorarioClasesDTO = new RespItemHorarioClasesDTO();

            oRespItemHorarioClasesDTO.Success = false;
            oRespItemHorarioClasesDTO.Item = null;
            oRespItemHorarioClasesDTO.User = oReqFilterHorarioClasesDTO.User;
            oRespItemHorarioClasesDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterHorarioClasesDTO.User))
            {
                oRespItemHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClases no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemHorarioClasesDTO.MessageList.Count == 0)
            {
                HorarioClasesDTO oHorarioClasesDTO = null;
                try
                {
                    switch (oReqFilterHorarioClasesDTO.FilterCase)
                    {

                        case filterCaseHorarioClases.PorCodigo:
                            {
                                oHorarioClasesDTO = new HorarioClasesDTO();
                                oHorarioClasesDTO = oHorarioClasesData.BuscarPorCodigoHorarioClases(oReqFilterHorarioClasesDTO.Item);
                            }
                            break;
                        case filterCaseHorarioClases.BuscarPorCodigoConDetalle:
                            {
                                oHorarioClasesDTO = new HorarioClasesDTO();
                                oHorarioClasesDTO = oHorarioClasesData.BuscarPorCodigoHorarioClasesConDetalle(oReqFilterHorarioClasesDTO.Item);

                            }
                            break;
                        default:
                            {
                                oHorarioClasesDTO = new HorarioClasesDTO();
                            }
                            break;
                    }

                    oRespItemHorarioClasesDTO.Item = new HorarioClasesDTO();
                    oRespItemHorarioClasesDTO.Item = oHorarioClasesDTO;
                    oRespItemHorarioClasesDTO.Success = true;
                    oRespItemHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemHorarioClasesDTO.Success = false;
                    oRespItemHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemHorarioClasesDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo HorarioClasesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespHorarioClasesDTO ExecuteTransac(ReqHorarioClasesDTO oReqHorarioClasesDTO)
		{
			RespHorarioClasesDTO oRespHorarioClasesDTO = new RespHorarioClasesDTO();

            oRespHorarioClasesDTO.MessageList = new List<Mensaje>();
            oRespHorarioClasesDTO.User = oReqHorarioClasesDTO.User;
            
            if (String.IsNullOrEmpty(oReqHorarioClasesDTO.User))
            {
                oRespHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClases no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespHorarioClasesDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (HorarioClasesDTO item in oReqHorarioClasesDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oHorarioClasesData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oHorarioClasesData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oHorarioClasesData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespHorarioClasesDTO.Success = true;
                        oRespHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespHorarioClasesDTO.Success = false;
                        oRespHorarioClasesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespHorarioClasesDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
