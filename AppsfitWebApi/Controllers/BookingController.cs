using System;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

using E_BusinessLayer.CentroEntrenamiento;
using E_BusinessLayer.Gimnasio;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using E_DataModel.Gimnasio;

using AppsfitWebApi.Models;


namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/booking")]
    public class BookingController : ApiController
    {

        //LISTA DE SALAS
        [HttpPost]
        [Route("listarsalasdisponibles")]
        public HttpResponseMessage CentroEntrenamiento_uspListarSalaMaquinas_Presencial(CentroEntrenamiento_Presencial_SalaDTO request)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (request.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }else if (request.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSede.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            ReqFilterCentroEntrenamiento_Presencial_SalaDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_SalaDTO()
            {
                Item = new CentroEntrenamiento_Presencial_SalaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    UsuarioCreacion = "Appsfit"
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_Sala.CentroEntrenamiento_uspListarSalaMaquinas_Presencial,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_SalaDTO oResp = null;

            using (CentroEntrenamiento_Presencial_SalaLogic oCentroEntrenamiento_Presencial_SalaLogic = new CentroEntrenamiento_Presencial_SalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_SalaLogic.CentroEntrenamiento_Presencial_SalaGetList(oReq);
            }

            if (oResp.Success)
            {

                oResp.List.Add(new CentroEntrenamiento_Presencial_SalaDTO { 
                    TipoSala = 1,
                    Descripcion = "SALAS GRUPALES"
                });

                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "Lista de salas cargada correctamente."; //¡El usuario y contraseña son correctos!
                _objResponseModel.Message2 = string.Empty;
                _objResponseModel.Date = oResp.List;
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        private string ObtenerDiaSemana(int dia)
        {
            var NombreSemana = "";
            switch (dia)
            {
                case 1: NombreSemana = "DOMINGO"; break;
                case 2: NombreSemana = "LUNES"; break;
                case 3: NombreSemana = "MARTES"; break;
                case 4: NombreSemana = "MIÉRCOLES"; break;
                case 5: NombreSemana = "JUEVES"; break;
                case 6: NombreSemana = "VIERNES"; break;
                case 7: NombreSemana = "SÁBADO"; break;
                default: NombreSemana = "ERROR"; break;
            }
            return NombreSemana;
        }

        //LISTA DE FECHA
        [HttpPost]
        [Route("listarfechasdisponibles")]
        public HttpResponseMessage CentroEntrenamiento_uspObtenerFechasReservas_Configuracion(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (request.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }else if (request.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSede.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (request.CodigoMembresia == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoMembresia.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (request.CodigoSocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }


            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoMembresia = request.CodigoMembresia,                  
                    CodigoSocio = request.CodigoSocio,
                    FechaHoraReserva = DateTime.Now //request.FechaHoraReserva
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspObtenerFechasReservas_Configuracion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                List<CentroEntrenamiento_FechasDisponibles> lista = new List<CentroEntrenamiento_FechasDisponibles>();

                if (oResp.Item != null)
                {
                   
                    CentroEntrenamiento_FechasDisponibles fila = new CentroEntrenamiento_FechasDisponibles();
                    //DIA 1
                    fila.DiaSemana = oResp.Item.DiaSemanaHoy;
                    fila.DiaSemanaTexto = ObtenerDiaSemana(oResp.Item.DiaSemanaHoy);
                    fila.FechaTextoTitulo = oResp.Item.FechaHoyTexto;
                    fila.FechaTextoParametro = oResp.Item.FechaHoyTextoParametro;
                    fila.FlagCantidadReserva = oResp.Item.validacionTieneReservaHoy;
                    lista.Add(fila);

                    //DIA 2
                    if (oResp.Item.DiaSemana1 > 0)
                    {
                        CentroEntrenamiento_FechasDisponibles fila2 = new CentroEntrenamiento_FechasDisponibles();
                        //DIA 1
                        fila2.DiaSemana = oResp.Item.DiaSemana1;
                        fila2.DiaSemanaTexto = ObtenerDiaSemana(oResp.Item.DiaSemana1);
                        fila2.FechaTextoTitulo = oResp.Item.FechaDespues1Texto;
                        fila2.FechaTextoParametro = oResp.Item.FechaDespues1TextoParametro;
                        fila.FlagCantidadReserva = oResp.Item.validacionTieneReservaDespues1;
                        lista.Add(fila2);
                    }

                    //DIA 3
                    if (oResp.Item.DiaSemana2 > 0)
                    {
                        CentroEntrenamiento_FechasDisponibles fila3 = new CentroEntrenamiento_FechasDisponibles();
                        //DIA 2
                        fila3.DiaSemana = oResp.Item.DiaSemana2;
                        fila3.DiaSemanaTexto = ObtenerDiaSemana(oResp.Item.DiaSemana2);
                        fila3.FechaTextoTitulo = oResp.Item.FechaDespues2Texto;
                        fila3.FechaTextoParametro = oResp.Item.FechaDespues2TextoParametro;
                        fila.FlagCantidadReserva = oResp.Item.validacionTieneReservaDespues2;
                        lista.Add(fila3);
                    }

                }

                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "Lista de fechas cargada correctamente."; //¡El usuario y contraseña son correctos!
                _objResponseModel.Message2 = string.Empty;
                _objResponseModel.Date = lista;
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }
        
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
          
        }

        [HttpPost]
        [Route("listarhorarios")]
        //LISTA DE HORARIOS MAQUINAS
        public HttpResponseMessage CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (request.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (request.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSede.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }            
            else if (request.CodigoSocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (request.TipoSala == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro TipoSala.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (request.FechaHoraReserva == null)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro FechaHoraReserva.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala,
                    DiaNumero = request.DiaNumero,
                    CodigoSocio = request.CodigoSocio,
                    FechaHoraReserva = request.FechaHoraReserva,
                    TipoSala = request.TipoSala,// 1 = SALAS GRUPALES , 2 = SALA MAQUINAS
                    HoraInicio = DateTime.Now,//request.HoraInicio,
                    HoraFin = DateTime.Now, //request.HoraFin
                    FlagCantidadReservaFecha = request.FlagCantidadReservaFecha
                },
                FilterCase = request.TipoSala == 1 ? filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb :filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {

                List<HorariosModel> lista = new List<HorariosModel>();
                if (request.TipoSala == 2) //SALA MAQUINAS
                {
                    for (int i = 0; i < oResp.List.Count; i++)
                    {
                        lista.Add(new HorariosModel()
                        {
                            CodigoHorarioClasesConfiguracion = oResp.List[i].CodigoHorarioClasesConfiguracion,
                            CodigoHorarioClasesTiempoReal = oResp.List[i].CodigoHorarioClasesTiempoReal,
                            CapacidadPermitida = oResp.List[i].CapacidadPermitida,
                            CantidadAsistencias = oResp.List[i].CantidadAsistencias,
                            CantidadPlazas = oResp.List[i].CantidadPlazas,
                            EstadoReserva = oResp.List[i].EstadoReserva,
                            ColorReserva = oResp.List[i].ColorReserva,
                            DiaNumero = oResp.List[i].DiaNumero,
                            Disciplina = oResp.List[i].Disciplina,
                            DesSala = oResp.List[i].DesSala,
                            HoraInicio = oResp.List[i].HoraInicio,
                            HoraFin = oResp.List[i].HoraFin,
                            HoraInicioTexto = oResp.List[i].HoraInicioTexto,
                            HoraFinTexto = oResp.List[i].HoraFinTexto,
                            FechaInicioTexto = oResp.List[i].FechaInicioTexto,
                            FechaFinTexto = oResp.List[i].FechaFinTexto,
                            CodigoHorarioClasesConfiguracionAsistencias = oResp.List[i].CodigoHorarioClasesConfiguracionAsistencias,
                            CodigoSocio = oResp.List[i].CodigoSocio,
                            CodigoMembresia = oResp.List[i].CodigoMembresia,
                            CodigoPaquete = oResp.List[i].CodigoPaquete,
                            validacionCancelarCita = oResp.List[i].validacionCancelarCita,                            
                            NombreProfesionalFitness = "MUSCULACIÓN",
                            LinkSala = string.Empty,
                            CompartirLinkSala = false,
                            EventoBoton = oResp.List[i].EventoBoton,
                            MensajeBoton = oResp.List[i].MensajeBoton
                        });
                    }
                }
                else if (request.TipoSala == 1) //SALA GRUPALES
                {
                    for (int i = 0; i < oResp.List.Count; i++)
                    {
                        lista.Add(new HorariosModel()
                        {
                            CodigoHorarioClasesConfiguracion = oResp.List[i].CodigoHorarioClasesConfiguracion,
                            CodigoHorarioClasesTiempoReal = oResp.List[i].CodigoHorarioClasesTiempoReal,
                            CapacidadPermitida = oResp.List[i].CapacidadPermitida,
                            CantidadAsistencias = oResp.List[i].CantidadAsistencias,
                            CantidadPlazas = oResp.List[i].CantidadPlazas,
                            EstadoReserva = oResp.List[i].EstadoReserva,
                            ColorReserva = oResp.List[i].ColorReserva,
                            DiaNumero = oResp.List[i].DiaNumero,
                            Disciplina = oResp.List[i].Disciplina,
                            DesSala = oResp.List[i].DesSala,
                            HoraInicio = oResp.List[i].HoraInicio,
                            HoraFin = oResp.List[i].HoraFin,
                            HoraInicioTexto = oResp.List[i].HoraInicioTexto,
                            HoraFinTexto = oResp.List[i].HoraFinTexto,
                            FechaInicioTexto = oResp.List[i].FechaInicioTexto,
                            FechaFinTexto = oResp.List[i].FechaFinTexto,
                            CodigoHorarioClasesConfiguracionAsistencias = oResp.List[i].CodigoHorarioClasesConfiguracionAsistencias,
                            CodigoSocio = oResp.List[i].CodigoSocio,
                            CodigoMembresia = oResp.List[i].CodigoMembresia,
                            CodigoPaquete = oResp.List[i].CodigoPaquete,
                            validacionCancelarCita = oResp.List[i].validacionCancelarCita,

                            NombreProfesionalFitness = oResp.List[i].NombreProfesionalFitness.ToUpper(),
                            LinkSala = oResp.List[i].LinkSala,
                            CompartirLinkSala = oResp.List[i].CompartirLinkSala,
                            EventoBoton = oResp.List[i].EventoBoton,
                            MensajeBoton = oResp.List[i].MensajeBoton

                        });
                    }
                }
              

                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "Lista de salas cargada correctamente."; 
                _objResponseModel.Message2 = string.Empty;
                _objResponseModel.Date = lista;
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpPost]
        [Route("horariosreservadospendientes")]
        public HttpResponseMessage CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (request.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (request.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSede.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (request.CodigoSocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (request.CodigoMembresia == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoMembresia.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oParametros = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO();
            oParametros.CodigoSocio = request.CodigoSocio;
            oParametros.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oParametros.CodigoSede = request.CodigoSede;
            oParametros.CodigoMembresia = request.CodigoMembresia;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias.CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio,
                User = "appsfit",
                Item = oParametros,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oLogic.CentroEntrenamiento_Presencial_HorarioClasesAsistenciasGetList(oReq);
            }

            if (oResp.Success)
            {              
                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "Lista de salas cargada correctamente."; //¡El usuario y contraseña son correctos!
                _objResponseModel.Message2 = string.Empty;
                _objResponseModel.Date = oResp.List;
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;         
        }


        [HttpPost]
        [Route("reservarclase")]
        //RESERVAR CLASE
        public HttpResponseMessage CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            
            ResponseModel _objResponseModel = new ResponseModel();

            //VALIDACIONES
            bool validacion = true;
            if (request.CodigoHorarioClasesConfiguracion == String.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoHorarioClasesConfiguracion.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }
            else if (request.DiaNumero == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro DiaNumero.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }
            else if (request.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }
            else if (request.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSede.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }
            else if (request.CodigoSocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSocio.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }
            else if (request.CodigoPaquete == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoPaquete.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }

            if (!validacion)
            {
                var responseV = new HttpResponseMessage(HttpStatusCode.OK);
                responseV.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                responseV.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return responseV;
            }

            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal == null ? string.Empty : request.CodigoHorarioClasesConfiguracionTiempoReal,
                CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias == null ? string.Empty : request.CodigoHorarioClasesConfiguracionAsistencias,
                NroCupo = 0,
                CodigoSocio = request.CodigoSocio,
                CodigoInvitado = 0,
                CodigoMembresia = request.CodigoMembresia,
                CodigoPaquete = request.CodigoPaquete,
                FechaReservacion = request.FechaReservacion,
                DiaNumero = request.DiaNumero,
                UsuarioCreacion = "Appsfit",//request.UsuarioCreacion,
                UsuarioReservacion = "Appsfit",//request.UsuarioCreacion,
                Operation = Operation.Create//request. == "N" ? Operation.Create : Operation.Update,
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                List = list,
                User = "Appsfit"
            };

            RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {
                //mensaje = oResp.MessageList[0].Codigo;
                CentroEntrenamiento_EditorPaginaWebDTO oCentroEntrenamiento_EditorPaginaWebDTO = new CentroEntrenamiento_EditorPaginaWebDTO();
                oCentroEntrenamiento_EditorPaginaWebDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
                oCentroEntrenamiento_EditorPaginaWebDTO.CodigoSede = request.CodigoSede;

                oCentroEntrenamiento_EditorPaginaWebDTO = CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(oCentroEntrenamiento_EditorPaginaWebDTO);
                
                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "ACABAS DE RESERVAR TU CLASE EN " + request.Disciplina + ", DE " + request.HoraInicioTexto + " A " + request.HoraFinTexto; 
                _objResponseModel.Message2 = oCentroEntrenamiento_EditorPaginaWebDTO.ReservasNormativa;
                _objResponseModel.Message3 = oCentroEntrenamiento_EditorPaginaWebDTO.ReservasNotas;
                _objResponseModel.Date = null;
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;// "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo, por favor.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        //METODO OBTENER MENSAJES DE RESERVA
        private CentroEntrenamiento_EditorPaginaWebDTO CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva(CentroEntrenamiento_EditorPaginaWebDTO request)
        {
            CentroEntrenamiento_EditorPaginaWebDTO oItemViewModel = null;

            CentroEntrenamiento_EditorPaginaWebDTO oCentroEntrenamiento_EditorPaginaWebDTO = new CentroEntrenamiento_EditorPaginaWebDTO();
            // oCentroEntrenamiento_EditorPaginaWebDTO.CodigoPagina = request.CodigoPagina;
            oCentroEntrenamiento_EditorPaginaWebDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oCentroEntrenamiento_EditorPaginaWebDTO.CodigoSede = request.CodigoSede;

            ReqFilterCentroEntrenamiento_EditorPaginaWebDTO oReq = new ReqFilterCentroEntrenamiento_EditorPaginaWebDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_EditorPaginaWeb.CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva,
                Item = oCentroEntrenamiento_EditorPaginaWebDTO,
                User = "appsfit"
            };
            RespItemCentroEntrenamiento_EditorPaginaWebDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebLogic oCentroEntrenamiento_EditorPaginaWebLogic = new CentroEntrenamiento_EditorPaginaWebLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebLogic.CentroEntrenamiento_EditorPaginaWebGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new CentroEntrenamiento_EditorPaginaWebDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;
        }


        [HttpPost]
        [Route("cancelarclase")]
        //CANCELAR CLASE
        public HttpResponseMessage CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            //int validacion = 0;
            ResponseModel _objResponseModel = new ResponseModel();
            //VALIDACIONES
            bool validacion = true;
            if (request.CodigoHorarioClasesConfiguracion == String.Empty)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = "Falta selecionar una clase.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }
            else if (request.CodigoHorarioClasesConfiguracionTiempoReal == String.Empty)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = "Falta selecionar una clase real.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }else if (request.CodigoHorarioClasesConfiguracionAsistencias == String.Empty)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = "No tienes clase reservada.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }
            else if (request.CodigoSocio == 0)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = "Falta seleccionar un cliente.";
                _objResponseModel.Message2 = "";
                validacion = false;
            }

            if (!validacion)
            {
                var responseV = new HttpResponseMessage(HttpStatusCode.OK);
                responseV.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                responseV.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return responseV;
            }

            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal == null ? string.Empty : request.CodigoHorarioClasesConfiguracionTiempoReal,
                CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias == null ? string.Empty : request.CodigoHorarioClasesConfiguracionAsistencias,
                CodigoSocio = request.CodigoSocio,
                UsuarioCreacion = "Appsfit",
                Operation = Operation.Update
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic.ExecuteTransac(oReq);
            }
           
            if (oResp.Success)
            {              
                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "TÚ RESERVA ESTA CANCELADA, AHORA PUEDES ELEJIR OTRO HORARIO";
                _objResponseModel.Message2 = "";                
                _objResponseModel.Date = null;
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }

    public class CentroEntrenamiento_FechasDisponibles
    {
        public int DiaSemana { get; set; }
        public string DiaSemanaTexto { get; set; }
        public string FechaTextoTitulo { get; set; }
        public string FechaTextoParametro { get; set; }
        public int FlagCantidadReserva { get; set; }

    }

}
