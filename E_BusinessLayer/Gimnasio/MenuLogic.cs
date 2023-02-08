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
	
	public class MenuLogic: IDisposable
	{
		MenuData oMenuData = null;
		public MenuLogic()
		{
			oMenuData = new MenuData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	MenuGetList
		//Objetivo: Retorna una colección de registros de tipo MenuDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListMenuDTO MenuGetList(ReqFilterMenuDTO oReqFilterMenuDTO)
		{
		
			RespListMenuDTO oRespListMenuDTO = new RespListMenuDTO();
		
			oRespListMenuDTO.List = new List<MenuDTO>();
			oRespListMenuDTO.User = oReqFilterMenuDTO.User;
			oRespListMenuDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterMenuDTO.User))
            {
                oRespListMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Menu no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterMenuDTO.Paging == null)
            {
                oRespListMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListMenuDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterMenuDTO.Paging.All && oReqFilterMenuDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterMenuDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<MenuDTO> MenuDTOList = new List<MenuDTO>();

                    switch (oReqFilterMenuDTO.FilterCase)
                    {
                       
                        case filterCaseMenu.ListarSEG_PermisosPorCodigoMenuSuperior:
                            {
                                MenuDTOList = oMenuData.ListarSEG_PermisosPorCodigoMenuSuperior(oReqFilterMenuDTO.Item);
                            }
                            break;
                        default:
                            {
                                MenuDTOList = oMenuData.Listar();
                            }
                            break;
                    }

                    oRespListMenuDTO.List = MenuDTOList;
                    oRespListMenuDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListMenuDTO.Success = false;
                    oRespListMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListMenuDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	MenuGetItem
		//Objetivo: Retorna un registro de tipo MenuDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemMenuDTO MenuGetItem(ReqFilterMenuDTO oReqFilterMenuDTO)
		{
			RespItemMenuDTO oRespItemMenuDTO = new RespItemMenuDTO();

            oRespItemMenuDTO.Success = false;
            oRespItemMenuDTO.Item = null;
            oRespItemMenuDTO.User = oReqFilterMenuDTO.User;
            oRespItemMenuDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterMenuDTO.User))
            {
                oRespItemMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Menu no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemMenuDTO.MessageList.Count == 0)
            {
                MenuDTO oMenuDTO = null;
                try
                {
                    switch (oReqFilterMenuDTO.FilterCase)
                    {
                       
                        case filterCaseMenu.porCodigo:
                            {
                                oMenuDTO = new MenuDTO();
                                oMenuDTO = oMenuData.BuscarPorCodigoMenu(oReqFilterMenuDTO.Item);
                            }
                            break;
                        default:
                            {
                                oMenuDTO = new MenuDTO();
                            }
                            break;
                    }

                    oRespItemMenuDTO.Item = new MenuDTO();
                    oRespItemMenuDTO.Item = oMenuDTO;
                    oRespItemMenuDTO.Success = true;
                    oRespItemMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemMenuDTO.Success = false;
                    oRespItemMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemMenuDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo MenuDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespMenuDTO ExecuteTransac(ReqMenuDTO oReqMenuDTO)
		{
			RespMenuDTO oRespMenuDTO = new RespMenuDTO();

            oRespMenuDTO.MessageList = new List<Mensaje>();
            oRespMenuDTO.User = oReqMenuDTO.User;
            
            if (String.IsNullOrEmpty(oReqMenuDTO.User))
            {
                oRespMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Menu no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespMenuDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (MenuDTO item in oReqMenuDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oMenuData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oMenuData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oMenuData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespMenuDTO.Success = true;
                        oRespMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespMenuDTO.Success = false;
                        oRespMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespMenuDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
