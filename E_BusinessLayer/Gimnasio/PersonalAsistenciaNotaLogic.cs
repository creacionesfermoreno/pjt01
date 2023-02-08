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
	//Archivo     : PersonalAsistenciaNotaLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : bleondata@outlook.com - Beltran Leon Champi cel: 
	//Fecha       : 31/05/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class PersonalAsistenciaNotaLogic: IDisposable
	{
		PersonalAsistenciaNotaData oPersonalAsistenciaNotaData = null;
		public PersonalAsistenciaNotaLogic()
		{
			oPersonalAsistenciaNotaData = new PersonalAsistenciaNotaData();
		}

        //-------------------------------------------------------------------
        //Nombre:	PersonalAsistenciaNotaGetList
        //Objetivo: Retorna una colección de registros de tipo PersonalAsistenciaNotaDTO
        //Valores Prueba:
        //Creacion: bleondata@outlook.com - Beltran Leon Champi cel: 
        //Modificacion: bleondata@outlook.com - Beltran Leon Champi cel: 
        //-------------------------------------------------------------------
        public RespListPersonalAsistenciaNotaDTO PersonalAsistenciaNotaGetList(ReqFilterPersonalAsistenciaNotaDTO oReqFilterPersonalAsistenciaNotaDTO)
		{
		
			RespListPersonalAsistenciaNotaDTO oRespListPersonalAsistenciaNotaDTO = new RespListPersonalAsistenciaNotaDTO();
		
			oRespListPersonalAsistenciaNotaDTO.List = new List<PersonalAsistenciaNotaDTO>();
			oRespListPersonalAsistenciaNotaDTO.User = oReqFilterPersonalAsistenciaNotaDTO.User;
			oRespListPersonalAsistenciaNotaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPersonalAsistenciaNotaDTO.User))
            {
                oRespListPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistenciaNota no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPersonalAsistenciaNotaDTO.Paging == null)
            {
                oRespListPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPersonalAsistenciaNotaDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterPersonalAsistenciaNotaDTO.Paging.All && oReqFilterPersonalAsistenciaNotaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPersonalAsistenciaNotaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<PersonalAsistenciaNotaDTO> PersonalAsistenciaNotaDTOList = new List<PersonalAsistenciaNotaDTO>();

                    switch (oReqFilterPersonalAsistenciaNotaDTO.FilterCase)
                    {
                        default:
                            {
                                PersonalAsistenciaNotaDTOList = oPersonalAsistenciaNotaData.Listar(oReqFilterPersonalAsistenciaNotaDTO.Item, oReqFilterPersonalAsistenciaNotaDTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListPersonalAsistenciaNotaDTO.List = PersonalAsistenciaNotaDTOList;
                    oRespListPersonalAsistenciaNotaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPersonalAsistenciaNotaDTO.Success = false;
                    oRespListPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }
            return oRespListPersonalAsistenciaNotaDTO;
        }

        //-------------------------------------------------------------------
        //Nombre:	PersonalAsistenciaNotaGetItem
        //Objetivo: Retorna un registro de tipo PersonalAsistenciaNotaDTO
        //Valores Prueba:
        //Creacion: bleondata@outlook.com - Beltran Leon Champi cel: 
        //Modificacion: bleondata@outlook.com - Beltran Leon Champi cel: 
        //-------------------------------------------------------------------
        public RespItemPersonalAsistenciaNotaDTO PersonalAsistenciaNotaGetItem(ReqFilterPersonalAsistenciaNotaDTO oReqFilterPersonalAsistenciaNotaDTO)
		{
			RespItemPersonalAsistenciaNotaDTO oRespItemPersonalAsistenciaNotaDTO = new RespItemPersonalAsistenciaNotaDTO();

            oRespItemPersonalAsistenciaNotaDTO.Success = false;
            oRespItemPersonalAsistenciaNotaDTO.Item = null;
            oRespItemPersonalAsistenciaNotaDTO.User = oReqFilterPersonalAsistenciaNotaDTO.User;
            oRespItemPersonalAsistenciaNotaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPersonalAsistenciaNotaDTO.User))
            {
                oRespItemPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistenciaNota no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPersonalAsistenciaNotaDTO.MessageList.Count == 0)
            {
                PersonalAsistenciaNotaDTO oPersonalAsistenciaNotaDTO = null;
                try
                {
                    switch (oReqFilterPersonalAsistenciaNotaDTO.FilterCase)
                    {

                        case filterCasePersonalAsistenciaNota.BuscarPorCodigo:
                            {
                                oPersonalAsistenciaNotaDTO = new PersonalAsistenciaNotaDTO();
                                oPersonalAsistenciaNotaDTO = oPersonalAsistenciaNotaData.BuscarPorCodigoPersonalAsistenciaNota(oReqFilterPersonalAsistenciaNotaDTO.Item);
                            }
                            break;
                        default:
                            {
                                oPersonalAsistenciaNotaDTO = new PersonalAsistenciaNotaDTO();
                            }
                            break;
                    }

                    oRespItemPersonalAsistenciaNotaDTO.Item = new PersonalAsistenciaNotaDTO();
                    oRespItemPersonalAsistenciaNotaDTO.Item = oPersonalAsistenciaNotaDTO;
                    oRespItemPersonalAsistenciaNotaDTO.Success = true;
                    oRespItemPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPersonalAsistenciaNotaDTO.Success = false;
                    oRespItemPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPersonalAsistenciaNotaDTO;
		}

        //-------------------------------------------------------------------
        //Nombre:	ExecuteTransac
        //Objetivo: Almacena el registro de un objeto de tipo PersonalAsistenciaNotaDTO
        //Valores Prueba:
        //Creacion: bleondata@outlook.com - Beltran Leon Champi cel: 
        //Modificacion: bleondata@outlook.com - Beltran Leon Champi cel: 
        //-------------------------------------------------------------------
        public RespPersonalAsistenciaNotaDTO ExecuteTransac(ReqPersonalAsistenciaNotaDTO oReqPersonalAsistenciaNotaDTO)
		{
			RespPersonalAsistenciaNotaDTO oRespPersonalAsistenciaNotaDTO = new RespPersonalAsistenciaNotaDTO();

            oRespPersonalAsistenciaNotaDTO.MessageList = new List<Mensaje>();
            oRespPersonalAsistenciaNotaDTO.User = oReqPersonalAsistenciaNotaDTO.User;
            
            if (String.IsNullOrEmpty(oReqPersonalAsistenciaNotaDTO.User))
            {
                oRespPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistenciaNota no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPersonalAsistenciaNotaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (PersonalAsistenciaNotaDTO item in oReqPersonalAsistenciaNotaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oPersonalAsistenciaNotaData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oPersonalAsistenciaNotaData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oPersonalAsistenciaNotaData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespPersonalAsistenciaNotaDTO.Success = true;
                        oRespPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPersonalAsistenciaNotaDTO.Success = false;
                        oRespPersonalAsistenciaNotaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPersonalAsistenciaNotaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
