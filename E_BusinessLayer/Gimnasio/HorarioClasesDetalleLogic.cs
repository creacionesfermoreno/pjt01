
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
    //Archivo     : HorarioClasesDetalleLogic.cs
    //Proyecto    : (NOMBRE DEL PROYECTO)
    //Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
    //Fecha       : 22/03/2018
    //Descripcion : Clase para capa de negocio
    //-------------------------------------------------------------------
    public class HorarioClasesDetalleLogic : IDisposable
    {
        HorarioClasesDetalleData oHorarioClasesDetalleData = null;
        HorarioClasesConfiguracionData oHorarioClasesConfiguracionData = null;
        HorarioClasesData oHorarioClasesData = null;
        SalaHorarioData oSalaHorarioData = null;
        ClientesData oSociosData = null;
        ContratoData oContratoData = null;
        AsistenciaData oAsistenciaData = null;
        InvitadosData oInvitadosData = null;
        AsistenciaInvitadosData oAsistenciaInvitadosData = null;
        PlanesData oPaquetesData = null;

        public HorarioClasesDetalleLogic()
        {
            oHorarioClasesDetalleData = new HorarioClasesDetalleData();
            oHorarioClasesConfiguracionData = new HorarioClasesConfiguracionData();
            oHorarioClasesData = new HorarioClasesData();
            oSalaHorarioData = new SalaHorarioData();
            oSociosData = new ClientesData();
            oContratoData = new ContratoData();
            oAsistenciaData = new AsistenciaData();
            oInvitadosData = new InvitadosData();
            oAsistenciaInvitadosData = new AsistenciaInvitadosData();
            oPaquetesData = new PlanesData();
        }

        //-------------------------------------------------------------------
        //Nombre:	HorarioClasesDetalleGetList
        //Objetivo: Retorna una colección de registros de tipo HorarioClasesDetalleDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespListHorarioClasesDetalleDTO HorarioClasesDetalleGetList(ReqFilterHorarioClasesDetalleDTO oReqFilterHorarioClasesDetalleDTO)
        {

            RespListHorarioClasesDetalleDTO oRespListHorarioClasesDetalleDTO = new RespListHorarioClasesDetalleDTO();

            oRespListHorarioClasesDetalleDTO.List = new List<HorarioClasesDetalleDTO>();
            oRespListHorarioClasesDetalleDTO.User = oReqFilterHorarioClasesDetalleDTO.User;
            oRespListHorarioClasesDetalleDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterHorarioClasesDetalleDTO.User))
            {
                oRespListHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClasesDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterHorarioClasesDetalleDTO.Paging == null)
            {
                oRespListHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListHorarioClasesDetalleDTO.MessageList.Count == 0)
            {

                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterHorarioClasesDetalleDTO.Paging.All && oReqFilterHorarioClasesDetalleDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterHorarioClasesDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<HorarioClasesDetalleDTO> HorarioClasesDetalleDTOList = new List<HorarioClasesDetalleDTO>();

                    switch (oReqFilterHorarioClasesDetalleDTO.FilterCase)
                    {
                        default:
                            {
                                HorarioClasesDetalleDTOList = oHorarioClasesDetalleData.Listar(oReqFilterHorarioClasesDetalleDTO.Item);
                            }
                            break;
                    }

                    oRespListHorarioClasesDetalleDTO.List = HorarioClasesDetalleDTOList;
                    oRespListHorarioClasesDetalleDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListHorarioClasesDetalleDTO.Success = false;
                    oRespListHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListHorarioClasesDetalleDTO;

        }

        //-------------------------------------------------------------------
        //Nombre:	HorarioClasesDetalleGetItem
        //Objetivo: Retorna un registro de tipo HorarioClasesDetalleDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespItemHorarioClasesDetalleDTO HorarioClasesDetalleGetItem(ReqFilterHorarioClasesDetalleDTO oReqFilterHorarioClasesDetalleDTO)
        {
            RespItemHorarioClasesDetalleDTO oRespItemHorarioClasesDetalleDTO = new RespItemHorarioClasesDetalleDTO();

            oRespItemHorarioClasesDetalleDTO.Success = false;
            oRespItemHorarioClasesDetalleDTO.Item = null;
            oRespItemHorarioClasesDetalleDTO.User = oReqFilterHorarioClasesDetalleDTO.User;
            oRespItemHorarioClasesDetalleDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterHorarioClasesDetalleDTO.User))
            {
                oRespItemHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClasesDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemHorarioClasesDetalleDTO.MessageList.Count == 0)
            {
                HorarioClasesDetalleDTO oHorarioClasesDetalleDTO = null;
                HorarioClasesDetalleCalendarioDTO oHorarioClasesDetalleCalendarioDTO = null;
                try
                {
                    switch (oReqFilterHorarioClasesDetalleDTO.FilterCase)
                    {
                        case filterCaseHorarioClasesDetalle.ListaCalendario:
                            {
                                #region ListaCalendario 
                                oHorarioClasesDetalleCalendarioDTO = new HorarioClasesDetalleCalendarioDTO()
                                {
                                    ListaConfiguracion = new List<HorarioClasesConfiguracionDTO>(),
                                    ListaHorarios = new List<HorarioClasesDTO>(),
                                    ListaSalas = new List<SalaHorarioDTO>(),
                                    ExisteHorario = true
                                };

                                oHorarioClasesDetalleCalendarioDTO.ListaHorarios = oHorarioClasesData.ListarCalendarioDelDia(new HorarioClasesDTO()
                                {
                                    CodigoUnidadNegocio = oReqFilterHorarioClasesDetalleDTO.Item.CodigoUnidadNegocio,
                                    CodigoSede = oReqFilterHorarioClasesDetalleDTO.Item.CodigoSede
                                });

                                oHorarioClasesDetalleCalendarioDTO.ListaSalas = oSalaHorarioData.Listar(new SalaHorarioDTO()
                                {
                                    CodigoUnidadNegocio = oReqFilterHorarioClasesDetalleDTO.Item.CodigoUnidadNegocio,
                                    CodigoSede = oReqFilterHorarioClasesDetalleDTO.Item.CodigoSede
                                });

                                if (oHorarioClasesDetalleCalendarioDTO.ListaHorarios.Count == 0)
                                {
                                    oHorarioClasesDetalleCalendarioDTO.ExisteHorario = false;
                                    oHorarioClasesDetalleCalendarioDTO.ListaConfiguracion = oHorarioClasesConfiguracionData.ListarConfiguracionPorDia(new HorarioClasesConfiguracionDTO()
                                    {
                                        CodigoUnidadNegocio = oReqFilterHorarioClasesDetalleDTO.Item.CodigoUnidadNegocio,
                                        CodigoSede = oReqFilterHorarioClasesDetalleDTO.Item.CodigoSede
                                    });
                                }
                                #endregion
                            }
                            break;
                        case filterCaseHorarioClasesDetalle.InformacionSocio:
                            oHorarioClasesDetalleDTO = new HorarioClasesDetalleDTO() { Socio = new ClientesDTO(), ListaMembresias = new List<ContratoDTO>() };
                            oHorarioClasesDetalleDTO.Socio = oSociosData.BuscarInfoPorCodSocioFiltro(new ClientesDTO()
                            {
                                CodigoUnidadNegocio = oReqFilterHorarioClasesDetalleDTO.Item.CodigoUnidadNegocio,
                                CodigoSede = oReqFilterHorarioClasesDetalleDTO.Item.CodigoSede,
                                Filtro = oReqFilterHorarioClasesDetalleDTO.Item.CodigoSocio.ToString()
                            });
                            oHorarioClasesDetalleDTO.ListaMembresias = oContratoData.uspListarMembresiasSociosClasesGrupales(new ContratoDTO()
                            {
                                CodigoUnidadNegocio = oReqFilterHorarioClasesDetalleDTO.Item.CodigoUnidadNegocio,
                                CodigoSede = oReqFilterHorarioClasesDetalleDTO.Item.CodigoSede,
                                CodigoSocio = oHorarioClasesDetalleDTO.Socio.CodigoSocio //oReqFilterHorarioClasesDetalleDTO.Item.CodigoSocio
                            });
                            break;
                        default:
                            {
                                oHorarioClasesDetalleDTO = new HorarioClasesDetalleDTO();
                            }
                            break;
                    }

                    oRespItemHorarioClasesDetalleDTO.Item = new HorarioClasesDetalleDTO();
                    oRespItemHorarioClasesDetalleDTO.Item = oHorarioClasesDetalleDTO;

                    oRespItemHorarioClasesDetalleDTO.Data = new HorarioClasesDetalleCalendarioDTO();
                    oRespItemHorarioClasesDetalleDTO.Data = oHorarioClasesDetalleCalendarioDTO;

                    oRespItemHorarioClasesDetalleDTO.Success = true;
                    oRespItemHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemHorarioClasesDetalleDTO.Success = false;
                    oRespItemHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemHorarioClasesDetalleDTO;
        }

        //-------------------------------------------------------------------
        //Nombre:	ExecuteTransac
        //Objetivo: Almacena el registro de un objeto de tipo HorarioClasesDetalleDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespHorarioClasesDetalleDTO ExecuteTransac(ReqHorarioClasesDetalleDTO oReqHorarioClasesDetalleDTO)
        {
            RespHorarioClasesDetalleDTO oRespHorarioClasesDetalleDTO = new RespHorarioClasesDetalleDTO()
            {
                Item = new HorarioClasesDetalleDTO()
                {
                    Socio = new ClientesDTO() { },
                    Invitado = new InvitadosDTO() { },
                    ListaMembresias = new List<ContratoDTO>()
                }
            };

            oRespHorarioClasesDetalleDTO.MessageList = new List<Mensaje>();
            oRespHorarioClasesDetalleDTO.User = oReqHorarioClasesDetalleDTO.User;

            if (String.IsNullOrEmpty(oReqHorarioClasesDetalleDTO.User))
            {
                oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioClasesDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            /*Validaciones*/
            foreach (var item in oReqHorarioClasesDetalleDTO.List)
            {
                switch (item.Operation)
                {
                    case Operation.RegistroAsistenciaClasesGrupalesChecking:

                        #region Validacion Cases Grupales
                        //
                        if (oHorarioClasesDetalleData.ValidarExisteReservaHorarioClasesPorSocio(item) > 0)
                        {
                            oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = 151,
                                Detalle = "El nro de cupo " + item.NroCupo.ToString() + " ya esta ocupado",
                                Tipo = TipoMensaje.Error
                            });
                        }
                        else
                        {
                            var socios = new ClientesDTO() { };
                            socios = oSociosData.BuscarInfoPorCodSocioFiltro(new ClientesDTO()
                            {
                                CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                                CodigoSede = item.CodigoSede,
                                Filtro = item.CodigoSocio.ToString()
                            });
                            if (socios != null && socios.CodigoSocio > 0)
                            {
                                oRespHorarioClasesDetalleDTO.Item.Socio = socios;
                                oRespHorarioClasesDetalleDTO.Item.ListaMembresias = oContratoData.uspListarMembresiasSociosClasesGrupales(new ContratoDTO()
                                {
                                    CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                                    CodigoSede = item.CodigoSede,
                                    CodigoSocio = item.CodigoSocio
                                });
                            }
                            else
                            {
                                oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                {
                                    Codigo = 152,
                                    Detalle = "El socio con el codigo " + item.CodigoSocio.ToString() + " no existe.",
                                    Tipo = TipoMensaje.Error
                                });
                            }
                        }

                        //Validacion de Asistencia
                        int horarioDisponible = oPaquetesData.ValidarBuscarDiasHorarioPaquete(item.CodigoUnidadNegocio, item.CodigoPaquete);
                        if (horarioDisponible == 0)
                        {
                            oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = 150 + oRespHorarioClasesDetalleDTO.MessageList.Count,
                                Detalle = "Horario No disponible.",
                                Tipo = TipoMensaje.Error
                            });
                        }
                        #endregion
                        break;
                    case Operation.RegistroAsistenciaInvitadosClasesGrupalesChecking:
                        #region Validacion Checking Invitado 
                        var invitado = oInvitadosData.uspBuscarInfoPorCodInvitadoFiltro(new InvitadosDTO()
                        {
                            CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                            CodigoSede = item.CodigoSede,
                            CodigoInvitado = item.CodigoInvitado
                        });
                        if (invitado == null)
                        {
                            oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = 153,
                                Detalle = "El invitado con el codigo " + item.CodigoInvitado.ToString() + " no existe.",
                                Tipo = TipoMensaje.Error
                            });
                        }
                        else
                        {
                            if (invitado.NroDias == 0 && invitado.NroDiasActual == 0)
                            {
                                oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                {
                                    Codigo = 154,
                                    Detalle = "El invitado con el codigo " + item.CodigoInvitado.ToString() + " No tiene días libres registrados .. ¡",
                                    Tipo = TipoMensaje.Error
                                });
                            }
                            else if (invitado.NroDias <= invitado.NroDiasActual && invitado.NroDias != 0)
                            {
                                oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                {
                                    Codigo = 154,
                                    Detalle = "El invitado con el codigo " + item.CodigoInvitado.ToString() + " se terminaron sus días libres .. ¡",
                                    Tipo = TipoMensaje.Error
                                });
                            }
                            else if (invitado.NroDias > invitado.NroDiasActual)
                            {
                                item.Invitado = invitado;
                                item.Invitado.CodigoInvitado = item.CodigoInvitado;
                                item.Invitado.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                item.Invitado.CodigoSede = item.CodigoSede;
                                item.Invitado.UsuarioCreacion = item.UsuarioCreacion;
                            }
                        }
                        #endregion
                        break;

                    case Operation.RegistroMasivoHorarioClasesGrupales:

                        break;
                }
            }

            if (oRespHorarioClasesDetalleDTO.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (HorarioClasesDetalleDTO item in oReqHorarioClasesDetalleDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    /*Registrar Masivo*/
                                    oHorarioClasesDetalleData.Registrar(item);
                                    break;
                                case Operation.RegistroMasivoHorarioClasesGrupales:
                                    /*Registrar*/
                                    oHorarioClasesDetalleData.RegistrarMasivoNuevosHorarioClasesGrupales(item);
                                    break;
                                case Operation.RegistroAsistenciaInvitadosClasesGrupalesChecking:
                                    if (item.Invitado.NroDias > item.Invitado.NroDiasActual)
                                    {
                                        /*Invitados*/
                                        oAsistenciaInvitadosData.uspActualizarAsistenciaInvitadoPorCodigoInvitado(new AsistenciaInvitadosDTO() { CodigoUnidadNegocio = item.CodigoUnidadNegocio, CodigoSede = item.CodigoSede, CodigoInvitado = item.CodigoInvitado });
                                        oAsistenciaInvitadosData.Registrar(new AsistenciaInvitadosDTO()
                                        {
                                            CodigoInvitado = item.CodigoInvitado,
                                            CodigoSede = item.CodigoSede,
                                            UsuarioCreacion = item.UsuarioCreacion,
                                            CodigoUnidadNegocio = item.CodigoUnidadNegocio
                                        });

                                    }
                                    /*Registro de reserva*/
                                    oHorarioClasesDetalleData.Registrar(item);
                                    oRespHorarioClasesDetalleDTO.Item.Invitado = item.Invitado;
                                    break;                              
                                case Operation.RegistroAsistenciaClasesGrupalesChecking:
                                    /*Verificar */
                                    UsuariosIngresosData oUsuariosIngresosDataUpdateMembresiaNroIngresos = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOUpdateMembresiaNroIngresos = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOUpdateMembresiaNroIngresos.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOUpdateMembresiaNroIngresos.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOUpdateMembresiaNroIngresos.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOUpdateMembresiaNroIngresos.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOUpdateMembresiaNroIngresos.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOUpdateMembresiaNroIngresos.Longitud = item.TK_Longitude;
                                    oUsuariosIngresosDTOUpdateMembresiaNroIngresos = oUsuariosIngresosDataUpdateMembresiaNroIngresos.uspValidarAccesoSistema(oUsuariosIngresosDTOUpdateMembresiaNroIngresos);

          

                                    if (oUsuariosIngresosDTOUpdateMembresiaNroIngresos.CodigoValidacion == 3)
                                    {
                                       string MensajeObervacion = oContratoData.ActualizarNroIngreso(new ContratoDTO()
                                        {
                                            CodigoMenbresia = item.CodigoMembresia,
                                            CodigoSocio = item.CodigoSocio,
                                            CodigoSede = item.CodigoSede,
                                            CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                                            UsuarioCreacion = item.UsuarioCreacion
                                        });
                                        if (Convert.ToInt32(MensajeObervacion.Split('|')[0]) > 0)
                                        {
                                            oAsistenciaData.Registrar(new AsistenciaDTO()
                                            {
                                                CodigoAsistencia = 0,
                                                CodigoPersona = item.CodigoSocio,
                                                TipoPersona = "S",
                                                CodigoPaquete = item.CodigoPaquete,
                                                CodigoMembresiaReal = item.CodigoMembresia,
                                                UsuarioCreacion = item.UsuarioCreacion,
                                                CodigoSede = item.CodigoSede,
                                                CodigoUnidadNegocio = item.CodigoUnidadNegocio
                                            });
                                        }
                                        else
                                        {
                                            throw new Exception("Llego al limite de asistensias, usted ya no puede marcar asistensia.");
                                        }
                                        
                                    }
                                    else
                                    {
                                        throw new Exception("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias");
                                    }

                                    oHorarioClasesDetalleData.Registrar(item);
                                    break;
                                case Operation.Delete:
                                    oHorarioClasesDetalleData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespHorarioClasesDetalleDTO.Success = true;
                        oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespHorarioClasesDetalleDTO.Success = false;
                        oRespHorarioClasesDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespHorarioClasesDetalleDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
