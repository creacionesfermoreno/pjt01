using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.IO;

namespace E_BusinessLayer.Gimnasio
{
	//-------------------------------------------------------------------
	//Archivo     : CategoriaLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 19/08/2014
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class CategoriaLogic: IDisposable
	{
		CategoriaData oCategoriaData = null;
		public CategoriaLogic()
		{
			oCategoriaData = new CategoriaData();
		}

        string ruta = @"D:\Licencia.txt";

		//-------------------------------------------------------------------
		//Nombre:	CategoriaGetList
		//Objetivo: Retorna una colección de registros de tipo CategoriaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListCategoriaDTO CategoriaGetList(ReqFilterCategoriaDTO oReqFilterCategoriaDTO)
		{
		
			RespListCategoriaDTO oRespListCategoriaDTO = new RespListCategoriaDTO();
		
			oRespListCategoriaDTO.List = new List<CategoriaDTO>();
			oRespListCategoriaDTO.User = oReqFilterCategoriaDTO.User;
			oRespListCategoriaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterCategoriaDTO.User))
            {
                oRespListCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Categoria no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterCategoriaDTO.Paging == null)
            {
                oRespListCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            //if (!(File.Exists(ruta)))
            //{
            //    oRespListCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
            //    {
            //        Codigo = 200,
            //        Detalle = "Alguien intenta Copiar el Sistema AdStore",
            //        Tipo = TipoMensaje.Error
            //    });
            //}
            //else
            //{
            //    using (StreamReader sr = new StreamReader(ruta))
            //    {
            //        string clave = ConfigurationManager.AppSettings["appClave"].ToString();
            //        String claveArchivo = sr.ReadToEnd();

            //        if (clave != claveArchivo)
            //        {
            //            oRespListCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
            //            {
            //                Codigo = 200,
            //                Detalle = "Alguien intenta Copiar el Sistema AdStore",
            //                Tipo = TipoMensaje.Error
            //            });
            //        }

            //    }
            //}
			
			if (oRespListCategoriaDTO.MessageList.Count == 0)
            {
                
                try
                {
                  
                    
                    if (!oReqFilterCategoriaDTO.Paging.All && oReqFilterCategoriaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterCategoriaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<CategoriaDTO> CategoriaDTOList = new List<CategoriaDTO>();

                    switch (oReqFilterCategoriaDTO.FilterCase)
                    {
                        default:
                            {
                                CategoriaDTOList = oCategoriaData.Listar(oReqFilterCategoriaDTO.Item);
                            }
                            break;
                    }

                    oRespListCategoriaDTO.List = CategoriaDTOList;
                    oRespListCategoriaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListCategoriaDTO.Success = false;
                    oRespListCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListCategoriaDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	CategoriaGetItem
		//Objetivo: Retorna un registro de tipo CategoriaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemCategoriaDTO CategoriaGetItem(ReqFilterCategoriaDTO oReqFilterCategoriaDTO)
		{
			RespItemCategoriaDTO oRespItemCategoriaDTO = new RespItemCategoriaDTO();

            oRespItemCategoriaDTO.Success = false;
            oRespItemCategoriaDTO.Item = null;
            oRespItemCategoriaDTO.User = oReqFilterCategoriaDTO.User;
            oRespItemCategoriaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterCategoriaDTO.User))
            {
                oRespItemCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Categoria no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

           //if (!(File.Exists(ruta)))
           //{
           //    oRespItemCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
           //    {
           //        Codigo = 200,
           //        Detalle = "Alguien intenta Copiar el Sistema AdStore",
           //        Tipo = TipoMensaje.Error
           //    });
           //}
           //else
           //{
           //    using (StreamReader sr = new StreamReader(ruta))
           //    {
           //        string clave = ConfigurationManager.AppSettings["appClave"].ToString();
           //        String claveArchivo = sr.ReadToEnd();

           //        if (clave != claveArchivo)
           //        {
           //            oRespItemCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
           //            {
           //                Codigo = 200,
           //                Detalle = "Alguien intenta Copiar el Sistema AdStore",
           //                Tipo = TipoMensaje.Error
           //            });
           //        }

           //    }
           //}

            if (oRespItemCategoriaDTO.MessageList.Count == 0)
            {
                CategoriaDTO oCategoriaDTO = null;
                try
                {
                    switch (oReqFilterCategoriaDTO.FilterCase)
                    {
                       
                        case filterCaseCategoria.BuscarPorCodigo:
                            {
                                oCategoriaDTO = new CategoriaDTO();
                                oCategoriaDTO = oCategoriaData.BuscarPorCodigoCategoria(oReqFilterCategoriaDTO.Item);
                            }
                            break;
                        default:
                            {
                                oCategoriaDTO = new CategoriaDTO();
                            }
                            break;
                    }

                    oRespItemCategoriaDTO.Item = new CategoriaDTO();
                    oRespItemCategoriaDTO.Item = oCategoriaDTO;
                    oRespItemCategoriaDTO.Success = true;
                    oRespItemCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemCategoriaDTO.Success = false;
                    oRespItemCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemCategoriaDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo CategoriaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespCategoriaDTO ExecuteTransac(ReqCategoriaDTO oReqCategoriaDTO)
		{
			RespCategoriaDTO oRespCategoriaDTO = new RespCategoriaDTO();

            oRespCategoriaDTO.MessageList = new List<Mensaje>();
            oRespCategoriaDTO.User = oReqCategoriaDTO.User;
            
            if (String.IsNullOrEmpty(oReqCategoriaDTO.User))
            {
                oRespCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Categoria no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

           

            if (oRespCategoriaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (CategoriaDTO item in oReqCategoriaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oCategoriaData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oCategoriaData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oCategoriaData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespCategoriaDTO.Success = true;
                        oRespCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespCategoriaDTO.Success = false;
                        oRespCategoriaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespCategoriaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
