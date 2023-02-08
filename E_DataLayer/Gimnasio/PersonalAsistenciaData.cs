
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class PersonalAsistenciaData
    {
        public List<PersonalAsistenciaDTO> Listar(PersonalAsistenciaDTO oPersonalAsistenciaDTO, Paging paging, ref uint recordCount)
        {
            List<PersonalAsistenciaDTO> lista = new List<PersonalAsistenciaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPersonalAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAsistenciaDTO.NumeroDocumento;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", System.Data.SqlDbType.VarChar, 200)).Value = oPersonalAsistenciaDTO.NombreCompleto;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oPersonalAsistenciaDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oPersonalAsistenciaDTO.FechaFin;
                    
                }
            }
            return lista;
        }

        public List<PersonalFitnessAsistenciaDTO> ListarAsistenciaPorProfesionalFitness(PersonalAsistenciaDTO oPersonalAsistenciaDTO, Paging paging, ref uint recordCount)
        {
            List<PersonalFitnessAsistenciaDTO> lista = new List<PersonalFitnessAsistenciaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspPersonalAsistenciaListarPorProfesionalFitness", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAsistenciaDTO.NumeroDocumento??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", System.Data.SqlDbType.VarChar, 200)).Value = oPersonalAsistenciaDTO.NombreCompleto ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oPersonalAsistenciaDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oPersonalAsistenciaDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CodigoDisciplina", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoDisciplina;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    SqlParameter sqlOut = new SqlParameter("@TotalRecord", "0");
                    sqlOut.SqlDbType = System.Data.SqlDbType.Int;
                    sqlOut.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(sqlOut);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PersonalFitnessAsistenciaDTO item = new PersonalFitnessAsistenciaDTO();
                              
                                item.NombreProfesionalFitness = reader[reader.GetOrdinal("Profesor")].ToString();
                                item.NumeroDocumento = reader[reader.GetOrdinal("DNIProfesional")].ToString();
                                item.Disciplina = reader[reader.GetOrdinal("Disciplina")].ToString();
                                item.CostoPorClase = Convert.ToDecimal(reader[reader.GetOrdinal("CostoPorClase")]);

                                item.FechaHoraInicioClase = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraInicio")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraIngreso")))
                                {
                                    item.FechaHoraMarcacionIngreso_texto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso")]).ToString("dd/MM/yyyy HH:mm tt");
                                }
                                else
                                {
                                    item.FechaHoraMarcacionIngreso_texto = string.Empty;
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraSalida")))
                                {
                                    item.FechaHoraMarcacionSalida_texto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida")]).ToString("dd/MM/yyyy HH:mm tt");
                                }
                                else
                                {
                                    item.FechaHoraMarcacionSalida_texto = string.Empty;
                                }
                                //item.FechaHoraMarcacionIngreso = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso")]);
                                //item.FechaHoraMarcacionSalida = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida")]);

                                item.Tardanza = Convert.ToInt32(reader[reader.GetOrdinal("Tardanza")]);
                               // item.TotalAlumnos = Convert.ToInt32(reader[reader.GetOrdinal("TotalAlumnos")].ToString());
                                item.DescuentoPorTardanza = Convert.ToDecimal(reader[reader.GetOrdinal("DescuentoPorTardanza")]);
                                item.TotalPago = (item.CostoPorClase - item.DescuentoPorTardanza) < 0 ? 0 : (item.CostoPorClase - item.DescuentoPorTardanza);
                                lista.Add(item);
                            }
                        }
                    }
                    if (cmd.Parameters["@TotalRecord"].Value != null)
                    {
                        recordCount = Convert.ToUInt32(cmd.Parameters["@TotalRecord"].Value);
                    }
                }
            }
            return lista;
        }

        public List<PersonalAsistenciaDTO> ListarAsistenciaPorPersonalAdministrativo(PersonalAsistenciaDTO oPersonalAsistenciaDTO, Paging paging, ref uint recordCount)
        {
            List<PersonalAsistenciaDTO> lista = new List<PersonalAsistenciaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspPersonalAsistenciaListarPorPersonalAdministrativo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 20)).Value = oPersonalAsistenciaDTO.NumeroDocumento??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", System.Data.SqlDbType.VarChar, 200)).Value = oPersonalAsistenciaDTO.NombreCompleto??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oPersonalAsistenciaDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oPersonalAsistenciaDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCargo", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoCargo;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    SqlParameter sqlOut = new SqlParameter("@TotalRecord", "0");
                    sqlOut.SqlDbType = System.Data.SqlDbType.Int;
                    sqlOut.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(sqlOut);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PersonalAsistenciaDTO item = new PersonalAsistenciaDTO();
                                item.NumeroDocumento = reader[reader.GetOrdinal("NumeroDocumento")].ToString();
                                item.NombreCompleto = reader[reader.GetOrdinal("NombreCompleto")].ToString();
                                item.CodigoPersonalAsistencia = (reader[reader.GetOrdinal("CodigoPersonalAsistencia")].ToString());
                                if (!reader.IsDBNull(reader.GetOrdinal("DescripcionCargo")))
                                {
                                    item.DescripcionCargo = reader[reader.GetOrdinal("DescripcionCargo")].ToString();
                                }

                                //item.DesTipoTurno = reader[reader.GetOrdinal("DesTipoTurno")].ToString();
                                if (!reader.IsDBNull(reader.GetOrdinal("IngresoProgramadoTurno1")))
                                {
                                    item.FechaHoraIngresoProgramada = Convert.ToDateTime(reader[reader.GetOrdinal("IngresoProgramadoTurno1")]);
                                }
                                item.FechaHoraIngreso = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraSalida")))
                                {
                                    item.FechaHoraSalida = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida")]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioInicio")))
                                {
                                    item.FechaHoraRefrigerioSalida = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioInicio")].ToString());
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioFin")))
                                {
                                    item.FechaHoraRefrigerioRetorno = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioFin")].ToString());
                                }



                                //--HORARIO 2
                                if (!reader.IsDBNull(reader.GetOrdinal("IngresoProgramadoTurno2")))
                                {
                                    item.FechaHoraIngresoProgramada_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("IngresoProgramadoTurno2")]);
                                }
                                
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraIngreso_TurnoTarde")))
                                {
                                    item.FechaHoraIngreso_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso_TurnoTarde")]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraSalida_TurnoTarde")))
                                {
                                    item.FechaHoraSalida_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida_TurnoTarde")]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioInicio_TurnoTarde")))
                                {
                                    item.FechaHoraRefrigerioSalida_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioInicio_TurnoTarde")]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioFin_TurnoTarde")))
                                {
                                    item.FechaHoraRefrigerioRetorno_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioFin_TurnoTarde")]);
                                }


                                if (!reader.IsDBNull(reader.GetOrdinal("MinutosTardanza")))
                                {
                                    item.TardanzaMinutos = Convert.ToInt32(reader[reader.GetOrdinal("MinutosTardanza")]);
                                    if (item.TardanzaMinutos > 0)
                                    {
                                        if (item.TardanzaMinutos >= 60)
                                        {
                                            int hora = item.TardanzaMinutos % 60;
                                            item.TardanzaTexto = ((item.TardanzaMinutos / 60) + "h " + (item.TardanzaMinutos % 60) + "m");
                                        }
                                        else
                                        {
                                            item.TardanzaTexto = (item.TardanzaMinutos + "m");
                                        }
                                    }
                                }
                                else { 
                                    item.TardanzaTexto = "";
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("MinutosTardanza_Turno2")))
                                {
                                    item.TardanzaMinutos_TurnoTarde = Convert.ToInt32(reader[reader.GetOrdinal("MinutosTardanza_Turno2")]);
                                    if (item.TardanzaMinutos_TurnoTarde > 0)
                                    {
                                        if (item.TardanzaMinutos_TurnoTarde >= 60)
                                        {
                                            int hora = item.TardanzaMinutos_TurnoTarde % 60;
                                            item.TardanzaTexto_TurnoTarde = ((item.TardanzaMinutos_TurnoTarde / 60) + "h " + (item.TardanzaMinutos_TurnoTarde % 60) + "m");
                                        }
                                        else
                                        {
                                            item.TardanzaTexto_TurnoTarde = (item.TardanzaMinutos_TurnoTarde + "m");
                                        }
                                    }
                                }
                                else
                                {
                                    item.TardanzaTexto_TurnoTarde = "";
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("TotalMinutosTardanza")))
                                {
                                    item.TotalTardanzaMinutos = Convert.ToInt32(reader[reader.GetOrdinal("TotalMinutosTardanza")]);
                                    if (item.TotalTardanzaMinutos > 0)
                                    {
                                        if (item.TotalTardanzaMinutos >= 60)
                                        {
                                            int hora = item.TotalTardanzaMinutos % 60;
                                            item.TotalTardanzaTexto = ((item.TotalTardanzaMinutos / 60) + "h " + (item.TotalTardanzaMinutos % 60) + "m");
                                        }
                                        else
                                        {
                                            item.TotalTardanzaTexto = (item.TotalTardanzaMinutos + "m");
                                        }
                                    }
                                }
                                else
                                {
                                    item.TotalTardanzaTexto = "";
                                }

                                lista.Add(item);
                            }

                        }
                    }
                    if (cmd.Parameters["@TotalRecord"].Value != null)
                    {
                        recordCount = Convert.ToUInt32(cmd.Parameters["@TotalRecord"].Value);
                    }

                }
            }
            return lista;
        }
        
        public List<PersonalAdministrativoAsistenciaResumentDTO> ListarPersonalAdministrativoAsistenciaResumen(PersonalAsistenciaDTO oPersonalAsistenciaDTO, Paging paging, ref uint recordCount)
        {
            List<PersonalAdministrativoAsistenciaResumentDTO> lista = new List<PersonalAdministrativoAsistenciaResumentDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPersonalAdministrativoAsistenciaResumen", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAsistenciaDTO.NumeroDocumento??string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@NombreCompleto", System.Data.SqlDbType.VarChar, 200)).Value = oPersonalAsistenciaDTO.NombreCompleto ?? string.Empty;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oPersonalAsistenciaDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oPersonalAsistenciaDTO.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCargo", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoCargo;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;

                    SqlParameter sqlOut = new SqlParameter("@TotalRecord", "0");
                    sqlOut.SqlDbType = System.Data.SqlDbType.Int;
                    sqlOut.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(sqlOut);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PersonalAdministrativoAsistenciaResumentDTO item = new PersonalAdministrativoAsistenciaResumentDTO();
                                item.DNI = reader[reader.GetOrdinal("NumeroDocumento")].ToString();
                                item.NombreCompleto = reader[reader.GetOrdinal("NombreCompleto")].ToString();
                                item.DescripcionCargo = (reader[reader.GetOrdinal("DescripcionCargo")].ToString());
                                item.DiasTrabajados = Convert.ToInt32(reader[reader.GetOrdinal("DiasTrabajado")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("MinutosRefrigerio")))
                                    item.MinutosRefrigerio = Convert.ToInt32(reader[reader.GetOrdinal("MinutosRefrigerio")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("MinutosTrabajados")))
                                    item.MinutosTrabajados = Convert.ToInt32(reader[reader.GetOrdinal("MinutosTrabajados")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("HorasTrabajadas")))
                                    item.HorasTrabajadas = Convert.ToInt32(reader[reader.GetOrdinal("HorasTrabajadas")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("Horas")))
                                    item.HorasTrabajadasText = reader[reader.GetOrdinal("Horas")].ToString();                                
                                if (!reader.IsDBNull(reader.GetOrdinal("MinutosTardanza")))
                                    item.MinutosTardanza = Convert.ToInt32(reader[reader.GetOrdinal("MinutosTardanza")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("Sueldo")))
                                    item.Sueldo = Convert.ToDecimal(reader[reader.GetOrdinal("Sueldo")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("DescuentoPorMinuto")))
                                    item.DescuentoPorMinuto = Convert.ToDecimal(reader[reader.GetOrdinal("DescuentoPorMinuto")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("TotalDescuento")))
                                    item.TotalDescuento = Convert.ToDecimal(reader[reader.GetOrdinal("TotalDescuento")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("SubTotal")))
                                    item.SubTotal = Convert.ToDecimal(reader[reader.GetOrdinal("SubTotal")]);
                                if (!reader.IsDBNull(reader.GetOrdinal("TotalAPagar")))
                                    item.TotalNeto = Convert.ToDecimal(reader[reader.GetOrdinal("TotalAPagar")]);
                                lista.Add(item);
                            }
                        }
                    }
                    if (cmd.Parameters["@TotalRecord"].Value != null)
                    {
                        recordCount = Convert.ToUInt32(cmd.Parameters["@TotalRecord"].Value);
                    }
                }
            }
            return lista;
        }
        
        public List<PersonalAsistenciaDTO> ListarAsistenciaPersonalPorCodigo(PersonalAsistenciaDTO oPersonalAsistenciaDTO, Paging paging, ref uint recordCount)
        {
            List<PersonalAsistenciaDTO> lista = new List<PersonalAsistenciaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarPersonalAsistenciaProfesores", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAsistenciaDTO.CodigoPersonal;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            //item.DNIProfesor = reader.GetValue(reader.GetOrdinal(""))
                            while (reader.Read())
                            {
                                PersonalAsistenciaDTO item = new PersonalAsistenciaDTO();
                                if (!reader.IsDBNull(reader.GetOrdinal("CodigoPersonalAsistencia")))
                                {
                                    item.CodigoPersonalAsistencia = reader[reader.GetOrdinal("CodigoPersonalAsistencia")].ToString();
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraIngreso")))
                                {
                                    item.FechaHoraIngreso = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso")]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraSalida")))
                                {
                                    item.FechaHoraSalida = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida")]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioInicio")))
                                {
                                    item.FechaHoraRefrigerioSalida = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioInicio")]);
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioFin")))
                                {
                                    item.FechaHoraRefrigerioRetorno = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioFin")]);
                                }
                                lista.Add(item);
                            }
                        }
                    }


                }
            }
            return lista;
        }
        
        public List<PersonalAsistenciaDTO> ListarPersonalPorFiltroAutocompletado(PersonalAsistenciaDTO oPersonalAsistenciaDTO)
        {
            List<PersonalAsistenciaDTO> lista = new List<PersonalAsistenciaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspListarPersonalPorDNI", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPersonalAsistenciaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Filtro", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAsistenciaDTO.NombreCompleto ?? string.Empty;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PersonalAsistenciaDTO item = new PersonalAsistenciaDTO();
                                if (!reader.IsDBNull(reader.GetOrdinal("NumeroDocumento")))
                                {
                                    item.NumeroDocumento = reader[reader.GetOrdinal("NumeroDocumento")].ToString();
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("NombreCompleto")))
                                {
                                    item.NombreCompleto = reader[reader.GetOrdinal("NombreCompleto")].ToString();
                                }
                                lista.Add(item);
                            }
                        }
                    }


                }
            }
            return lista;
        }
                      
        public PersonalAsistenciaDTO BuscarPorCodigoPersonalAsistencia(PersonalAsistenciaDTO oPersonalAsistencia)
        {
            PersonalAsistenciaDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarPersonalAsistenciaProfesionalFitnessPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oPersonalAsistencia.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oPersonalAsistencia.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAsistencia.CodigoPersonal;
                    if (!string.IsNullOrEmpty(oPersonalAsistencia.CodigoHorarioClases))
                    {
                        if (oPersonalAsistencia.CodigoHorarioClases.Length > 10)
                        {
                            cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar, 50)).Value = oPersonalAsistencia.CodigoHorarioClases;
                        }

                    }
                    
                    using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new PersonalAsistenciaDTO();
                                itemDTO.CodigoUnidadNegocio = oPersonalAsistencia.CodigoUnidadNegocio;
                                itemDTO.CodigoSede = oPersonalAsistencia.CodigoSede;
                                itemDTO.CodigoProfesional = oPersonalAsistencia.CodigoProfesional;
                                itemDTO.CodigoPersonal = oPersonalAsistencia.CodigoPersonal;
                                itemDTO.CodigoPersonalAsistencia = reader[reader.GetOrdinal("CodigoPersonalAsistencia")].ToString();
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraIngreso")))
                                {
                                    itemDTO.FechaHoraIngreso = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso")].ToString());
                                    itemDTO.FechaHoraIngresoTexto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso")]).ToString("h:mm:ss tt");
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraSalida")))
                                {
                                    itemDTO.FechaHoraSalida = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida")].ToString());
                                    itemDTO.FechaHoraSalidaTexto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida")]).ToString("h:mm:ss tt");
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioInicio")))
                                {
                                    itemDTO.FechaHoraRefrigerioSalida = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioInicio")].ToString());
                                    itemDTO.FechaHoraRefrigerioSalidaTexto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioInicio")]).ToString("h:mm:ss tt");
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioFin")))
                                {
                                    itemDTO.FechaHoraRefrigerioRetorno = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioFin")].ToString());
                                    itemDTO.FechaHoraRefrigerioRetornoTexto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioFin")]).ToString("h:mm:ss tt");
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraIngreso_TurnoTarde")))
                                {
                                    itemDTO.FechaHoraIngreso_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso_TurnoTarde")].ToString());
                                    itemDTO.FechaHoraIngreso_TurnoTardeTexto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraIngreso_TurnoTarde")]).ToString("h:mm:ss tt");
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraSalida_TurnoTarde")))
                                {
                                    itemDTO.FechaHoraSalida_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida_TurnoTarde")].ToString());
                                    itemDTO.FechaHoraSalida_TurnoTardeTexto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraSalida_TurnoTarde")]).ToString("h:mm:ss tt");
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioInicio_TurnoTarde")))
                                {
                                    itemDTO.FechaHoraRefrigerioSalida_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioInicio_TurnoTarde")].ToString());
                                    itemDTO.FechaHoraRefrigerioSalida_TurnoTardeTexto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioInicio_TurnoTarde")]).ToString("h:mm:ss tt");
                                }

                                if (!reader.IsDBNull(reader.GetOrdinal("FechaHoraRefrigerioFin_TurnoTarde")))
                                {
                                    itemDTO.FechaHoraRefrigerioRetorno_TurnoTarde = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioFin_TurnoTarde")].ToString());
                                    itemDTO.FechaHoraRefrigerioRetorno_TurnoTardeTexto = Convert.ToDateTime(reader[reader.GetOrdinal("FechaHoraRefrigerioFin_TurnoTarde")]).ToString("h:mm:ss tt");
                                }
                                //tipoTurno 0 = SIN HORARIO LABORAL, 
                                          //1 = HORARIO LABORAL SOLO MAÑANA, 
                                          //2 = HORARIO LABORAL SOLO TARDE, 
                                          //3 HORARIO LABORAL MAÑANA Y TARDE
                                itemDTO.tipoTurno = Convert.ToInt32(reader[reader.GetOrdinal("tipoTurno")]);
                                

                            }
                        }
                    }
                }
            }
            
            return itemDTO;
        }
        
        public void CentroEntrenamiento_UspRegistrarPersonalEventualAsistencia(PersonalAsistenciaDTO item)
        {
            string campoRetorno = string.Empty;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("CentroEntrenamiento_UspRegistrarPersonalEventualAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                    if (item.CodigoHorarioClasesTiempoReal != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionTiempoReal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesTiempoReal;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracionTiempoReal", System.Data.SqlDbType.VarChar, 50)).Value = DBNull.Value;
                    }

                    if (item.CodigoPersonalAsistencia != null)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAsistencia;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50)).Value = DBNull.Value;
                    }

                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;                    
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@TipoAsistencia", System.Data.SqlDbType.Int)).Value = item.TipoAsistencia;
                    
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.ExecuteNonQuery();                    
                }
            }
            
        }

        public string Registrar(PersonalAsistenciaDTO item)
        {
            string campoRetorno = string.Empty;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarPersonalAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    SqlParameter param = new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50);
                    param.Direction = System.Data.ParameterDirection.Output;
                    param.Value = "";

                    cmd.Parameters.Add(param);
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonal;
                    if (item.FechaHoraIngreso.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraIngreso", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraIngreso;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraIngreso", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    }


                    if (item.FechaHoraSalida.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraSalida", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraSalida;
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraSalida", System.Data.SqlDbType.DateTime)).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.ExecuteNonQuery();
                    campoRetorno = cmd.Parameters["@CodigoPersonalAsistencia"].Value.ToString();
                }
            }
            return campoRetorno;
        }

        public void Actualizar(PersonalAsistenciaDTO item)
        {

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPersonalAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.VarChar, 50)).Value = DBNull.Value;//item.CodigoPersonal;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraIngreso", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraIngreso;

                    if (item.FechaHoraRefrigerioSalida.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraRefrigerioSalida", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraRefrigerioSalida;
                    }
                    if (item.FechaHoraRefrigerioRetorno.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraRefrigerioRetorno", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraRefrigerioRetorno;
                    }
                    if (item.FechaHoraSalida.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraSalida", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraSalida;
                    }

                    if (item.FechaHoraIngreso_TurnoTarde.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraIngreso_TurnoTarde", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraIngreso_TurnoTarde;
                    }

                    if (item.FechaHoraRefrigerioSalida_TurnoTarde.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraRefrigerioInicio_TurnoTarde", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraRefrigerioSalida_TurnoTarde;
                    }

                    if (item.FechaHoraRefrigerioRetorno_TurnoTarde.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraRefrigerioFin_TurnoTarde", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraRefrigerioRetorno_TurnoTarde;
                    }

                    if (item.FechaHoraSalida_TurnoTarde.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraSalida_TurnoTarde", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraSalida_TurnoTarde;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioEdicion ?? item.UsuarioCreacion;
                    cmd.ExecuteNonQuery();
                }
            }

        }
        
        public void ActualizarPersonalAsistencia(PersonalAsistenciaDTO item)
        {

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarPersonalAsistenciaGeneral", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAsistencia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonal", System.Data.SqlDbType.VarChar, 50)).Value = DBNull.Value;//item.CodigoPersonal;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraIngreso", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraIngreso;

                    if (item.FechaHoraRefrigerioSalida.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraRefrigerioSalida", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraRefrigerioSalida;
                    }
                    if (item.FechaHoraRefrigerioRetorno.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraRefrigerioRetorno", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraRefrigerioRetorno;
                    }
                    if (item.FechaHoraSalida.HasValue)
                    {
                        cmd.Parameters.Add(new SqlParameter("@FechaHoraSalida", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraSalida;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioEdicion ?? item.UsuarioCreacion;
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Eliminar(PersonalAsistenciaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarPersonalAsistencia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoPersonalAsistencia;
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
