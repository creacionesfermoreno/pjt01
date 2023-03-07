
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO:AuditoriaDTO
    { 
        public int FlagCantidadReservaFecha { get; set; }
        public string CodigoHorarioClasesConfiguracion { get; set; }
        public string CodigoHorarioClasesTiempoReal { get; set; }
        public string CodigoPersonalAsistencia { get; set; }
        public DateTime FechaHoraIngreso { get; set; }     
        public DateTime FechaHoraSalida { get; set; }   
        public string CodigoHorarioClasesConfiguracionAsistencias { get; set; }        
        public int CodigoSocio { get; set; }
        public int CodigoMembresia { get; set; }
        public int CodigoPaquete { get; set; }
        public DateTime FechaHoraReserva { get; set; }
        public int validacionCancelarCita { get; set; }

        public int validacionTieneReservaHoy { get; set; }
        public int validacionTieneReservaDespues1 { get; set; }
        public int validacionTieneReservaDespues2 { get; set; }
        public int CodigoDisciplinaSala            { get; set; }

        public string Disciplina { get; set; }
        public string DesSala { get; set; }
        public string CodigoProfesional               { get; set; }
        public int NroSala                         { get; set; }
        public DateTime HoraInicio                      { get; set; }
        public DateTime HoraFin                         { get; set; }

        public decimal CostoPorClase { get; set; }
        public decimal DescuentoPorminuto { get; set; }

        public string FechaInicioTexto
        {
            get
            {
                return HoraInicio.ToString("dd-MM-yyyy");
            }
        }

        public string FechaFinTexto
        {
            get
            {
                return HoraFin.ToString("dd-MM-yyyy");
            }
        }

        public string HoraInicioTexto
        {
            get
            {
                return HoraInicio.ToString("hh:mm tt");
            }
        }
        
        public string HoraFinTexto
        {
            get
            {
                return HoraFin.ToString("hh:mm tt");
            }
        }

        public int CapacidadPermitida { get; set; }
        public int CantidadAsistencias { get; set; }
        public int CantidadPlazas
        {
            get
            {
                return (CapacidadPermitida - CantidadAsistencias);
            }
        }


        public int EventoBoton
        {
            get
            {
                var _EventoBoton = 0;

                if (CodigoHorarioClasesConfiguracionAsistencias != null)
                {
                    _EventoBoton = 2; // "EVENTO CANCELAR";
                }
                else
                {
                    if (FlagCantidadReservaFecha > 0)
                    {
                        _EventoBoton = 4; //"EVENTO MENSAJE TIENE RESERVA EN LA FECHA SELECCIONADA";
                    }
                    else
                    {
                        if ((CapacidadPermitida - CantidadAsistencias) <= 0)
                        {
                            _EventoBoton = 3; //"EVENTO MENSAJE OCUPADO O NRO DE CUPOS LLENOS O LLEGO AL LIMITE DE PLAZAS";
                        }
                        else if ((CapacidadPermitida - CantidadAsistencias) > 0)
                        {
                            _EventoBoton = 1; //"EVENTO RESERVAR";
                        }
                    }
                   
                }

                return _EventoBoton;
            }
        }

        public string MensajeBoton
        {
            get
            {
                var _EventoBoton = string.Empty;

                if (CodigoHorarioClasesConfiguracionAsistencias != null)
                {
                    _EventoBoton = string.Empty; // "EVENTO CANCELAR";
                }
                else
                {
                    if (FlagCantidadReservaFecha > 0)
                    {
                        _EventoBoton = "LO SENTIMOS NO PUEDES RESERVAR, YA TIENES UNA RESERVA EN ESTA FECHA SELECCIONADA."; //"EVENTO MENSAJE TIENE RESERVA EN LA FECHA SELECCIONADA";
                    }
                    else
                    {
                        if ((CapacidadPermitida - CantidadAsistencias) <= 0)
                        {
                            _EventoBoton = "LO SENTIMOS, NO HAY CUPOS DISPONIBLES EN ESTA CLASE."; //"EVENTO MENSAJE OCUPADO O NRO DE CUPOS LLENOS O LLEGO AL LIMITE DE PLAZAS";
                        }
                        else if ((CapacidadPermitida - CantidadAsistencias) > 0)
                        {
                            _EventoBoton = string.Empty; //"EVENTO RESERVAR";
                        }
                    }

                }

                return _EventoBoton;
            }
        }


        public string EstadoReserva
        {
            get
            {
                var _EstadoReserva = string.Empty;

                if (CodigoHorarioClasesConfiguracionAsistencias != null)
                {
                    _EstadoReserva = "CANCELAR";
                }
                else
                {
                    if (FlagCantidadReservaFecha > 0)
                    {
                        _EstadoReserva = "NO DISPONIBLE"; 
                    }
                    else
                    {
                        if ((CapacidadPermitida - CantidadAsistencias) <= 0)
                        {
                            _EstadoReserva = "OCUPADO";
                        }
                        else if ((CapacidadPermitida - CantidadAsistencias) > 0)
                        {
                            _EstadoReserva = "RESERVAR";
                        }
                    }

                }

                return _EstadoReserva;
            }
        }

        public string ColorReserva {
            get {
                var _ColorReserva = string.Empty;
                if (CodigoHorarioClasesConfiguracionAsistencias != null)
                {
                    _ColorReserva = "#FF0000";
                }
                else
                {

                    if (FlagCantidadReservaFecha > 0)
                    {
                        _ColorReserva = "#EFEFF4";
                    }
                    else
                    {
                        if ((CapacidadPermitida - CantidadAsistencias) == 0)
                        {
                            _ColorReserva = "#808080";
                        }
                        else if ((CapacidadPermitida - CantidadAsistencias) > 0)
                        {
                            _ColorReserva = "#4CD964";
                        }
                    }
                 
                }
              
                return _ColorReserva;
            }
        }

        public int TipoSala { get; set; }
        public int CodigoSala { get; set; }

        public int DiaNumero { get; set; }
        public string DiaNombre { get; set; }
        public bool Estado { get; set; }
        public string DescripcionEstado { get; set; }      
        public CentroEntrenamiento_ProfesorDTO ProfesionalFitness { get; set; }
        public string PhotoProfesionalFitness { get; set; }
        public string NombreProfesionalFitness { get; set; }
        public string DNIProfesionalFitness { get; set; }
        public string Color { get; set; }        
        public string Celular { get; set; }
        public string EstadoCelular { get; set; }
        public string Whatsapp { get; set; }           
        public string Correo { get; set; }        
        public string Direccion { get; set; }        
        public DateTime FechaNacimiento { get; set; }        
        public string DesDia { get; set; }        
        public Common.Operation Operation { get; set; }
        
        public string Accion { get; set; }

        public DateTime FechaHoy { get; set; }
        public DateTime FechaDespues1 { get; set; }
        public DateTime FechaDespues2 { get; set; }

        public int DiaSemanaHoy { get; set; }
        public int DiaSemana1 { get; set; }
        public int DiaSemana2 { get; set; }
        public string FechaHoyTexto
        {
            get
            {
                return FechaHoy.ToString("dd/MM/yyy");
            }
        }

        public string FechaDespues1Texto
        {
            get
            {
                return FechaDespues1.ToString("dd/MM/yyy");
            }
        }

        public string FechaDespues2Texto
        {
            get
            {
                return FechaDespues2.ToString("dd/MM/yyy");
            }
        }

        public string FechaHoyTextoParametro
        {
            get
            {
                return FechaHoy.ToString("MM/dd/yyy");
            }
        }

        public string FechaDespues1TextoParametro
        {
            get
            {
                return FechaDespues1.ToString("MM/dd/yyy");
            }
        }

        public string FechaDespues2TextoParametro
        {
            get
            {
                return FechaDespues2.ToString("MM/dd/yyy");
            }
        }

        public DateTime FechaHoraReservaInicio_filtro { get; set; }
        public DateTime FechaHoraReservaFin_filtro { get; set; }
        public string Buscador_filtro { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public string ImagenUrl { get; set; }

        public string DesEstado { get; set; }
        public string DesEstadoColor { get; set; }
        public int CantidadTotal { get; set; }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EstadoAlarma { get; set; }
        public string NotaAlarma { get; set; }


        public bool CompartirLinkSala { get; set; }
        public string LinkSala { get; set; }


        public string DesflagAsistio { get; set; }
        public string flagVistaBotonMarcarAsistencia { get; set; }
        public string flagVistaImagenAsistio { get; set; }
    }


    public class ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO : Request
    {
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO : Response
    {

    }

    public class RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO : Response
    {
        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO : Response
    {
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
