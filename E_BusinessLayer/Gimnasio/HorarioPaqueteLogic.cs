
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
	//Archivo     : HorarioPaqueteLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 07/11/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class HorarioPaqueteLogic: IDisposable
	{
		HorarioPaqueteData oHorarioPaqueteData = null;
		public HorarioPaqueteLogic()
		{
			oHorarioPaqueteData = new HorarioPaqueteData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioPaqueteGetList
		//Objetivo: Retorna una colección de registros de tipo HorarioPaqueteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListHorarioPaqueteDTO HorarioPaqueteGetList(ReqFilterHorarioPaqueteDTO oReqFilterHorarioPaqueteDTO)
		{
		
			RespListHorarioPaqueteDTO oRespListHorarioPaqueteDTO = new RespListHorarioPaqueteDTO();
		
			oRespListHorarioPaqueteDTO.List = new List<HorarioPaqueteDTO>();
			oRespListHorarioPaqueteDTO.User = oReqFilterHorarioPaqueteDTO.User;
			oRespListHorarioPaqueteDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterHorarioPaqueteDTO.User))
            {
                oRespListHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPaquete no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterHorarioPaqueteDTO.Paging == null)
            {
                oRespListHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListHorarioPaqueteDTO.MessageList.Count == 0)
            {
                
                try
                {
                    //uint recordCount = 0;
                    
                    if (!oReqFilterHorarioPaqueteDTO.Paging.All && oReqFilterHorarioPaqueteDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterHorarioPaqueteDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<HorarioPaqueteDTO> HorarioPaqueteDTOList = new List<HorarioPaqueteDTO>();

                    switch (oReqFilterHorarioPaqueteDTO.FilterCase)
                    {
                        case filterCaseHorarioPaquete.uspListarDiasHorarioPaquete_visualizar:
                            {
                                HorarioPaqueteDTOList = oHorarioPaqueteData.uspListarDiasHorarioPaquete_visualizar(oReqFilterHorarioPaqueteDTO.Item);
                            }
                        break;
                       
                    }

                    oRespListHorarioPaqueteDTO.List = HorarioPaqueteDTOList;
                    oRespListHorarioPaqueteDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListHorarioPaqueteDTO.Success = false;
                    oRespListHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListHorarioPaqueteDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioPaqueteGetItem
		//Objetivo: Retorna un registro de tipo HorarioPaqueteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemHorarioPaqueteDTO HorarioPaqueteGetItem(ReqFilterHorarioPaqueteDTO oReqFilterHorarioPaqueteDTO)
		{
			RespItemHorarioPaqueteDTO oRespItemHorarioPaqueteDTO = new RespItemHorarioPaqueteDTO();

            oRespItemHorarioPaqueteDTO.Success = false;
            oRespItemHorarioPaqueteDTO.Item = null;
            oRespItemHorarioPaqueteDTO.User = oReqFilterHorarioPaqueteDTO.User;
            oRespItemHorarioPaqueteDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterHorarioPaqueteDTO.User))
            {
                oRespItemHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPaquete no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemHorarioPaqueteDTO.MessageList.Count == 0)
            {
                HorarioPaqueteDTO oHorarioPaqueteDTO = null;
                try
                {
                    switch (oReqFilterHorarioPaqueteDTO.FilterCase)
                    {
                        default:
                            {
                                oHorarioPaqueteDTO = new HorarioPaqueteDTO();
                            }
                            break;
                    }

                    oRespItemHorarioPaqueteDTO.Item = new HorarioPaqueteDTO();
                    oRespItemHorarioPaqueteDTO.Item = oHorarioPaqueteDTO;
                    oRespItemHorarioPaqueteDTO.Success = true;
                    oRespItemHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemHorarioPaqueteDTO.Success = false;
                    oRespItemHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemHorarioPaqueteDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo HorarioPaqueteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespHorarioPaqueteDTO ExecuteTransac(ReqHorarioPaqueteDTO oReqHorarioPaqueteDTO)
		{
			RespHorarioPaqueteDTO oRespHorarioPaqueteDTO = new RespHorarioPaqueteDTO();

            oRespHorarioPaqueteDTO.MessageList = new List<Mensaje>();
            oRespHorarioPaqueteDTO.User = oReqHorarioPaqueteDTO.User;
            
            if (String.IsNullOrEmpty(oReqHorarioPaqueteDTO.User))
            {
                oRespHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPaquete no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespHorarioPaqueteDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (HorarioPaqueteDTO item in oReqHorarioPaqueteDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                   // oHorarioPaqueteData.Registrar(item);
                                    foreach (var item_1 in item.ListaDetalle_H)
                                    {
                                        HorarioPaqueteDTO NewPT = new HorarioPaqueteDTO();
                                        NewPT.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        NewPT.CodigoHP = 0;
                                        NewPT.CodigoPaquete = item.CodigoPaquete;
                                        NewPT.Dia = item_1.Dia;
                                        NewPT.UsuarioCreacion = item.UsuarioCreacion;
                                        NewPT.Estado = item_1.Estado; 
                                        oHorarioPaqueteData.Registrar(NewPT);
                                    }
                                    break;                             
                            }
                        }
                        tx.Complete();
                        oRespHorarioPaqueteDTO.Success = true;
                        oRespHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespHorarioPaqueteDTO.Success = false;
                        oRespHorarioPaqueteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespHorarioPaqueteDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public int verificarExiteDiaSemanaCurso(int CodigoUnidadNegocio,int CodigoPaquete, int dia)
        {

            int flag = 0;
            flag = oHorarioPaqueteData.verificarExiteDiaSemanaCurso(CodigoUnidadNegocio,CodigoPaquete, dia);
            return flag;
        }

	}
}
