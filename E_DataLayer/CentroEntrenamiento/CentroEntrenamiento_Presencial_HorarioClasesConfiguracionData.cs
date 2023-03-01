using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;

namespace E_DataLayer.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_HorarioClasesConfiguracionData
    {

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendarioChecking(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendarioChecking", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = request.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@TipoSala", System.Data.SqlDbType.Int)).Value = 1;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalNombre")))
                                {
                                    itemDTO.NombreProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalNombre")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroDocumento")))
                                {
                                    itemDTO.DNIProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("NroDocumento")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalPhoto")))
                                {
                                    itemDTO.PhotoProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("ProfesionalPhoto")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }


        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = request.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@TipoSala", System.Data.SqlDbType.Int)).Value = 1;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalNombre")))
                                {
                                    itemDTO.NombreProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalNombre")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroDocumento")))
                                {
                                    itemDTO.DNIProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("NroDocumento")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalPhoto")))
                                {
                                    itemDTO.PhotoProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("ProfesionalPhoto")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            
            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = request.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReserva", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReserva;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    cmd.Parameters.Add(new SqlParameter("@TipoSala", System.Data.SqlDbType.Int)).Value = 1;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = request.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Asistencias")))
                                {
                                    itemDTO.CantidadAsistencias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Asistencias")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }

                                itemDTO.CompartirLinkSala = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("CompartirLinkSala")]);

                                itemDTO.LinkSala = oIDataReader[oIDataReader.GetOrdinal("LinkSala")].ToString();
                               
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalNombre")))
                                {
                                    itemDTO.NombreProfesionalFitness = (oIDataReader[oIDataReader.GetOrdinal("ProfesionalNombre")].ToString());
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroDocumento")))
                                {
                                    itemDTO.DNIProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("NroDocumento")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalPhoto")))
                                {
                                    itemDTO.PhotoProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("ProfesionalPhoto")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }                             

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSocio")))
                                {
                                    itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoMembresia")))
                                {
                                    itemDTO.CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoPaquete")))
                                {
                                    itemDTO.CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("validacionCancelarCita")))
                                {
                                    itemDTO.validacionCancelarCita = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("validacionCancelarCita")]);
                                }

                                itemDTO.FlagCantidadReservaFecha = request.FlagCantidadReservaFecha;

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request, Paging paging)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = request.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = request.Buscador_filtro;

                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReservaInicio", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReservaInicio_filtro;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReservaFin", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReservaFin_filtro;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = request.Estado;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionTiempoReal")].ToString();
                                itemDTO.CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString();
                                itemDTO.FechaHoraReserva = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraReserva")]);
                                itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                itemDTO.Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString();
                                itemDTO.Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString();
                                itemDTO.Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString();
                                itemDTO.DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString();
                                itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                //itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                itemDTO.DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString();
                                itemDTO.DesEstadoColor = oIDataReader[oIDataReader.GetOrdinal("DesEstadoColor")].ToString();
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        //LISTA DE PERSONAS RESERVARON SE VISUALIZARA EN EL MODULO CLIENTES
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionChecking(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionChecking", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = request.CodigoSede;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                                    
                                itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionTiempoReal")].ToString();
                                itemDTO.CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString();
                                itemDTO.FechaHoraReserva = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraReserva")]);
                                itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                itemDTO.Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString();
                                itemDTO.Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString();
                                itemDTO.Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString();
                                itemDTO.EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("EstadoCelular")].ToString();
                                itemDTO.Whatsapp = oIDataReader[oIDataReader.GetOrdinal("Whatsapp")].ToString();
                                itemDTO.DNI = oIDataReader[oIDataReader.GetOrdinal("DNI")].ToString();
                                itemDTO.ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("ImagenUrl")].ToString();
                                itemDTO.CodigoSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSala")]);
                                itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]);
                                itemDTO.CantidadAsistencias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Asistencias")]);
                                itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                //itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                               // itemDTO.DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString();
                                itemDTO.EstadoAlarma = oIDataReader[oIDataReader.GetOrdinal("EstadoAlarma")].ToString();
                                itemDTO.NotaAlarma = oIDataReader[oIDataReader.GetOrdinal("NotaAlarma")].ToString();
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            
            return lista;
        }

        //LISTA DE CLASES CREADAS EN TIEMPO REAL CLASES GRUPALES Y DE MAQUINAS

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request, Paging paging)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReservaInicio_filtro;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReservaFin_filtro;
                    cmd.Parameters.Add(new SqlParameter("@TipoSala", System.Data.SqlDbType.Int)).Value = request.TipoSala;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")].ToString();
                                }
                                //if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                //{
                                //    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                //}
                               
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }

                                itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Asistencias")))
                                {
                                    itemDTO.CantidadAsistencias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Asistencias")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Estado")))
                                {
                                    itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                }

                                itemDTO.DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString();

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReservaInicio_filtro;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReservaFin_filtro;
                    cmd.Parameters.Add(new SqlParameter("@TipoSala", System.Data.SqlDbType.Int)).Value = request.TipoSala;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                                {
                                    CantidadTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        //LISTAR LOS HORARIOS DE LA SALA DE MAQUINAS QUE HAN SIDO RESERVADAS DESDE HOY PARA ADELANTE
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSala;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSala")))
                                {
                                    itemDTO.CodigoSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSala")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNombre")))
                                {
                                    itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Asistencias")))
                                {
                                    itemDTO.CantidadAsistencias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Asistencias")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesEstado")))
                                {
                                    itemDTO.DesEstado = oIDataReader[oIDataReader.GetOrdinal("DesEstado")].ToString();
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }


        //LISTAR DE HORARIO DEL PROFESOR AL MOMENTO DE BUSCAR PARA MARCAR SU ASISTENCIA
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> ListarPorCodigoProfesionalFitnessDelDia(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oHorarioClases)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesPorCodigoProfesionalFitnessDelDia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClases.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClases.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesionalFitness", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClases.CodigoProfesional;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")].ToString();
                                }
                                itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]);
                                itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]);

                                itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                itemDTO.CantidadAsistencias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Asistencias")]);
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoPersonalAsistencia")))
                                {
                                    itemDTO.CodigoPersonalAsistencia = oIDataReader[oIDataReader.GetOrdinal("CodigoPersonalAsistencia")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraIngreso")))
                                {
                                    itemDTO.FechaHoraIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraIngreso")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraSalida")))
                                {
                                    itemDTO.FechaHoraSalida = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraSalida")]);
                                }

                                list.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            return list;
        }
        
        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = request.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = request.Buscador_filtro;

                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReservaInicio", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReservaInicio_filtro;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReservaFin", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReservaFin_filtro;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = request.Estado;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                                {
                                    CantidadTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadTotal")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        //LISTAR USUARIOS FIT
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request, Paging paging)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = request.CodigoSede;                  
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = request.Buscador_filtro;
                    
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                itemDTO.CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]);
                                itemDTO.CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]);
                                itemDTO.FullName = oIDataReader[oIDataReader.GetOrdinal("FullName")].ToString();
                                itemDTO.UserName = oIDataReader[oIDataReader.GetOrdinal("UserName")].ToString();
                                itemDTO.PasswordHash = oIDataReader[oIDataReader.GetOrdinal("PasswordHash")].ToString();
                                itemDTO.Email = oIDataReader[oIDataReader.GetOrdinal("Email")].ToString();
                                itemDTO.PhoneNumber = oIDataReader[oIDataReader.GetOrdinal("PhoneNumber")].ToString();                                
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = request.CodigoSede;                 
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = request.Buscador_filtro;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                                {
                                    CantidadTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadTotal")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }
        
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = request.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@TipoSala", System.Data.SqlDbType.Int)).Value = 2;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                              
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                               
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                               
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = request.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReserva", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReserva;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = request.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = request.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@TipoSala", System.Data.SqlDbType.Int)).Value = 2;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = request.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = request.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Asistencias")))
                                {
                                    itemDTO.CantidadAsistencias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Asistencias")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSocio")))
                                {
                                    itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoMembresia")))
                                {
                                    itemDTO.CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoPaquete")))
                                {
                                    itemDTO.CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("validacionCancelarCita")))
                                {
                                    itemDTO.validacionCancelarCita = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("validacionCancelarCita")]);
                                }

                                itemDTO.FlagCantidadReservaFecha = request.FlagCantidadReservaFecha;
                            
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = request.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("TipoSala")))
                                {
                                    itemDTO.TipoSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoSala")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("NroSala")))
                                {
                                    itemDTO.NroSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroSala")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("ProfesionalNombre")))
                                {
                                    itemDTO.NombreProfesionalFitness = oIDataReader[oIDataReader.GetOrdinal("ProfesionalNombre")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Asistencias")))
                                {
                                    itemDTO.CantidadAsistencias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Asistencias")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSocio")))
                                {
                                    itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoMembresia")))
                                {
                                    itemDTO.CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoPaquete")))
                                {
                                    itemDTO.CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("validacionCancelarCita")))
                                {
                                    itemDTO.validacionCancelarCita = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("validacionCancelarCita")]);
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }

            return lista;
        }


        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = request.DiaNumero;                  
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = request.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = request.HoraFin;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Estado")))
                                {
                                    itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                              
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }
                                
                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSala;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                var itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Estado")))
                                {
                                    itemDTO.Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNombre")))
                                {
                                    itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }

                                lista.Add(itemDTO);
                            }
                        }
                    }
                }
            }


            return lista;
        }

        public string CentroEntrenamiento_uspRegistrarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspRegistrarPresencial_HorarioClasesConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar,50)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDisciplinaSala", System.Data.SqlDbType.Int)).Value = item.CodigoDisciplinaSala;

                    if (item.CodigoProfesional == string.Empty)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value ="sinp";
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.CodigoSala;

                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = item.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@CapacidadPermitida", System.Data.SqlDbType.Int)).Value = item.CapacidadPermitida;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@DiaNombre", System.Data.SqlDbType.VarChar, 50)).Value = item.DiaNombre;

                    cmd.Parameters.Add(new SqlParameter("@CostoPorClase", System.Data.SqlDbType.Decimal)).Value = item.CostoPorClase;
                    cmd.Parameters.Add(new SqlParameter("@DescuentoPorminuto", System.Data.SqlDbType.Decimal)).Value = item.DescuentoPorminuto;

                    cmd.Parameters.Add(new SqlParameter("@CompartirLinkSala", System.Data.SqlDbType.Bit)).Value = item.CompartirLinkSala;
                    cmd.Parameters.Add(new SqlParameter("@LinkSala", System.Data.SqlDbType.VarChar, 500)).Value = item.LinkSala;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    resultado = cmd.Parameters["@CodigoHorarioClasesConfiguracion"].Value.ToString();
                }

            }
            return resultado;
        }

        public string CentroEntrenamiento_uspDesactivarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspDesactivarPresencial_HorarioClasesConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                   
                    cmd.ExecuteNonQuery();
                }

            }
            return resultado;
        }

        public void CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar,50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDisciplinaSala", System.Data.SqlDbType.Int)).Value = item.CodigoDisciplinaSala;

                    if (item.CodigoProfesional == string.Empty)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value ="sinp";
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = item.CodigoSala;

                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@HoraFin", System.Data.SqlDbType.DateTime)).Value = item.HoraFin;
                    cmd.Parameters.Add(new SqlParameter("@CapacidadPermitida", System.Data.SqlDbType.Int)).Value = item.CapacidadPermitida;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@DiaNombre", System.Data.SqlDbType.VarChar, 50)).Value = item.DiaNombre;

                    cmd.Parameters.Add(new SqlParameter("@CostoPorClase", System.Data.SqlDbType.Decimal)).Value = item.CostoPorClase;
                    cmd.Parameters.Add(new SqlParameter("@DescuentoPorminuto", System.Data.SqlDbType.Decimal)).Value = item.DescuentoPorminuto;


                    cmd.Parameters.Add(new SqlParameter("@CompartirLinkSala", System.Data.SqlDbType.Bit)).Value = item.CompartirLinkSala;
                    cmd.Parameters.Add(new SqlParameter("@LinkSala", System.Data.SqlDbType.VarChar, 500)).Value = item.LinkSala;

                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                    
                }

            }
            //return resultado;
        }

        public void CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO item)
        {
            string resultado = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();

                }

            }
            //return resultado;
        }


        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSala;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = request.CodigoHorarioClasesConfiguracion;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoDisciplinaSala")))
                                {
                                    itemDTO.CodigoDisciplinaSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoDisciplinaSala")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNombre")))
                                {
                                    itemDTO.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("HoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraFin")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoProfesional")))
                                {
                                    itemDTO.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CostoPorClase")))
                                {
                                    itemDTO.CostoPorClase = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoPorClase")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DescuentoPorminuto")))
                                {
                                    itemDTO.DescuentoPorminuto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DescuentoPorminuto")]);
                                }

                                itemDTO.ProfesionalFitness = new CentroEntrenamiento_ProfesorDTO() { };

                                itemDTO.ProfesionalFitness.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                itemDTO.ProfesionalFitness.NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreProfecionalFitness")].ToString();
                                itemDTO.ProfesionalFitness.Nombres = oIDataReader[oIDataReader.GetOrdinal("NombresProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Apellidos = oIDataReader[oIDataReader.GetOrdinal("ApellidosProfesor")].ToString();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("TipoDocumentoProfesor")))
                                {
                                    itemDTO.ProfesionalFitness.TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumentoProfesor")]);
                                }

                                itemDTO.ProfesionalFitness.NroDocumento = oIDataReader[oIDataReader.GetOrdinal("DNIProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Celular = oIDataReader[oIDataReader.GetOrdinal("CelularProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Telefono = oIDataReader[oIDataReader.GetOrdinal("TelefonoProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Correo = oIDataReader[oIDataReader.GetOrdinal("CorreoProfesor")].ToString();
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaNacimientoProfesor")))
                                {
                                    itemDTO.ProfesionalFitness.FechaNacimiento = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaNacimientoProfesor")]);
                                }

                                itemDTO.ProfesionalFitness.Genero = oIDataReader[oIDataReader.GetOrdinal("GeneroProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Direccion = oIDataReader[oIDataReader.GetOrdinal("DireccionProfesor")].ToString();
                                itemDTO.ProfesionalFitness.Distrito = oIDataReader[oIDataReader.GetOrdinal("DistritoProfesor")].ToString();
                                itemDTO.ProfesionalFitness.ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("PhotoProfecionalFitness")].ToString();

                                itemDTO.ProfesionalFitness.CostoPorClase = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("CostoPorClaseProfesor")]);
                                itemDTO.ProfesionalFitness.DescuentoPorminuto = Convert.ToDecimal(oIDataReader[oIDataReader.GetOrdinal("DescuentoPorminutoProfesor")]);

                                itemDTO.LinkSala = oIDataReader[oIDataReader.GetOrdinal("LinkSala")].ToString();
                                itemDTO.CompartirLinkSala = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("CompartirLinkSala")]);

                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspObtenerFechasReservas_Configuracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspObtenerFechasReservas_Configuracion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReserva", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReserva;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = request.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = request.CodigoMembresia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("validacionTieneReservaHoy")))
                                {
                                    itemDTO.validacionTieneReservaHoy = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("validacionTieneReservaHoy")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("validacionTieneReservaDespues1")))
                                {
                                    itemDTO.validacionTieneReservaDespues1 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("validacionTieneReservaDespues1")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("validacionTieneReservaDespues2")))
                                {
                                    itemDTO.validacionTieneReservaDespues2 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("validacionTieneReservaDespues2")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaSemanaHoy")))
                                {
                                    itemDTO.DiaSemanaHoy = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaSemanaHoy")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaSemana1")))
                                {
                                    itemDTO.DiaSemana1 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaSemana1")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaSemana2")))
                                {
                                    itemDTO.DiaSemana2 = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaSemana2")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoy")))
                                {
                                    itemDTO.FechaHoy = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoy")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaDespues1")))
                                {
                                    itemDTO.FechaDespues1 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaDespues1")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaDespues2")))
                                {
                                    itemDTO.FechaDespues2 = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaDespues2")]);
                                }

                            }
                        }
                    }
                }
            }

            return itemDTO;
        }
        
        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO itemDTO = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = request.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = request.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = request.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReserva", System.Data.SqlDbType.DateTime)).Value = request.FechaHoraReserva;                    
                    cmd.Parameters.Add(new SqlParameter("@TipoSala", System.Data.SqlDbType.Int)).Value = 2;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = request.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSala", System.Data.SqlDbType.Int)).Value = request.CodigoSala;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {                           
                            while (oIDataReader.Read())
                            {
                               
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")))
                                {
                                    itemDTO.CodigoHorarioClasesTiempoReal = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesTiempoReal")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CapacidadPermitida")))
                                {
                                    itemDTO.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CapacidadPermitida")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Asistencias")))
                                {
                                    itemDTO.CantidadAsistencias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Asistencias")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DiaNumero")))
                                {
                                    itemDTO.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("TipoSala")))
                                {
                                    itemDTO.TipoSala = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoSala")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Disciplina")))
                                {
                                    itemDTO.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("DesSala")))
                                {
                                    itemDTO.DesSala = oIDataReader[oIDataReader.GetOrdinal("DesSala")].ToString();
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraInicio")))
                                {
                                    itemDTO.HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraFin")))
                                {
                                    itemDTO.HoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("Color")))
                                {
                                    itemDTO.Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")))
                                {
                                    itemDTO.CodigoHorarioClasesConfiguracionAsistencias = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracionAsistencias")].ToString();
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoSocio")))
                                {
                                    itemDTO.CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoMembresia")))
                                {
                                    itemDTO.CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoPaquete")))
                                {
                                    itemDTO.CodigoPaquete = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPaquete")]);
                                }

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("validacionCancelarCita")))
                                {
                                    itemDTO.validacionCancelarCita = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("validacionCancelarCita")]);
                                }
                                
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }


    }

}
