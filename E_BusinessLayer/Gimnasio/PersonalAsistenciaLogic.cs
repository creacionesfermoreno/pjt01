
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataLayer.CentroEntrenamiento;
using E_DataModel.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_BusinessLayer.Gimnasio
{
    //-------------------------------------------------------------------
    //Archivo     : PersonalAsistenciaLogic.cs
    //Proyecto    : (NOMBRE DEL PROYECTO)	
    //Fecha       : 26/04/2018
    //Descripcion : Clase para capa de negocio
    //-------------------------------------------------------------------
    public class PersonalAsistenciaLogic : IDisposable
    {
        PersonalAsistenciaData oPersonalAsistenciaData = null;
        ProfesionalFitnessData oProfesionalFitnessData = null;
        HorarioClasesData oHorarioClasesData = null;
        CentroEntrenamiento_Presencial_HorarioClasesConfiguracionData oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData = null;
        PersonalAdministrativoData oPersonalAdministrativoData = null;
        public PersonalAsistenciaLogic()
        {
            oPersonalAsistenciaData = new PersonalAsistenciaData();
            oProfesionalFitnessData = new ProfesionalFitnessData();
            oHorarioClasesData = new HorarioClasesData();
            oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionData();
            oPersonalAdministrativoData = new PersonalAdministrativoData();
        }

        //-------------------------------------------------------------------
        //Nombre:	PersonalAsistenciaGetList
        //Objetivo: Retorna una colección de registros de tipo PersonalAsistenciaDTO
        //Valores Prueba:
        //-------------------------------------------------------------------
        public RespListPersonalAsistenciaDTO PersonalAsistenciaGetList(ReqFilterPersonalAsistenciaDTO oReqFilterPersonalAsistenciaDTO)
        {

            RespListPersonalAsistenciaDTO oRespListPersonalAsistenciaDTO = new RespListPersonalAsistenciaDTO();

            oRespListPersonalAsistenciaDTO.List = new List<PersonalAsistenciaDTO>();
            oRespListPersonalAsistenciaDTO.ListProfesores = new List<PersonalFitnessAsistenciaDTO>();
            oRespListPersonalAsistenciaDTO.ListPersonalAdministrativoAsistencia = new List<PersonalAdministrativoAsistenciaResumentDTO>();

            oRespListPersonalAsistenciaDTO.User = oReqFilterPersonalAsistenciaDTO.User;
            oRespListPersonalAsistenciaDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterPersonalAsistenciaDTO.User))
            {
                oRespListPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterPersonalAsistenciaDTO.Paging == null)
            {
                oRespListPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
            if (oRespListPersonalAsistenciaDTO.MessageList.Count == 0)
            {
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterPersonalAsistenciaDTO.Paging.All && oReqFilterPersonalAsistenciaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPersonalAsistenciaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }
                    List<PersonalAsistenciaDTO> PersonalAsistenciaDTOList = new List<PersonalAsistenciaDTO>();
                    List<PersonalFitnessAsistenciaDTO> ProfesionalFitnessAsistenciaDTOList = new List<PersonalFitnessAsistenciaDTO>();
                    List<PersonalAdministrativoAsistenciaResumentDTO> personalAdministrativoResumenList = new List<PersonalAdministrativoAsistenciaResumentDTO>();

                    switch (oReqFilterPersonalAsistenciaDTO.FilterCase)
                    {
                        case filterCasePersonalAsistencia.AsistenciaProfesores:
                            ProfesionalFitnessAsistenciaDTOList = oPersonalAsistenciaData.ListarAsistenciaPorProfesionalFitness(oReqFilterPersonalAsistenciaDTO.Item, oReqFilterPersonalAsistenciaDTO.Paging, ref recordCount);
                            break;
                        case filterCasePersonalAsistencia.AsistenciaPersonalAdministrativo:
                            PersonalAsistenciaDTOList = oPersonalAsistenciaData.ListarAsistenciaPorPersonalAdministrativo(oReqFilterPersonalAsistenciaDTO.Item, oReqFilterPersonalAsistenciaDTO.Paging, ref recordCount);
                            break;
                        case filterCasePersonalAsistencia.ListaAsistenciaPorCodigoPersonal:
                            PersonalAsistenciaDTOList = oPersonalAsistenciaData.ListarAsistenciaPersonalPorCodigo(oReqFilterPersonalAsistenciaDTO.Item, oReqFilterPersonalAsistenciaDTO.Paging, ref recordCount);
                            break;
                        case filterCasePersonalAsistencia.FilterAutocomplete:
                            PersonalAsistenciaDTOList = oPersonalAsistenciaData.ListarPersonalPorFiltroAutocompletado(oReqFilterPersonalAsistenciaDTO.Item);
                            break;
                        case filterCasePersonalAsistencia.ListarTodasAsistenciaPorDNI:
                            {
                                var oPersonalAsistenciaDTO = new PersonalAsistenciaDTO();


                                //Personal Fijo
                                //==========================================================================
                                PersonalAdministrativoDTO personalAdm = oPersonalAdministrativoData.uspBuscarPersonalAsistenciaConfiguracionPorDNI(new PersonalAdministrativoDTO()
                                {
                                    NumeroDocumento = oReqFilterPersonalAsistenciaDTO.Item.NumeroDocumento,
                                    CodigoSede = oReqFilterPersonalAsistenciaDTO.Item.CodigoSede,
                                    CodigoUnidadNegocio = oReqFilterPersonalAsistenciaDTO.Item.CodigoUnidadNegocio
                                });
                                if (personalAdm != null)
                                {
                                    oReqFilterPersonalAsistenciaDTO.Item.CodigoPersonal = personalAdm.CodigoPersonalAdministrativo;
                                    var pPersonalFijoAsistencia = oPersonalAsistenciaData.BuscarPorCodigoPersonalAsistencia(oReqFilterPersonalAsistenciaDTO.Item);
                                    if (pPersonalFijoAsistencia == null)
                                    {
                                        pPersonalFijoAsistencia = new PersonalAsistenciaDTO();
                                    }
                                    pPersonalFijoAsistencia.PersonalAdministrativo = personalAdm;
                                    pPersonalFijoAsistencia.TipoPersonal = 2;
                                    PersonalAsistenciaDTOList.Add(pPersonalFijoAsistencia);
                                }
                                //==========================================================================
                                //Profesores
                                ProfesionalFitnessDTO profesorFit = oProfesionalFitnessData.uspBuscarProfesionalFitnessPorDNI(new ProfesionalFitnessDTO()
                                {
                                    CodigoSede = oReqFilterPersonalAsistenciaDTO.Item.CodigoSede,
                                    CodigoUnidadNegocio = oReqFilterPersonalAsistenciaDTO.Item.CodigoUnidadNegocio,
                                    DNI = oReqFilterPersonalAsistenciaDTO.Item.NumeroDocumento
                                });
                                if (profesorFit != null)
                                {
                                    oReqFilterPersonalAsistenciaDTO.Item.CodigoPersonal = profesorFit.CodigoProfesional;
                                    profesorFit.ListaHorarioClases = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.ListarPorCodigoProfesionalFitnessDelDia(new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                                    {
                                        CodigoUnidadNegocio = oReqFilterPersonalAsistenciaDTO.Item.CodigoUnidadNegocio,
                                        CodigoSede = oReqFilterPersonalAsistenciaDTO.Item.CodigoSede,
                                        CodigoProfesional = profesorFit.CodigoProfesional
                                    });
                                    //var oPersonalFitnessAsistencia = oPersonalAsistenciaData.BuscarPorCodigoPersonalAsistencia(oReqFilterPersonalAsistenciaDTO.Item);
                                    PersonalAsistenciaDTO oPersonalFitnessAsistencia = new PersonalAsistenciaDTO();
                                    //if (oPersonalFitnessAsistencia == null)
                                    //{
                                    //    oPersonalFitnessAsistencia = new PersonalAsistenciaDTO() { };
                                    //}
                                    oPersonalFitnessAsistencia.TipoPersonal = 1;
                                    oPersonalFitnessAsistencia.ProfesionalFitness = profesorFit;
                                    PersonalAsistenciaDTOList.Add(oPersonalFitnessAsistencia);
                                }
                            }
                            break;
                        case filterCasePersonalAsistencia.ListarAsistenciaPersonalAdministrativoResumen:
                            personalAdministrativoResumenList = oPersonalAsistenciaData.ListarPersonalAdministrativoAsistenciaResumen(oReqFilterPersonalAsistenciaDTO.Item, oReqFilterPersonalAsistenciaDTO.Paging, ref recordCount);
                            break;
                        default:
                            PersonalAsistenciaDTOList = oPersonalAsistenciaData.Listar(oReqFilterPersonalAsistenciaDTO.Item, oReqFilterPersonalAsistenciaDTO.Paging, ref recordCount);
                            break;
                    }
                    oRespListPersonalAsistenciaDTO.List = PersonalAsistenciaDTOList;
                    oRespListPersonalAsistenciaDTO.ListProfesores = ProfesionalFitnessAsistenciaDTOList;
                    oRespListPersonalAsistenciaDTO.ListPersonalAdministrativoAsistencia = personalAdministrativoResumenList;
                    oRespListPersonalAsistenciaDTO.Paging = new Paging()
                    {
                        TotalRecord = recordCount
                    };

                    oRespListPersonalAsistenciaDTO.Success = true;
                }
                catch (Exception ex)
                {
                    oRespListPersonalAsistenciaDTO.Success = false;
                    oRespListPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListPersonalAsistenciaDTO;

        }

        //-------------------------------------------------------------------
        //Nombre:	PersonalAsistenciaGetItem
        //Objetivo: Retorna un registro de tipo PersonalAsistenciaDTO
        //Valores Prueba:
        //-------------------------------------------------------------------
        public RespItemPersonalAsistenciaDTO PersonalAsistenciaGetItem(ReqFilterPersonalAsistenciaDTO oReqFilterPersonalAsistenciaDTO)
        {
            RespItemPersonalAsistenciaDTO oRespItemPersonalAsistenciaDTO = new RespItemPersonalAsistenciaDTO();

            oRespItemPersonalAsistenciaDTO.Success = false;
            oRespItemPersonalAsistenciaDTO.Item = null;
            oRespItemPersonalAsistenciaDTO.User = oReqFilterPersonalAsistenciaDTO.User;
            oRespItemPersonalAsistenciaDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterPersonalAsistenciaDTO.User))
            {
                oRespItemPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPersonalAsistenciaDTO.MessageList.Count == 0)
            {
                PersonalAsistenciaDTO oPersonalAsistenciaDTO = null;
                try
                {
                    switch (oReqFilterPersonalAsistenciaDTO.FilterCase)
                    {

                        case filterCasePersonalAsistencia.BuscarAsistenciaPorDNI:
                            {
                                oPersonalAsistenciaDTO = new PersonalAsistenciaDTO();
                                PersonalAdministrativoDTO personalAdm = null;
                                ProfesionalFitnessDTO profesorFit = null;

                                if (oReqFilterPersonalAsistenciaDTO.Item.PersonalAdministrativo != null)
                                {
                                    if (!string.IsNullOrEmpty(oReqFilterPersonalAsistenciaDTO.Item.PersonalAdministrativo.NumeroDocumento))
                                    {
                                        personalAdm = new PersonalAdministrativoDTO();
                                        personalAdm = oPersonalAdministrativoData.uspBuscarPersonalAsistenciaConfiguracionPorDNI(new PersonalAdministrativoDTO()
                                        {
                                            NumeroDocumento = oReqFilterPersonalAsistenciaDTO.Item.PersonalAdministrativo.NumeroDocumento,
                                            CodigoSede = oReqFilterPersonalAsistenciaDTO.Item.CodigoSede,
                                            CodigoUnidadNegocio = oReqFilterPersonalAsistenciaDTO.Item.CodigoUnidadNegocio
                                        });
                                        if (personalAdm != null)
                                        {
                                            oReqFilterPersonalAsistenciaDTO.Item.CodigoPersonal = personalAdm.CodigoPersonalAdministrativo;
                                            
                                        }
                                    }
                                }
                                if (oReqFilterPersonalAsistenciaDTO.Item.ProfesionalFitness != null)
                                {
                                    if (!string.IsNullOrEmpty(oReqFilterPersonalAsistenciaDTO.Item.ProfesionalFitness.DNI))
                                    {
                                        profesorFit = new ProfesionalFitnessDTO();
                                        profesorFit = oProfesionalFitnessData.uspBuscarProfesionalFitnessPorDNI(new ProfesionalFitnessDTO()
                                        {
                                            DNI = oReqFilterPersonalAsistenciaDTO.Item.ProfesionalFitness.DNI
                                        });
                                        if (profesorFit != null)
                                        {
                                            oReqFilterPersonalAsistenciaDTO.Item.CodigoPersonal = profesorFit.CodigoProfesional;
                                            profesorFit.ListaHorarioClases = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionData.ListarPorCodigoProfesionalFitnessDelDia(new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                                            {
                                                CodigoUnidadNegocio = oReqFilterPersonalAsistenciaDTO.Item.CodigoUnidadNegocio,
                                                CodigoSede = oReqFilterPersonalAsistenciaDTO.Item.CodigoSede,
                                                CodigoProfesional = profesorFit.CodigoProfesional
                                            });
                                        }
                                    }
                                }

                                oPersonalAsistenciaDTO = oPersonalAsistenciaData.BuscarPorCodigoPersonalAsistencia(oReqFilterPersonalAsistenciaDTO.Item);
                                if (oPersonalAsistenciaDTO != null)
                                {
                                    oPersonalAsistenciaDTO.ProfesionalFitness = profesorFit;
                                    oPersonalAsistenciaDTO.PersonalAdministrativo = personalAdm;
                                }
                                else
                                {
                                    oPersonalAsistenciaDTO = new PersonalAsistenciaDTO() { };
                                    oPersonalAsistenciaDTO.ProfesionalFitness = profesorFit;
                                    oPersonalAsistenciaDTO.PersonalAdministrativo = personalAdm;
                                }
                            }
                            break;


                        default:
                            {
                                oPersonalAsistenciaDTO = new PersonalAsistenciaDTO();
                            }
                            break;
                    }

                    oRespItemPersonalAsistenciaDTO.Item = new PersonalAsistenciaDTO();
                    oRespItemPersonalAsistenciaDTO.Item = oPersonalAsistenciaDTO;
                    oRespItemPersonalAsistenciaDTO.Success = true;
                    oRespItemPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPersonalAsistenciaDTO.Success = false;
                    oRespItemPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPersonalAsistenciaDTO;
        }

        //-------------------------------------------------------------------
        //Nombre:	ExecuteTransac
        //Objetivo: Almacena el registro de un objeto de tipo PersonalAsistenciaDTO
        //Valores Prueba:
        //-------------------------------------------------------------------
        public RespPersonalAsistenciaDTO ExecuteTransac(ReqPersonalAsistenciaDTO oReqPersonalAsistenciaDTO)
        {
            RespPersonalAsistenciaDTO oRespPersonalAsistenciaDTO = new RespPersonalAsistenciaDTO();

            oRespPersonalAsistenciaDTO.MessageList = new List<Mensaje>();
            oRespPersonalAsistenciaDTO.List = new List<PersonalAsistenciaDTO>();
            oRespPersonalAsistenciaDTO.User = oReqPersonalAsistenciaDTO.User;

            if (String.IsNullOrEmpty(oReqPersonalAsistenciaDTO.User))
            {
                oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PersonalAsistencia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }


            int Indice = 0;
            foreach (PersonalAsistenciaDTO item in oReqPersonalAsistenciaDTO.List)
            {
                Indice += 1;
                switch (item.Operation)
                {
                    case Operation.RegistrarAsistenciaProfesionalFitness:
                        #region ProfesionalFitness
                        if (string.IsNullOrEmpty(item.CodigoProfesional))
                        {
                            ProfesionalFitnessDTO profesionalFitness = new ProfesionalFitnessDTO();
                            profesionalFitness.CodigoProfesional = item.CodigoProfesional;
                            
                            if (profesionalFitness != null)
                            {                                
                                item.ProfesionalFitness = profesionalFitness;
                            }
                        }

                        #endregion
                        break;
                    case Operation.RegistrarAsistenciaPersonalAdministrativo:
                        #region Personal Administrativo
                        if (string.IsNullOrEmpty(item.CodigoProfesional))
                        {
                            PersonalAdministrativoDTO personalAdministrativo = new PersonalAdministrativoDTO();
                            personalAdministrativo = oPersonalAdministrativoData.uspBuscarPersonalAsistenciaConfiguracionPorDNI(new PersonalAdministrativoDTO()
                            {
                                NumeroDocumento = item.NumeroDocumento,
                                CodigoSede = item.CodigoSede,
                                CodigoUnidadNegocio = item.CodigoUnidadNegocio
                            });

                            if (personalAdministrativo != null)
                            {
                                item.CodigoPersonal = personalAdministrativo.CodigoPersonalAdministrativo;
                                item.PersonalAdministrativo = personalAdministrativo;
                            }
                        }
                        #endregion
                        break;
                }
                if (item.Operation == Operation.RegistrarAsistenciaProfesionalFitness || item.Operation == Operation.RegistrarAsistenciaPersonalAdministrativo)
                {
                    if (!string.IsNullOrEmpty(item.CodigoProfesional) || !string.IsNullOrEmpty(item.CodigoPersonal))
                    {
                        var personalAsistenciaDTO = new PersonalAsistenciaDTO()
                        {
                            CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                            CodigoSede = item.CodigoSede,
                        };
                        //List<HorarioClasesDTO> horarioProfesor = new List<HorarioClasesDTO>();

                        switch (item.Operation)
                        {
                            case Operation.RegistrarAsistenciaProfesionalFitness:
                                //personalAsistenciaDTO.CodigoPersonal = item.ProfesionalFitness.CodigoProfesional;
                                //personalAsistenciaDTO.CodigoHorarioClases = item.CodigoHorarioClases;
                                //item.CodigoPersonal = item.ProfesionalFitness.CodigoProfesional;
                                //horarioProfesor = oHorarioClasesData.ListarPorCodigoProfesionalFitnessDelDia(new HorarioClasesDTO()
                                //{
                                //    CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                                //    CodigoSede = item.CodigoSede,
                                //    CodigoProfesional = item.CodigoProfesional
                                //});

                                break;
                            case Operation.RegistrarAsistenciaPersonalAdministrativo:
                                personalAsistenciaDTO.CodigoPersonal = item.PersonalAdministrativo.CodigoPersonalAdministrativo;
                                break;
                        }
                        if (item.Operation == Operation.RegistrarAsistenciaPersonalAdministrativo)
                        {
                            var asistencia = oPersonalAsistenciaData.BuscarPorCodigoPersonalAsistencia(personalAsistenciaDTO);
                            if (asistencia != null)
                            {
                                    if (item.OperacionMarcacion == 0)
                                    {
                                        if (!asistencia.FechaHoraRefrigerioSalida.HasValue)
                                        {
                                            item.OperacionMarcacion = 2;
                                        }
                                        else if (!asistencia.FechaHoraRefrigerioRetorno.HasValue)
                                        {
                                            item.OperacionMarcacion = 3;
                                        }
                                        else if (!asistencia.FechaHoraSalida.HasValue)
                                        {
                                            item.OperacionMarcacion = 4;
                                        }
                                        else if (!asistencia.FechaHoraIngreso_TurnoTarde.HasValue)
                                        {
                                            item.OperacionMarcacion = 5;
                                        }
                                        else if (!asistencia.FechaHoraRefrigerioSalida_TurnoTarde.HasValue)
                                        {
                                            item.OperacionMarcacion = 6;
                                        }
                                        else if (!asistencia.FechaHoraRefrigerioRetorno_TurnoTarde.HasValue)
                                        {
                                            item.OperacionMarcacion = 7;
                                        }
                                        else if (!asistencia.FechaHoraSalida_TurnoTarde.HasValue)
                                        {
                                            item.OperacionMarcacion = 8;
                                        }
                                    }

                                    if (item.OperacionMarcacion == 2)
                                    {
                                        #region Salida Refrigerio
                                        if (!asistencia.FechaHoraRefrigerioSalida.HasValue)
                                        {
                                            item.Operation = Operation.Update;
                                            item.CodigoPersonalAsistencia = asistencia.CodigoPersonalAsistencia;
                                            item.FechaHoraRefrigerioSalida = DateTime.Now;
                                            item.FechaHoraRefrigerioRetorno = null;
                                            item.FechaHoraSalida = null;

                                            item.FechaHoraIngreso_TurnoTarde = null;
                                            item.FechaHoraRefrigerioSalida_TurnoTarde = null;
                                            item.FechaHoraRefrigerioRetorno_TurnoTarde = null;
                                            item.FechaHoraSalida_TurnoTarde = null;
                                        }
                                        else
                                        {
                                            item.FechaHoraSalida = asistencia.FechaHoraSalida;
                                            oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                            {
                                                Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                Detalle = "personal con el Nro " + item.NumeroDocumento + " ya marco su salida a refrigerio.!",
                                                Tipo = TipoMensaje.Error
                                            });
                                        }
                                        #endregion

                                    }
                                    else if (item.OperacionMarcacion == 3)
                                    {
                                        #region Retorno Refrigerio
                                        if (!asistencia.FechaHoraRefrigerioRetorno.HasValue)
                                        {
                                            item.Operation = Operation.Update;
                                            item.CodigoPersonalAsistencia = asistencia.CodigoPersonalAsistencia;
                                            item.FechaHoraRefrigerioRetorno = DateTime.Now;
                                            item.FechaHoraSalida = null;

                                            item.FechaHoraIngreso_TurnoTarde = null;
                                            item.FechaHoraRefrigerioSalida_TurnoTarde = null;
                                            item.FechaHoraRefrigerioRetorno_TurnoTarde = null;
                                            item.FechaHoraSalida_TurnoTarde = null;
                                        }
                                        else
                                        {
                                            item.FechaHoraSalida = asistencia.FechaHoraSalida;
                                            oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                            {
                                                Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                Detalle = "personal con el Nro " + item.NumeroDocumento + " ya marco su salida a refrigerio.!",
                                                Tipo = TipoMensaje.Error
                                            });
                                        }
                                        #endregion

                                    }
                                    else if (item.OperacionMarcacion == 4)
                                    {
                                        #region Salida
                                        if (!asistencia.FechaHoraSalida.HasValue)
                                        {
                                            item.Operation = Operation.Update;
                                            item.CodigoPersonalAsistencia = asistencia.CodigoPersonalAsistencia;
                                            item.FechaHoraSalida = DateTime.Now;

                                            item.FechaHoraIngreso_TurnoTarde = null;
                                            item.FechaHoraRefrigerioSalida_TurnoTarde = null;
                                            item.FechaHoraRefrigerioRetorno_TurnoTarde = null;
                                            item.FechaHoraSalida_TurnoTarde = null;
                                        }
                                        else
                                        {
                                            item.FechaHoraSalida = asistencia.FechaHoraSalida;
                                            oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                            {
                                                Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                Detalle = "personal con el Nro " + item.NumeroDocumento + " ya marco su salida a refrigerio.!",
                                                Tipo = TipoMensaje.Error
                                            });
                                        }
                                        #endregion
                                    }
                                    else if (item.OperacionMarcacion == 5)
                                    {
                                        #region Salida turno tarde
                                        if (!asistencia.FechaHoraIngreso_TurnoTarde.HasValue)
                                        {
                                            item.Operation = Operation.Update;
                                            item.CodigoPersonalAsistencia = asistencia.CodigoPersonalAsistencia;

                                            item.FechaHoraIngreso_TurnoTarde = DateTime.Now;
                                            item.FechaHoraRefrigerioSalida_TurnoTarde = null;
                                            item.FechaHoraRefrigerioRetorno_TurnoTarde = null;
                                            item.FechaHoraSalida_TurnoTarde = null;
                                        }
                                        else
                                        {
                                            item.FechaHoraIngreso_TurnoTarde = asistencia.FechaHoraIngreso_TurnoTarde;
                                            oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                            {
                                                Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                Detalle = "personal con el Nro " + item.NumeroDocumento + " ya marco su salida a refrigerio.!",
                                                Tipo = TipoMensaje.Error
                                            });
                                        }
                                        #endregion
                                    }
                                    else if (item.OperacionMarcacion == 6)
                                    {
                                        #region Salida Refrigerio Tarde
                                        if (!asistencia.FechaHoraRefrigerioSalida_TurnoTarde.HasValue)
                                        {
                                            item.Operation = Operation.Update;
                                            item.CodigoPersonalAsistencia = asistencia.CodigoPersonalAsistencia;

                                            item.FechaHoraRefrigerioSalida_TurnoTarde = DateTime.Now;
                                            item.FechaHoraRefrigerioRetorno_TurnoTarde = null;
                                            item.FechaHoraSalida_TurnoTarde = null;
                                        }
                                        else
                                        {
                                            item.FechaHoraRefrigerioSalida_TurnoTarde = asistencia.FechaHoraRefrigerioSalida_TurnoTarde;
                                            oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                            {
                                                Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                Detalle = "personal con el Nro " + item.NumeroDocumento + " ya marco su salida a refrigerio.!",
                                                Tipo = TipoMensaje.Error
                                            });
                                        }
                                        #endregion
                                    }
                                    else if (item.OperacionMarcacion == 7)
                                    {
                                        #region Retorno Refrigerio Tarde
                                        if (!asistencia.FechaHoraRefrigerioRetorno_TurnoTarde.HasValue)
                                        {
                                            item.Operation = Operation.Update;
                                            item.CodigoPersonalAsistencia = asistencia.CodigoPersonalAsistencia;

                                            item.FechaHoraRefrigerioRetorno_TurnoTarde = DateTime.Now;
                                            item.FechaHoraSalida_TurnoTarde = null;
                                        }
                                        else
                                        {
                                            item.FechaHoraRefrigerioRetorno_TurnoTarde = asistencia.FechaHoraRefrigerioRetorno_TurnoTarde;
                                            oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                            {
                                                Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                Detalle = "personal con el Nro " + item.NumeroDocumento + " ya marco su salida a refrigerio.!",
                                                Tipo = TipoMensaje.Error
                                            });
                                        }
                                        #endregion
                                    }
                                    else if (item.OperacionMarcacion == 8)
                                    {
                                        #region Salida Tarde
                                        if (!asistencia.FechaHoraSalida_TurnoTarde.HasValue)
                                        {
                                            item.Operation = Operation.Update;
                                            item.CodigoPersonalAsistencia = asistencia.CodigoPersonalAsistencia;

                                            item.FechaHoraSalida_TurnoTarde = DateTime.Now;
                                        }
                                        else
                                        {
                                            item.FechaHoraSalida_TurnoTarde = asistencia.FechaHoraSalida_TurnoTarde;
                                            oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                            {
                                                Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                Detalle = "personal con el Nro " + item.NumeroDocumento + " ya marco su salida a refrigerio.!",
                                                Tipo = TipoMensaje.Error
                                            });
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        if (asistencia.FechaHoraIngreso.HasValue)
                                        {
                                            var FechaActual = DateTime.UtcNow.AddHours(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["numeroZonaHorario"]));
                                            item.FechaHoraIngreso = asistencia.FechaHoraIngreso;
                                            item.FechaHoraSalida = asistencia.FechaHoraSalida;
                                            TimeSpan sDiferencia = FechaActual.Subtract(asistencia.FechaHoraIngreso.Value.AddMinutes(30));
                                            if (sDiferencia.TotalMinutes <= 0)
                                            {
                                                oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                                {
                                                    Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                    Detalle = "personal con el nro " + item.NumeroDocumento + " ya marco su asistencia.!",
                                                    Tipo = TipoMensaje.Error
                                                });
                                            }
                                            else
                                            {

                                                if (!asistencia.FechaHoraSalida.HasValue)
                                                {
                                                    item.Operation = Operation.Update;
                                                    item.CodigoPersonalAsistencia = asistencia.CodigoPersonalAsistencia;
                                                }
                                                else if (FechaActual.Subtract(asistencia.FechaHoraSalida.Value.AddMinutes(30)).TotalMinutes <= 0)
                                                {
                                                    item.FechaHoraSalida = asistencia.FechaHoraSalida;
                                                    oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                                    {
                                                        Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                                        Detalle = "personal con el Nro " + item.NumeroDocumento + " ya marco su salida.!",
                                                        Tipo = TipoMensaje.Error
                                                    });
                                                }

                                            }

                                        }
                                    }                                
                            }
                            else
                            {
                                if (item.OperacionMarcacion > 1)
                                {
                                    oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                    {
                                        Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                        Detalle = "No se realizo marcacion de asistencia:" + item.NumeroDocumento,
                                        Tipo = TipoMensaje.Error
                                    });
                                }

                            }
                        }
                       
                    }
                    else
                    {
                        oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                            Detalle = "No existe personal con el nro. documento :" + item.NumeroDocumento,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }
                oRespPersonalAsistenciaDTO.List.Add(item);
            }

            if (oRespPersonalAsistenciaDTO.MessageList.Count == 0)
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (PersonalAsistenciaDTO item in oReqPersonalAsistenciaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.RegistrarAsistenciaProfesionalFitness:

                                    if (!string.IsNullOrEmpty(item.CodigoProfesional))
                                    {
                                        oPersonalAsistenciaData.CentroEntrenamiento_UspRegistrarPersonalEventualAsistencia(item);                                      
                                    }
                                    else
                                    {
                                        oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                        {
                                            Codigo = (400 + oRespPersonalAsistenciaDTO.MessageList.Count),
                                            Detalle = "Falta ingresar el codigo de un profesor.",
                                            Tipo = TipoMensaje.Error
                                        });
                                    }
                                    break;
                                case Operation.RegistrarAsistenciaPersonalAdministrativo:
                                    if (!string.IsNullOrEmpty(item.CodigoPersonal))
                                    {
                                        oPersonalAsistenciaData.Registrar(item);
                                    }
                                    break;
                                case Operation.Update:
                                    if (!string.IsNullOrEmpty(item.CodigoPersonalAsistencia))
                                    {
                                        oPersonalAsistenciaData.Actualizar(item);

                                    }
                                    break;
                                case Operation.ActualizarAsistenciaPersonal:
                                    if (!string.IsNullOrEmpty(item.CodigoPersonalAsistencia))
                                    {
                                        oPersonalAsistenciaData.ActualizarPersonalAsistencia(item);

                                    }
                                    break;
                                case Operation.Delete:
                                    oPersonalAsistenciaData.Eliminar(item);
                                    break;
                            }
                        }
                        if (oRespPersonalAsistenciaDTO.MessageList.Count == 0)
                        {
                            tx.Complete();
                            oRespPersonalAsistenciaDTO.Success = true;
                            oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = 100,
                                Detalle = "Proceso Grabado Correctamente.",
                                Tipo = TipoMensaje.Informacion
                            });
                        }
                        else
                        {
                            tx.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPersonalAsistenciaDTO.Success = false;
                        oRespPersonalAsistenciaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPersonalAsistenciaDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}

