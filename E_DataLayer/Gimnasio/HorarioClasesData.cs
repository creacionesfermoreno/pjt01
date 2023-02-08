//using E_DataLayer.DLHelper;
using E_DataModel.Gimnasio;
using E_DataLayer.DLHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace E_DataLayer.Gimnasio
{
    public class HorarioClasesData
    {
        public List<HorarioClasesDTO> Listar(HorarioClasesDTO oHorarioClasesDTO)
        {
            List<HorarioClasesDTO> lista = new List<HorarioClasesDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClases", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoSede;
                  
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HorarioClasesDTO()
                                {
                                    CodigoHorarioClases = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClases")].ToString(),
                                    CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSala")]),
                                    CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]),
                                    DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString(),
                                    DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]),
                                    CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString(),
                                    CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString(),
                                    FechaHoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]),
                                    FechaHoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]),
                                    ProfesionalDTO = new ProfesionalFitnessDTO()
                                    {
                                        NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreProfecionalFitness")].ToString()
                                    }
                                });

                            }
                        }

                    }
                }
            }
          
            return lista;
        }

        public List<HorarioClasesDTO> ListarCalendarioDelDia(HorarioClasesDTO oHorarioClasesDTO)
        {
            List<HorarioClasesDTO> lista = new List<HorarioClasesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesCalendarioDiario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HorarioClasesDTO()
                                {
                                    CodigoHorarioClases = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClases")].ToString(),
                                    CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]),
                                    CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]),
                                    DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString(),
                                    DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]),
                                    CodigoHorarioClasesConfiguracion = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString(),
                                    CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString(),
                                    FechaHoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]),
                                    FechaHoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoTm")]),
                                    ProfesionalDTO = new ProfesionalFitnessDTO()
                                    {
                                        CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString(),
                                        NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreProfecionalFitness")].ToString(),
                                        DNI = oIDataReader[oIDataReader.GetOrdinal("DNIProfecionalFitness")].ToString(),
                                        ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("PhotoProfecionalFitness")].ToString()
                                    }
                                });

                            }
                        }

                    }
                }
            }

            for (int i = 0; i < lista.Count; i++)
            {
                using (var conn = new SqlConnection(Helper.Conexion()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("uspListarHorarioClasesDetalleCalendarioDiario", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoUnidadNegocio;
                        cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoSede;
                        cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar, 50)).Value = lista[i].CodigoHorarioClases;

                        using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                        {
                            if (oIDataReader.HasRows)
                            {
                                lista[i].ListaDetalleHorario = new List<HorarioClasesDetalleDTO>();

                                while (oIDataReader.Read())
                                {
                                    var horarioDetalle = new HorarioClasesDetalleDTO()
                                    {
                                        CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                        CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                        CodigoSocio = oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")] != null ? Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]) : 0,
                                        CodigoInvitado = oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")] != null ? Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]) : 0,
                                        NroCupo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroCupo")]),
                                        CodigoHorarioClases = lista[i].CodigoHorarioClases,
                                        NombreCompletoSocio = oIDataReader[oIDataReader.GetOrdinal("NombreCompletoSocio")].ToString(),
                                        DNISocio = oIDataReader[oIDataReader.GetOrdinal("DNISocio")].ToString(),
                                        PhotoSocio = oIDataReader[oIDataReader.GetOrdinal("PhotoSocio")].ToString(),
                                        FechaHoraReserva = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraReserva")]),
                                        Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoTm")]),
                                        CodigoHorarioClasesDetalle = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesDetalle")].ToString(),
                                        CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")])
                                    };
                                    if (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]) > 0)
                                    {
                                        horarioDetalle.Socio = new ClientesDTO()
                                        {
                                            DNI = oIDataReader[oIDataReader.GetOrdinal("DNISocio")].ToString(),
                                            ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("PhotoSocio")].ToString(),
                                            NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreCompletoSocio")].ToString(),
                                            CodigoSocio = oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")] != null ? Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]) : 0
                                        };
                                    }
                                    if (Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]) > 0)
                                    {
                                        horarioDetalle.Invitado = new InvitadosDTO()
                                        {
                                            DNI = oIDataReader[oIDataReader.GetOrdinal("DNIInvitado")].ToString(),
                                            Nombres = oIDataReader[oIDataReader.GetOrdinal("NombresInvitado")].ToString(),
                                            Apellidos = oIDataReader[oIDataReader.GetOrdinal("ApellidosInvitado")].ToString(),
                                            CodigoInvitado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")])
                                        };
                                    }

                                    lista[i].ListaDetalleHorario.Add(horarioDetalle);

                                }
                            }

                        }
                    }
                }
            }
            return lista;
        }

        public List<HorarioClasesDTO> ListarHorarioPorProfesional(HorarioClasesDTO oHorarioClasesDTO)
        {
            List<HorarioClasesDTO> lista = new List<HorarioClasesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesCalendarioDiario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HorarioClasesDTO()
                                {
                                    CodigoHorarioClases = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClases")].ToString(),
                                    CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]),
                                    CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]),
                                    DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString(),
                                    DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]),
                                    CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString(),
                                    FechaHoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]),
                                    FechaHoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoTm")]),
                                    ProfesionalDTO = new ProfesionalFitnessDTO()
                                    {
                                        NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreProfecionalFitness")].ToString()
                                    }
                                });

                            }
                        }

                    }
                }
            }

            return lista;
        }

        public List<HorarioClasesDTO> ListarHorarioPorFecha(HorarioClasesDTO oHorarioClasesDTO)
        {
            List<HorarioClasesDTO> lista = new List<HorarioClasesDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesCalendarioPorFecha", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodSala", System.Data.SqlDbType.Int, 10)).Value = oHorarioClasesDTO.NroSala;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oHorarioClasesDTO.FechaHoraInicio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                HorarioClasesDTO item = new HorarioClasesDTO();
                                item.CodigoHorarioClases = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClases")].ToString();
                                item.Disciplina = oIDataReader[oIDataReader.GetOrdinal("Disciplina")].ToString();
                                item.CodigoSalaHorario = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                item.CapacidadPermitida = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]);
                                item.DiaNombre = oIDataReader[oIDataReader.GetOrdinal("DiaNombre")].ToString();
                                item.DiaNumero = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                item.CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString();
                                item.FechaHoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                item.FechaHoraFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                item.Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoTm")]);
                                item.ProfesionalDTO = new ProfesionalFitnessDTO()
                                {
                                    CodigoProfesional = oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString(),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("NombreProfecionalFitness")].ToString(),
                                    DNI = oIDataReader[oIDataReader.GetOrdinal("DNIProfecionalFitness")].ToString(),
                                    ImagenUrl = oIDataReader[oIDataReader.GetOrdinal("PhotoProfecionalFitness")].ToString()
                                };
                                lista.Add(item);
                            }
                        }
                    }
                }
            }
            return lista;
        }

        public HorarioClasesDTO BuscarPorCodigoHorarioClases(HorarioClasesDTO oHorarioClases)
        {
            HorarioClasesDTO itemDTO = null;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarHorarioClasesPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClases.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClases.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClases.CodigoHorarioClases;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new HorarioClasesDTO();
                                itemDTO.CodigoHorarioClasesConfiguracion = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString());
                                itemDTO.CodigoProfesional = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString());
                                itemDTO.CodigoSalaHorario = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                itemDTO.DiaNombre = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("DiaNombre")]);
                                itemDTO.DiaNumero = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                itemDTO.FechaHoraInicio = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                itemDTO.FechaHoraFin = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                itemDTO.ProfesionalDTO = new ProfesionalFitnessDTO() { };
                                itemDTO.ProfesionalDTO.Nombres = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("NombreProfesionalFitness")]);
                                itemDTO.ProfesionalDTO.Apellidos = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("ApellidosProfesionalFitness")]);
                                itemDTO.ProfesionalDTO.DNI = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("DNIProfesionalFitness")]);
                                itemDTO.ProfesionalDTO.NombreCompleto = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("NombreCompletoProfecionalFitness")]);
                                itemDTO.ProfesionalDTO.ImagenUrl = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("PhotoPersonalFitness")]);
                            }
                        }
                    }
                }
            }

            //using (ERPProcesosDataContext dc = new ERPProcesosDataContext(Helper.Conexion()))
            //{
            //    var query = dc.uspBuscarHorarioClasesPorCodigo(oHorarioClases.CodigoUnidadNegocio, oHorarioClases.CodigoSede, oHorarioClases.CodigoHorarioClases);
            //    foreach (var item in query)
            //    {
            //        itemDTO = new HorarioClasesDTO()
            //        {
            //            CodigoHorarioClasesConfiguracion = item.CodigoHorarioClasesConfiguracion.Value.ToString(),
            //            CodigoProfesional = item.CodigoProfesional.Value.ToString(),
            //            CodigoSalaHorario = item.CodigoSala.HasValue ? item.CodigoSala.Value : 0,
            //            CapacidadPermitida = item.Capacidad.HasValue ? item.Capacidad.Value : 0,
            //            DiaNombre = item.DiaNombre,
            //            DiaNumero = item.DiaNumero.Value,
            //            FechaHoraInicio = item.FechaHoraInicio,
            //            FechaHoraFin = item.FechaHoraFin,
            //            ProfesionalDTO = new ProfesionalFitnessDTO()
            //            {
            //                Nombres = item.NombreProfesionalFitness,
            //                Apellidos = item.ApellidosProfesionalFitness,
            //                DNI = item.DNIProfesionalFitness,
            //                NombreCompleto = item.NombreCompletoProfecionalFitness,
            //                ImagenUrl = item.PhotoPersonalFitness
            //            }
            //        };
            //    }
            //}



            return itemDTO;
        }
        //LISTAR DE HORARIO DEL PROFESOR
        public List<HorarioClasesDTO> ListarPorCodigoProfesionalFitnessDelDia(HorarioClasesDTO oHorarioClases)
        {
            List<HorarioClasesDTO> list =  new List<HorarioClasesDTO>();
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
                                var itemDTO = new HorarioClasesDTO();
                              
                                itemDTO.CodigoHorarioClases = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClases")].ToString());
                                itemDTO.CodigoProfesional = oHorarioClases.CodigoProfesional; //Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString());                                
                                itemDTO.DiaNombre = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("DiaNombre")]);
                                itemDTO.DiaNumero = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                itemDTO.FechaHoraInicio = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                itemDTO.FechaHoraFin = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                itemDTO.Disciplina = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("Disciplina")]);

                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("CodigoPersonalAsistencia")))
                                {
                                    itemDTO.CodigoPersonalAsistencia = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoPersonalAsistencia")]); ;
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraIngreso")))
                                {
                                    itemDTO.FechaHoraIngresoAsistencia = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraIngreso")]); ;
                                }
                                if (!oIDataReader.IsDBNull(oIDataReader.GetOrdinal("FechaHoraSalida")))
                                {
                                    itemDTO.FechaHoraSalidaAsistencia = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraSalida")]); ;
                                }
                                list.Add(itemDTO);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public HorarioClasesDTO BuscarPorCodigoHorarioClasesConDetalle(HorarioClasesDTO oHorarioClases)
        {
            HorarioClasesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarHorarioClasesPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClases.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClases.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClases.CodigoHorarioClases;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new HorarioClasesDTO();
                                itemDTO.CodigoHorarioClases = oHorarioClases.CodigoHorarioClases;
                                itemDTO.CodigoHorarioClasesConfiguracion = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesConfiguracion")].ToString());
                                itemDTO.CodigoProfesional = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoProfesional")].ToString());
                                itemDTO.CodigoSalaHorario = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("CodigoSalaHorario")]);
                                itemDTO.CapacidadPermitida = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("Capacidad")]);
                                itemDTO.Disciplina = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("Disciplina")]);
                                itemDTO.DiaNombre = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("DiaNombre")]);
                                itemDTO.DiaNumero = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("DiaNumero")]);
                                itemDTO.FechaHoraInicio = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraInicio")]);
                                itemDTO.FechaHoraFin = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraFin")]);
                                itemDTO.Estado = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("EstadoTm")]);

                                itemDTO.ProfesionalDTO = new ProfesionalFitnessDTO() { };
                                itemDTO.ProfesionalDTO.Nombres = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("NombreProfesionalFitness")]);
                                itemDTO.ProfesionalDTO.Apellidos = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("ApellidosProfesionalFitness")]);
                                itemDTO.ProfesionalDTO.DNI = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("DNIProfesionalFitness")]);
                                itemDTO.ProfesionalDTO.NombreCompleto = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("NombreCompletoProfecionalFitness")]);
                                itemDTO.ProfesionalDTO.ImagenUrl = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("PhotoPersonalFitness")]);
                            }
                        }
                    }
                }
            }

            if (itemDTO != null)
            {


                itemDTO.ListaDetalleHorario = new List<HorarioClasesDetalleDTO>();
                using (var conn = new SqlConnection(Helper.Conexion()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("uspListarHorarioClasesDetalleCalendarioDiario", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = oHorarioClases.CodigoUnidadNegocio;
                        cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = oHorarioClases.CodigoSede;
                        cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClases.CodigoHorarioClases;

                        using (SqlDataReader oIDataReader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                        {
                            if (oIDataReader.HasRows)
                            {
                                while (oIDataReader.Read())
                                {

                                    var horarioDetalle = new HorarioClasesDetalleDTO()
                                    {
                                        CodigoHorarioClases = oHorarioClases.CodigoHorarioClases, //Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClases")]),

                                        CodigoUnidadNegocio = itemDTO.CodigoUnidadNegocio,
                                        CodigoSede = itemDTO.CodigoSede,
                                        CodigoSocio = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                        CodigoInvitado = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]),
                                        NroCupo = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("NroCupo")]),
                                        NombreCompletoSocio = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("NombreCompletoSocio")]),
                                        DNISocio = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("DNISocio")]),
                                        PhotoSocio = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("PhotoSocio")]),
                                        FechaHoraReserva = Conversion.DbValueToDefault<DateTime>(oIDataReader[oIDataReader.GetOrdinal("FechaHoraReserva")]),
                                        Estado = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("EstadoTm")]),
                                        CodigoHorarioClasesDetalle = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesDetalle")]),
                                        CodigoMembresia = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    };
                                    if (horarioDetalle.CodigoSocio > 0)
                                    {
                                        horarioDetalle.Socio = new ClientesDTO()
                                        {
                                            DNI = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("DNISocio")]),
                                            ImagenUrl = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("PhotoSocio")]),
                                            NombreCompleto = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("NombreCompletoSocio")]),
                                            CodigoSocio = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                        };
                                    }
                                    if (horarioDetalle.CodigoInvitado > 0)
                                    {
                                        horarioDetalle.Invitado = new InvitadosDTO()
                                        {
                                            DNI = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("DNIInvitado")]),
                                            Nombres = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("NombresInvitado")]),
                                            Apellidos = Conversion.DbValueToDefault<string>(oIDataReader[oIDataReader.GetOrdinal("ApellidosInvitado")]),
                                            CodigoInvitado = Conversion.DbValueToDefault<int>(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]),
                                        };
                                    }
                                    itemDTO.ListaDetalleHorario.Add(horarioDetalle);
                                }
                            }
                        }
                    }
                }
            }

            return itemDTO;
        }

        public string Registrar(HorarioClasesDTO item)
        {
            string Codigo = string.Empty;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspRegistrarHorarioClases", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalaHorario", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSalaHorario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;
                    cmd.Parameters.AddWithValue("@CodigoHorarioClases", "").Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Capacidad", System.Data.SqlDbType.Int, 10)).Value = item.CapacidadPermitida;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.Int, 10)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@DiaNombre", System.Data.SqlDbType.VarChar, 50)).Value = item.DiaNombre;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraFin", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraFin;
                    cmd.Parameters.Add(new SqlParameter("@EstadoTm", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    cmd.ExecuteNonQuery();
                    Codigo = cmd.Parameters["@CodigoHorarioClases"].Value.ToString();
                }
            }
            return Codigo;
        }

        public void Actualizar(HorarioClasesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarHorarioClases", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSalaHorario", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoSalaHorario;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesConfiguracion", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClasesConfiguracion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoProfesional;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClases;
                    cmd.Parameters.Add(new SqlParameter("@Capacidad", System.Data.SqlDbType.VarChar, 50)).Value = item.CapacidadPermitida;
                    cmd.Parameters.Add(new SqlParameter("@DiaNumero", System.Data.SqlDbType.VarChar, 50)).Value = item.DiaNumero;
                    cmd.Parameters.Add(new SqlParameter("@DiaNombre", System.Data.SqlDbType.VarChar, 50)).Value = item.DiaNombre;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraInicio", System.Data.SqlDbType.VarChar, 50)).Value = item.FechaHoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraFin", System.Data.SqlDbType.VarChar, 50)).Value = item.FechaHoraFin;
                    cmd.Parameters.Add(new SqlParameter("@EstadoTm", System.Data.SqlDbType.VarChar, 50)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }
            }            
        }

        public void Eliminar(HorarioClasesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarHorarioClases", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar, 50)).Value = item.CodigoHorarioClases;
                   
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool ActualizarHorarioEnEjecucion(HorarioClasesDTO oHorarioClasesDTO)
        {
            int resultado = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspActualizarHorarioClasesEstadoPorAsistenciaProfesor", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oHorarioClasesDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoProfesional", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClasesDTO.CodigoProfesional;
                    if (oHorarioClasesDTO.CodigoHorarioClases.Length >10)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClasesDTO.CodigoHorarioClases;
                    }
                    if (oHorarioClasesDTO.CodigoPersonalAsistencia.Length>10)
                    {
                        cmd.Parameters.Add(new SqlParameter("@CodigoPersonalAsistencia", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClasesDTO.CodigoPersonalAsistencia;
                    }
                    
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = oHorarioClasesDTO.UsuarioEdicion;
                    resultado = cmd.ExecuteNonQuery();
                }
            }
            return resultado > 0 ? true : false;
        }
    }
}
