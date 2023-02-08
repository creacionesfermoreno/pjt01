using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Transactions;

namespace E_BusinessLayer.Gimnasio
{
    public class PersonalAdministrativoLogic : IDisposable
    {
        PersonalAdministrativoData oPersonalAdministrativoData = null;

        PersonalAsistenciaConfiguracionData oPersonalAsistenciaConfiguracionData = null;
        public PersonalAdministrativoLogic()
        {
            oPersonalAdministrativoData = new PersonalAdministrativoData();
            oPersonalAsistenciaConfiguracionData = new PersonalAsistenciaConfiguracionData();
        }

        public RespListPersonalAdministrativoDTO PersonalAdministrativoGetList(ReqFilterPersonalAdministrativoDTO oReqFilterPersonalAdministrativoDTO)
        {

            RespListPersonalAdministrativoDTO oRespListPersonalAdministrativoDTO = new RespListPersonalAdministrativoDTO();

            oRespListPersonalAdministrativoDTO.List = new List<PersonalAdministrativoDTO>();
            oRespListPersonalAdministrativoDTO.User = oReqFilterPersonalAdministrativoDTO.User;
            oRespListPersonalAdministrativoDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterPersonalAdministrativoDTO.User))
            {
                oRespListPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Asistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterPersonalAdministrativoDTO.Paging == null)
            {
                oRespListPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListPersonalAdministrativoDTO.MessageList.Count == 0)
            {

                try
                {
                    List<PersonalAdministrativoDTO> PersonalAdministrativoDTOList = new List<PersonalAdministrativoDTO>();

                    switch (oReqFilterPersonalAdministrativoDTO.FilterCase)
                    {

                        case filterCasePersonalAdministrativo.PorCodigo:
                            {
                                
                                if (!oReqFilterPersonalAdministrativoDTO.Paging.All && oReqFilterPersonalAdministrativoDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterPersonalAdministrativoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListar_Socios_Inasistencias_NumeroRegistro"]);
                                }
                                PersonalAdministrativoDTOList = oPersonalAdministrativoData.Listar(oReqFilterPersonalAdministrativoDTO.Item);
                            }
                            break;
                        case filterCasePersonalAdministrativo.ListarPorFiltros:
                            {
                                PersonalAdministrativoDTOList = oPersonalAdministrativoData.ListarPorFiltros(oReqFilterPersonalAdministrativoDTO.Item);
                            }
                            break;
                        default:
                            PersonalAdministrativoDTOList = oPersonalAdministrativoData.Listar(oReqFilterPersonalAdministrativoDTO.Item);
                            break;

                    }

                    oRespListPersonalAdministrativoDTO.List = PersonalAdministrativoDTOList;
                    oRespListPersonalAdministrativoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPersonalAdministrativoDTO.Success = false;
                    oRespListPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListPersonalAdministrativoDTO;

        }

        public RespItemPersonalAdministrativoDTO PersonalAdministrativoGetItem(ReqFilterPersonalAdministrativoDTO oReqFilterPersonalAdministrativoDTO)
        {
            RespItemPersonalAdministrativoDTO oRespItemPersonalAdministrativoDTO = new RespItemPersonalAdministrativoDTO();

            oRespItemPersonalAdministrativoDTO.Success = false;
            oRespItemPersonalAdministrativoDTO.Item = null;
            oRespItemPersonalAdministrativoDTO.User = oReqFilterPersonalAdministrativoDTO.User;
            oRespItemPersonalAdministrativoDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterPersonalAdministrativoDTO.User))
            {
                oRespItemPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Asistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPersonalAdministrativoDTO.MessageList.Count == 0)
            {
                PersonalAdministrativoDTO oPersonalAdministrativoDTO = null;
                try
                {
                    switch (oReqFilterPersonalAdministrativoDTO.FilterCase)
                    {

                        case filterCasePersonalAdministrativo.PorCodigo:
                            {
                                oPersonalAdministrativoDTO = new PersonalAdministrativoDTO();
                                //oPersonalAdministrativoDTO = oPersonalAdministrativoData. .BuscarPorCodigoAsistencia(oReqFilterAsistenciaDTO.Item);
                            }
                            break;
                        case filterCasePersonalAdministrativo.BuscarAsistenciaConfiguracionPorNumeroDocumento:
                            {
                                oPersonalAdministrativoDTO = new PersonalAdministrativoDTO();
                                oPersonalAdministrativoDTO = oPersonalAdministrativoData.uspBuscarPersonalAsistenciaConfiguracionPorDNI(oReqFilterPersonalAdministrativoDTO.Item);
                            }
                            break;
                        case filterCasePersonalAdministrativo.BuscarPorNumeroDocumentoGlobal:
                            oPersonalAdministrativoDTO = new PersonalAdministrativoDTO();
                            oPersonalAdministrativoDTO = oPersonalAdministrativoData.uspBuscarPersonalAdministrativoGeneralPorNumeroDocumento(oReqFilterPersonalAdministrativoDTO.Item);
                            break;
                        default:
                            {
                                oPersonalAdministrativoDTO = new PersonalAdministrativoDTO();
                            }
                            break;
                    }

                    oRespItemPersonalAdministrativoDTO.Item = new PersonalAdministrativoDTO();
                    oRespItemPersonalAdministrativoDTO.Item = oPersonalAdministrativoDTO;
                    oRespItemPersonalAdministrativoDTO.Success = true;
                    oRespItemPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPersonalAdministrativoDTO.Success = false;
                    oRespItemPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }
            return oRespItemPersonalAdministrativoDTO;
        }

        public RespPersonalAdministrativoDTO ExecuteTransac(ReqPersonalAdministrativoDTO oReqPersonalAdministrativoDTO)
        {
            RespPersonalAdministrativoDTO oRespPersonalAdministrativoDTO = new RespPersonalAdministrativoDTO();

            oRespPersonalAdministrativoDTO.MessageList = new List<Mensaje>();
            oRespPersonalAdministrativoDTO.User = oReqPersonalAdministrativoDTO.User;

            if (String.IsNullOrEmpty(oReqPersonalAdministrativoDTO.User))
            {
                oRespPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Asistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }


            if (oRespPersonalAdministrativoDTO.MessageList.Count == 0)
            {

                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int CodigoIngreso = 0;
                        foreach (PersonalAdministrativoDTO item in oReqPersonalAdministrativoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    var CodigoPersonal = oPersonalAdministrativoData.ValidarPersonalAdministrativoPorNumeroDocumento(item);
                                    if (CodigoPersonal.Length < 10)
                                    {
                                        //throw new Exception("Personal con el Nro" + item.NumeroDocumento + " ya esta registrado." );
                                        item.CodigoPersonalAdministrativo = oPersonalAdministrativoData.Registrar(item);
                                    }
                                    else
                                    {
                                        item.CodigoPersonalAdministrativo = item.CodigoPersonalAdministrativo ?? CodigoPersonal;
                                    }

                                    if (item.AsistenciaConfiguracion != null)
                                    {
                                        if (string.IsNullOrEmpty(item.AsistenciaConfiguracion.CodigoPersonalAsistenciaConfiguracion))
                                        {
                                            oPersonalAsistenciaConfiguracionData.Registrar(new PersonalAsistenciaConfiguracionDTO()
                                            {
                                                CodigoUnidadNegocio = item.CodigoUnidadNegocio,                                               
                                                CodigoSede = item.CodigoSede,
                                                CodigoPersonal = item.CodigoPersonalAdministrativo,
                                                CodigoCargo = item.AsistenciaConfiguracion.CodigoCargo,

                                                HoraIngreso_Lunes_Turno1  = item.AsistenciaConfiguracion.HoraIngreso_Lunes_Turno1,
                                                HoraSalida_Lunes_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Lunes_Turno1,
                                                HoraIngreso_Martes_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Martes_Turno1,
                                                HoraSalida_Martes_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Martes_Turno1,
                                                HoraIngreso_Miercoles_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Miercoles_Turno1,
                                                HoraSalida_Miercoles_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Miercoles_Turno1 ,
                                                HoraIngreso_Jueves_Turno1  = item.AsistenciaConfiguracion.HoraIngreso_Jueves_Turno1,
                                                HoraSalida_Jueves_Turno1  = item.AsistenciaConfiguracion.HoraSalida_Jueves_Turno1,
                                                HoraIngreso_Viernes_Turno1  = item.AsistenciaConfiguracion.HoraIngreso_Viernes_Turno1  ,
                                                HoraSalida_Viernes_Turno1  = item.AsistenciaConfiguracion.HoraSalida_Viernes_Turno1   ,
                                                HoraIngreso_Sabado_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Sabado_Turno1   ,
                                                HoraSalida_Sabado_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Sabado_Turno1    ,
                                                HoraIngreso_Domingo_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Domingo_Turno1,
                                                HoraSalida_Domingo_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Domingo_Turno1,
                                                HoraIngreso_Lunes_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Lunes_Turno2    ,
                                                HoraSalida_Lunes_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Lunes_Turno2     ,
                                                HoraIngreso_Martes_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Martes_Turno2   ,
                                                HoraSalida_Martes_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Martes_Turno2    ,
                                                HoraIngreso_Miercoles_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Miercoles_Turno2,
                                                HoraSalida_Miercoles_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Miercoles_Turno2 ,
                                                HoraIngreso_Jueves_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Jueves_Turno2   ,
                                                HoraSalida_Jueves_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Jueves_Turno2    ,
                                                HoraIngreso_Viernes_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Viernes_Turno2  ,
                                                HoraSalida_Viernes_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Viernes_Turno2   ,
                                                HoraIngreso_Sabado_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Sabado_Turno2   ,
                                                HoraSalida_Sabado_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Sabado_Turno2    ,
                                                HoraIngreso_Domingo_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Domingo_Turno2  ,
                                                HoraSalida_Domingo_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Domingo_Turno2,
                                                
                                                Sueldo = item.AsistenciaConfiguracion.Sueldo,
                                                MinutosTolerancia = item.AsistenciaConfiguracion.MinutosTolerancia,
                                                MinutosRefrigerio = item.AsistenciaConfiguracion.MinutosRefrigerio,                                                
                                                DescuentoXMinuto = item.AsistenciaConfiguracion.DescuentoXMinuto,
                                              
                                                UsuarioCreacion = item.UsuarioCreacion,
                                                UsuarioEdicion = item.UsuarioEdicion
                                            });
                                        }
                                        else
                                        {
                                            //oPersonalAsistenciaConfiguracionData.Actualizar(new PersonalAsistenciaConfiguracionDTO()
                                            //{
                                            //    CodigoPersonalAsistenciaConfiguracion = item.AsistenciaConfiguracion.CodigoPersonalAsistenciaConfiguracion,
                                            //    CodigoPersonal = item.CodigoPersonalAdministrativo,
                                            //    CodigoSede = item.CodigoSede,
                                            //    HoraIngresoLV = item.AsistenciaConfiguracion.HoraIngresoLV,
                                            //    HoraSalidaLV = item.AsistenciaConfiguracion.HoraSalidaLV,
                                            //    HoraIngresoSabado = item.AsistenciaConfiguracion.HoraIngresoSabado,
                                            //    HoraIngresoDomingo = item.AsistenciaConfiguracion.HoraIngresoDomingo,
                                            //    UsuarioCreacion = item.UsuarioCreacion,
                                            //    UsuarioEdicion = item.UsuarioEdicion
                                            //});
                                        }
                                    }
                                    break;
                                case Operation.Update:
                                    if (string.IsNullOrEmpty(item.UrlImagen))
                                    {
                                        oPersonalAdministrativoData.Actualizar(item);
                                        if (item.AsistenciaConfiguracion != null)
                                        {
                                            if (string.IsNullOrEmpty(item.AsistenciaConfiguracion.CodigoPersonalAsistenciaConfiguracion))
                                        {
                                            oPersonalAsistenciaConfiguracionData.Registrar(new PersonalAsistenciaConfiguracionDTO()
                                            {
                                                CodigoUnidadNegocio = item.CodigoUnidadNegocio,                                               
                                                CodigoSede = item.CodigoSede,
                                                CodigoPersonal = item.CodigoPersonalAdministrativo,
                                                CodigoCargo = item.AsistenciaConfiguracion.CodigoCargo,

                                                HoraIngreso_Lunes_Turno1  = item.AsistenciaConfiguracion.HoraIngreso_Lunes_Turno1,
                                                HoraSalida_Lunes_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Lunes_Turno1,
                                                HoraIngreso_Martes_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Martes_Turno1,
                                                HoraSalida_Martes_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Martes_Turno1,
                                                HoraIngreso_Miercoles_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Miercoles_Turno1,
                                                HoraSalida_Miercoles_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Miercoles_Turno1 ,
                                                HoraIngreso_Jueves_Turno1  = item.AsistenciaConfiguracion.HoraIngreso_Jueves_Turno1,
                                                HoraSalida_Jueves_Turno1  = item.AsistenciaConfiguracion.HoraSalida_Jueves_Turno1,
                                                HoraIngreso_Viernes_Turno1  = item.AsistenciaConfiguracion.HoraIngreso_Viernes_Turno1  ,
                                                HoraSalida_Viernes_Turno1  = item.AsistenciaConfiguracion.HoraSalida_Viernes_Turno1   ,
                                                HoraIngreso_Sabado_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Sabado_Turno1   ,
                                                HoraSalida_Sabado_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Sabado_Turno1    ,
                                                HoraIngreso_Domingo_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Domingo_Turno1,
                                                HoraSalida_Domingo_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Domingo_Turno1,
                                                HoraIngreso_Lunes_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Lunes_Turno2    ,
                                                HoraSalida_Lunes_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Lunes_Turno2     ,
                                                HoraIngreso_Martes_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Martes_Turno2   ,
                                                HoraSalida_Martes_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Martes_Turno2    ,
                                                HoraIngreso_Miercoles_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Miercoles_Turno2,
                                                HoraSalida_Miercoles_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Miercoles_Turno2 ,
                                                HoraIngreso_Jueves_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Jueves_Turno2   ,
                                                HoraSalida_Jueves_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Jueves_Turno2    ,
                                                HoraIngreso_Viernes_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Viernes_Turno2  ,
                                                HoraSalida_Viernes_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Viernes_Turno2   ,
                                                HoraIngreso_Sabado_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Sabado_Turno2   ,
                                                HoraSalida_Sabado_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Sabado_Turno2    ,
                                                HoraIngreso_Domingo_Turno2  = item.AsistenciaConfiguracion.HoraIngreso_Domingo_Turno2  ,
                                                HoraSalida_Domingo_Turno2  = item.AsistenciaConfiguracion.HoraSalida_Domingo_Turno2,
                                                
                                                Sueldo = item.AsistenciaConfiguracion.Sueldo,
                                                MinutosTolerancia = item.AsistenciaConfiguracion.MinutosTolerancia,
                                                MinutosRefrigerio = item.AsistenciaConfiguracion.MinutosRefrigerio,                                                
                                                DescuentoXMinuto = item.AsistenciaConfiguracion.DescuentoXMinuto,
                                              
                                                UsuarioCreacion = item.UsuarioCreacion,
                                                UsuarioEdicion = item.UsuarioEdicion
                                            });
                                        }
                                            else
                                            {
                                                oPersonalAsistenciaConfiguracionData.Actualizar(new PersonalAsistenciaConfiguracionDTO()
                                                {
                                                    CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                                                    CodigoSede = item.CodigoSede,

                                                    CodigoPersonalAsistenciaConfiguracion = item.AsistenciaConfiguracion.CodigoPersonalAsistenciaConfiguracion,
                                                    CodigoPersonal = item.CodigoPersonalAdministrativo,
                                                    CodigoCargo = item.AsistenciaConfiguracion.CodigoCargo,

                                                    HoraIngreso_Lunes_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Lunes_Turno1,
                                                    HoraSalida_Lunes_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Lunes_Turno1,
                                                    HoraIngreso_Martes_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Martes_Turno1,
                                                    HoraSalida_Martes_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Martes_Turno1,
                                                    HoraIngreso_Miercoles_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Miercoles_Turno1,
                                                    HoraSalida_Miercoles_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Miercoles_Turno1,
                                                    HoraIngreso_Jueves_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Jueves_Turno1,
                                                    HoraSalida_Jueves_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Jueves_Turno1,
                                                    HoraIngreso_Viernes_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Viernes_Turno1,
                                                    HoraSalida_Viernes_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Viernes_Turno1,
                                                    HoraIngreso_Sabado_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Sabado_Turno1,
                                                    HoraSalida_Sabado_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Sabado_Turno1,
                                                    HoraIngreso_Domingo_Turno1 = item.AsistenciaConfiguracion.HoraIngreso_Domingo_Turno1,
                                                    HoraSalida_Domingo_Turno1 = item.AsistenciaConfiguracion.HoraSalida_Domingo_Turno1,
                                                    HoraIngreso_Lunes_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Lunes_Turno2,
                                                    HoraSalida_Lunes_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Lunes_Turno2,
                                                    HoraIngreso_Martes_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Martes_Turno2,
                                                    HoraSalida_Martes_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Martes_Turno2,
                                                    HoraIngreso_Miercoles_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Miercoles_Turno2,
                                                    HoraSalida_Miercoles_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Miercoles_Turno2,
                                                    HoraIngreso_Jueves_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Jueves_Turno2,
                                                    HoraSalida_Jueves_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Jueves_Turno2,
                                                    HoraIngreso_Viernes_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Viernes_Turno2,
                                                    HoraSalida_Viernes_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Viernes_Turno2,
                                                    HoraIngreso_Sabado_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Sabado_Turno2,
                                                    HoraSalida_Sabado_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Sabado_Turno2,
                                                    HoraIngreso_Domingo_Turno2 = item.AsistenciaConfiguracion.HoraIngreso_Domingo_Turno2,
                                                    HoraSalida_Domingo_Turno2 = item.AsistenciaConfiguracion.HoraSalida_Domingo_Turno2,

                                                    Sueldo = item.AsistenciaConfiguracion.Sueldo,
                                                    MinutosTolerancia = item.AsistenciaConfiguracion.MinutosTolerancia,
                                                    MinutosRefrigerio = item.AsistenciaConfiguracion.MinutosRefrigerio,
                                                    DescuentoXMinuto = item.AsistenciaConfiguracion.DescuentoXMinuto,
                                                    
                                                    UsuarioCreacion = item.UsuarioCreacion,
                                                    UsuarioEdicion = item.UsuarioEdicion
                                                });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        oPersonalAdministrativoData.ActualizarFoto(item);
                                    }
                                    break;
                                case Operation.CesarPersonalAdministrativo:
                                    oPersonalAdministrativoData.CesarPersonalAdministrativo(item);
                                    break;
                                case Operation.ActivarPersonalAdministrativo:
                                    oPersonalAdministrativoData.ActivarPersonalAdministrativo(item);
                                    break;
                                case Operation.Delete:
                                    //oPersonalAdministrativoData.Eliminar(item);
                                    break;

                            }
                            tx.Complete();
                            oRespPersonalAdministrativoDTO.Success = true;
                            oRespPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = CodigoIngreso,
                                Detalle = "Proceso Grabado Correctamente.",
                                Tipo = TipoMensaje.Informacion
                            });

                        }
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPersonalAdministrativoDTO.Success = false;
                        oRespPersonalAdministrativoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPersonalAdministrativoDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
