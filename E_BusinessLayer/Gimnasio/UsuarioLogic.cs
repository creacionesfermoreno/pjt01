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
	//Archivo     : UsuarioLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 11/25/2014
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class UsuarioLogic: IDisposable
	{
		UsuarioData oUsuarioData = null;
		public UsuarioLogic()
		{
			oUsuarioData = new UsuarioData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	UsuarioGetList
		//Objetivo: Retorna una colección de registros de tipo UsuarioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListUsuarioDTO UsuarioGetList(ReqFilterUsuarioDTO oReqFilterUsuarioDTO)
		{		
			RespListUsuarioDTO oRespListUsuarioDTO = new RespListUsuarioDTO();
		
			oRespListUsuarioDTO.List = new List<UsuarioDTO>();
			oRespListUsuarioDTO.User = oReqFilterUsuarioDTO.User;
			oRespListUsuarioDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterUsuarioDTO.User))
            {
                oRespListUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Usuario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterUsuarioDTO.Paging == null)
            {
                oRespListUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListUsuarioDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    
                    if (!oReqFilterUsuarioDTO.Paging.All && oReqFilterUsuarioDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterUsuarioDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<UsuarioDTO> UsuarioDTOList = new List<UsuarioDTO>();

                    switch (oReqFilterUsuarioDTO.FilterCase)
                    {
                       
                        case filterCaseUsuario.ListarVendedoresMigracion:
                            UsuarioDTOList = oUsuarioData.ListarVendedoresMigraciones(oReqFilterUsuarioDTO.Item);
                            break;
                        case filterCaseUsuario.ListarPerfilesFiltro:
                            UsuarioDTOList = oUsuarioData.ListarPerfilesFiltro(oReqFilterUsuarioDTO.Item);
                            break;
                        
                        case filterCaseUsuario.ListarUsuariosConFiltroVenta:
                            UsuarioDTOList = oUsuarioData.ListarUsuariosConFiltroVenta(oReqFilterUsuarioDTO.Item);
                            break;
                       
                        case filterCaseUsuario.Filter_uspListarColaboradorHuellero:
                            UsuarioDTOList = oUsuarioData.Listar(oReqFilterUsuarioDTO.Item);
                            break;

                      
                        case filterCaseUsuario.ListarAsesoresVentasAcuentaVentas:
                            UsuarioDTOList = oUsuarioData.ListarAsesoresVentasAcuentaVentas(oReqFilterUsuarioDTO.Item);
                            break;
                        case filterCaseUsuario.usplistardllCreadoPor:
                            UsuarioDTOList = oUsuarioData.usplistardllCreadoPor(oReqFilterUsuarioDTO.Item);
                            break;

                        case filterCaseUsuario.SEGListarUsuario_AgendaComercial:
                            UsuarioDTOList = oUsuarioData.SEGListarUsuario_AgendaComercial(oReqFilterUsuarioDTO.Item);
                            break;

                        case filterCaseUsuario.SEGListarUsuario_HacerContrato:
                            UsuarioDTOList = oUsuarioData.SEGListarUsuario_HacerContrato(oReqFilterUsuarioDTO.Item);
                            break;
                        case filterCaseUsuario.SEGListarUsuario_TrainnerActivos:
                            UsuarioDTOList = oUsuarioData.SEGListarUsuario_TrainnerActivos(oReqFilterUsuarioDTO.Item);
                            break;
                        case filterCaseUsuario.SEGListarUsuario_NutricionistasActivos:
                            UsuarioDTOList = oUsuarioData.SEGListarUsuario_NutricionistasActivos(oReqFilterUsuarioDTO.Item);
                            break;
                        case filterCaseUsuario.SEGListarUsuarioPorPerfil:
                            UsuarioDTOList = oUsuarioData.SEGListarUsuarioPorPerfil(oReqFilterUsuarioDTO.Item);
                            break;
                        case filterCaseUsuario.SEGListarUsuarioResponsableSuplementos:
                            UsuarioDTOList = oUsuarioData.SEGListarUsuarioResponsableSuplementos(oReqFilterUsuarioDTO.Item);
                            break;

                        case filterCaseUsuario.SEGListarUsuarioVendedorPrimerDiaMesConfiguracionMetas:
                            UsuarioDTOList = oUsuarioData.SEGListarUsuarioVendedorPrimerDiaMesConfiguracionMetas(oReqFilterUsuarioDTO.Item);
                            break;
                        case filterCaseUsuario.Exportar_SEGListarUsuarioPorPerfil:
                            UsuarioDTOList = oUsuarioData.Exportar_SEGListarUsuarioPorPerfil(oReqFilterUsuarioDTO.Item);
                            break;
                        default:
                            UsuarioDTOList = oUsuarioData.Listar(oReqFilterUsuarioDTO.Item);                            
                            break;



                    }

                    oRespListUsuarioDTO.List = UsuarioDTOList;
                    oRespListUsuarioDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListUsuarioDTO.Success = false;
                    oRespListUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListUsuarioDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	UsuarioGetItem
		//Objetivo: Retorna un registro de tipo UsuarioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemUsuarioDTO UsuarioGetItem(ReqFilterUsuarioDTO oReqFilterUsuarioDTO)
		{
			RespItemUsuarioDTO oRespItemUsuarioDTO = new RespItemUsuarioDTO();

            oRespItemUsuarioDTO.Success = false;
            oRespItemUsuarioDTO.Item = null;
            oRespItemUsuarioDTO.User = oReqFilterUsuarioDTO.User;
            oRespItemUsuarioDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterUsuarioDTO.User))
            {
                oRespItemUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Usuario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemUsuarioDTO.MessageList.Count == 0)
            {
                UsuarioDTO oUsuarioDTO = null;
                try
                {
                    switch (oReqFilterUsuarioDTO.FilterCase)
                    {

                        case filterCaseUsuario.BuscarInfoUsuario:
                            {
                                oUsuarioDTO = new UsuarioDTO();
                                oUsuarioDTO = oUsuarioData.BuscarInformacionDelUsuario(oReqFilterUsuarioDTO.Item);
                            }
                            break;
                        case filterCaseUsuario.filterCase_ValidarUsuarioLogeo:
                            {
                                oUsuarioDTO = new UsuarioDTO();
                                oUsuarioDTO = oUsuarioData.ValidarUsuarioLogeo(oReqFilterUsuarioDTO.Item);
                            }
                            break;
                        case filterCaseUsuario.uspValidarConfiguracionUsuarios:
                            {
                                oUsuarioDTO = new UsuarioDTO();
                                oUsuarioDTO = oUsuarioData.uspValidarConfiguracionUsuarios(oReqFilterUsuarioDTO.Item);
                            }
                            break;

                        case filterCaseUsuario.porCodigo:
                            {
                                oUsuarioDTO = new UsuarioDTO();
                                oUsuarioDTO = oUsuarioData.BuscarPorCodigoUsuario(oReqFilterUsuarioDTO.Item);
                            }
                            break;
                        case filterCaseUsuario.uspValidarUsuarioIngresado:
                            {
                                oUsuarioDTO = new UsuarioDTO();
                                oUsuarioDTO = oUsuarioData.uspValidarUsuarioIngresado(oReqFilterUsuarioDTO.Item);
                            }
                            break;

                        case filterCaseUsuario.uspValidarExisteCita_Usuario_AgendaGeneral:
                            {
                                oUsuarioDTO = new UsuarioDTO();
                                oUsuarioDTO = oUsuarioData.uspValidarExisteCita_Usuario_AgendaGeneral(oReqFilterUsuarioDTO.Item);
                            }
                            break;

                        default:
                            {
                                oUsuarioDTO = new UsuarioDTO();
                            }
                            break;
                    }

                    oRespItemUsuarioDTO.Item = new UsuarioDTO();
                    oRespItemUsuarioDTO.Item = oUsuarioDTO;
                    oRespItemUsuarioDTO.Success = true;
                    oRespItemUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemUsuarioDTO.Success = false;
                    oRespItemUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemUsuarioDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo UsuarioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespUsuarioDTO ExecuteTransac(ReqUsuarioDTO oReqUsuarioDTO)
		{
			RespUsuarioDTO oRespUsuarioDTO = new RespUsuarioDTO();

            oRespUsuarioDTO.MessageList = new List<Mensaje>();
            oRespUsuarioDTO.User = oReqUsuarioDTO.User;
            
            if (String.IsNullOrEmpty(oReqUsuarioDTO.User))
            {
                oRespUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Usuario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespUsuarioDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int estadoClave = 0;
                        string CodigoAccesoSistema = "";
                        foreach (UsuarioDTO item in oReqUsuarioDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    CodigoAccesoSistema = "999999999";
                                    oUsuarioData.Registrar(item);

                                    break;
                                case Operation.Update:
                                    CodigoAccesoSistema = "999999999";
                                    oUsuarioData.Actualizar(item);

                                    break;
                                case Operation.uspActualizarDatosUsuarios:
                                    oUsuarioData.uspActualizarDatosUsuarios(item);
                                    break;
                               
                                case Operation.UpdateClave:
                                    CodigoAccesoSistema = "999999999";
                                    estadoClave = oUsuarioData.ActualizarClave(item);
                                    break;
                         
                                case Operation.Delete:

                                    CodigoAccesoSistema = "999999999";
                                    oUsuarioData.Eliminar(item);

                                    break;

                                   case Operation.uspCambiarEstadoUsuarioConfiguracionNuevoMes:
                                    CodigoAccesoSistema = "999999999";
                                    oUsuarioData.uspCambiarEstadoUsuarioConfiguracionNuevoMes(item);

                                    break;

                                case Operation.Eliminarfiltro:
                                    oUsuarioData.EliminarFiltro(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespUsuarioDTO.Success = true;
                        oRespUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = estadoClave,
                            Detalle = CodigoAccesoSistema,
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespUsuarioDTO.Success = false;
                        oRespUsuarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespUsuarioDTO;
		}

        /*
        public int ValidarUsuario(int sede,string usuario, string contrasenia) {

            string ruta = @"C:\Windows\Licencia.txt";
            int flag = 0;

            flag = oUsuarioData.ValidarUsuario(sede, usuario, contrasenia);

            return flag;
        }
        */


		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int uspValidarExisteUsuario(int CodigoUnidadNegocio, int CodSede, string NombreCompleto)
        {
            int flag = 0;
            flag = oUsuarioData.uspValidarExisteUsuario(CodigoUnidadNegocio, CodSede, NombreCompleto);
            return flag;
        }

    }
}
