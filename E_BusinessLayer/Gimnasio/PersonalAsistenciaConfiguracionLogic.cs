
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
	//Archivo     : PersonalAsistenciaConfiguracionLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 31/05/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class PersonalAsistenciaConfiguracionLogic: IDisposable
	{
		PersonalAsistenciaConfiguracionData oPersonalAsistenciaConfiguracionData = null;
		public PersonalAsistenciaConfiguracionLogic()
		{
			oPersonalAsistenciaConfiguracionData = new PersonalAsistenciaConfiguracionData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PersonalAsistenciaConfiguracionGetList
		//Objetivo: Retorna una colección de registros de tipo PersonalAsistenciaConfiguracionDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListPersonalAsistenciaConfiguracionDTO PersonalAsistenciaConfiguracionGetList(ReqFilterPersonalAsistenciaConfiguracionDTO oReqFilterPersonalAsistenciaConfiguracionDTO)
		{
		
			RespListPersonalAsistenciaConfiguracionDTO oRespListPersonalAsistenciaConfiguracionDTO = new RespListPersonalAsistenciaConfiguracionDTO();
		
			oRespListPersonalAsistenciaConfiguracionDTO.List = new List<PersonalAsistenciaConfiguracionDTO>();
			oRespListPersonalAsistenciaConfiguracionDTO.User = oReqFilterPersonalAsistenciaConfiguracionDTO.User;
			oRespListPersonalAsistenciaConfiguracionDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPersonalAsistenciaConfiguracionDTO.User))
            {
                oRespListPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistenciaConfiguracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPersonalAsistenciaConfiguracionDTO.Paging == null)
            {
                oRespListPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPersonalAsistenciaConfiguracionDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterPersonalAsistenciaConfiguracionDTO.Paging.All && oReqFilterPersonalAsistenciaConfiguracionDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPersonalAsistenciaConfiguracionDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<PersonalAsistenciaConfiguracionDTO> PersonalAsistenciaConfiguracionDTOList = new List<PersonalAsistenciaConfiguracionDTO>();

                    switch (oReqFilterPersonalAsistenciaConfiguracionDTO.FilterCase)
                    {
                        default:
                            {
                                PersonalAsistenciaConfiguracionDTOList = oPersonalAsistenciaConfiguracionData.Listar(oReqFilterPersonalAsistenciaConfiguracionDTO.Item, oReqFilterPersonalAsistenciaConfiguracionDTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListPersonalAsistenciaConfiguracionDTO.List = PersonalAsistenciaConfiguracionDTOList;
                    oRespListPersonalAsistenciaConfiguracionDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPersonalAsistenciaConfiguracionDTO.Success = false;
                    oRespListPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListPersonalAsistenciaConfiguracionDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PersonalAsistenciaConfiguracionGetItem
		//Objetivo: Retorna un registro de tipo PersonalAsistenciaConfiguracionDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemPersonalAsistenciaConfiguracionDTO PersonalAsistenciaConfiguracionGetItem(ReqFilterPersonalAsistenciaConfiguracionDTO oReqFilterPersonalAsistenciaConfiguracionDTO)
		{
			RespItemPersonalAsistenciaConfiguracionDTO oRespItemPersonalAsistenciaConfiguracionDTO = new RespItemPersonalAsistenciaConfiguracionDTO();

            oRespItemPersonalAsistenciaConfiguracionDTO.Success = false;
            oRespItemPersonalAsistenciaConfiguracionDTO.Item = null;
            oRespItemPersonalAsistenciaConfiguracionDTO.User = oReqFilterPersonalAsistenciaConfiguracionDTO.User;
            oRespItemPersonalAsistenciaConfiguracionDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPersonalAsistenciaConfiguracionDTO.User))
            {
                oRespItemPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistenciaConfiguracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPersonalAsistenciaConfiguracionDTO.MessageList.Count == 0)
            {
                PersonalAsistenciaConfiguracionDTO oPersonalAsistenciaConfiguracionDTO = null;
                try
                {
                    switch (oReqFilterPersonalAsistenciaConfiguracionDTO.FilterCase)
                    {

                        case filterCasePersonalAsistenciaConfiguracion.BuscarPorCodigo:
                            {
                                oPersonalAsistenciaConfiguracionDTO = new PersonalAsistenciaConfiguracionDTO();
                                oPersonalAsistenciaConfiguracionDTO = oPersonalAsistenciaConfiguracionData.BuscarPorCodigoPersonalAsistenciaConfiguracion(oReqFilterPersonalAsistenciaConfiguracionDTO.Item);
                            }
                            break;
                        default:
                            {
                                oPersonalAsistenciaConfiguracionDTO = new PersonalAsistenciaConfiguracionDTO();
                            }
                            break;
                    }

                    oRespItemPersonalAsistenciaConfiguracionDTO.Item = new PersonalAsistenciaConfiguracionDTO();
                    oRespItemPersonalAsistenciaConfiguracionDTO.Item = oPersonalAsistenciaConfiguracionDTO;
                    oRespItemPersonalAsistenciaConfiguracionDTO.Success = true;
                    oRespItemPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPersonalAsistenciaConfiguracionDTO.Success = false;
                    oRespItemPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPersonalAsistenciaConfiguracionDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo PersonalAsistenciaConfiguracionDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespPersonalAsistenciaConfiguracionDTO ExecuteTransac(ReqPersonalAsistenciaConfiguracionDTO oReqPersonalAsistenciaConfiguracionDTO)
		{
			RespPersonalAsistenciaConfiguracionDTO oRespPersonalAsistenciaConfiguracionDTO = new RespPersonalAsistenciaConfiguracionDTO();

            oRespPersonalAsistenciaConfiguracionDTO.MessageList = new List<Mensaje>();
            oRespPersonalAsistenciaConfiguracionDTO.User = oReqPersonalAsistenciaConfiguracionDTO.User;
            
            if (String.IsNullOrEmpty(oReqPersonalAsistenciaConfiguracionDTO.User))
            {
                oRespPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistenciaConfiguracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPersonalAsistenciaConfiguracionDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (PersonalAsistenciaConfiguracionDTO item in oReqPersonalAsistenciaConfiguracionDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oPersonalAsistenciaConfiguracionData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oPersonalAsistenciaConfiguracionData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oPersonalAsistenciaConfiguracionData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespPersonalAsistenciaConfiguracionDTO.Success = true;
                        oRespPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPersonalAsistenciaConfiguracionDTO.Success = false;
                        oRespPersonalAsistenciaConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPersonalAsistenciaConfiguracionDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
