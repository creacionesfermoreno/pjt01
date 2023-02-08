using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace E_DataLayer.Gimnasio
{
    public class HorarioClasesDetalleData
    {
        public List<HorarioClasesDetalleDTO> Listar(HorarioClasesDetalleDTO oHorarioClasesDetalleDTO)
        {
            List<HorarioClasesDetalleDTO> lista = new List<HorarioClasesDetalleDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorarioClasesDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHorarioClasesDetalleDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oHorarioClasesDetalleDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar,50)).Value = oHorarioClasesDetalleDTO.CodigoHorarioClasesDetalle;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HorarioClasesDetalleDTO()
                                {                                  
                                    CodigoHorarioClasesDetalle = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClasesDetalle")].ToString(),
                                    CodigoHorarioClases = oIDataReader[oIDataReader.GetOrdinal("CodigoHorarioClases")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("EstadoTm")]),
                                    FechaHoraReserva = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraReserva")]),
                                    NroCupo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroCupo")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NombreCompletoSocio = oIDataReader[oIDataReader.GetOrdinal("NombreCompletoSocio")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }
        
        public string Registrar(HorarioClasesDetalleDTO item)
        {
            string CodigoHorarioClasesDetalle = string.Empty;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspRegistrarHorarioClasesDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar,50)).Value = item.CodigoHorarioClases;
                    cmd.Parameters.AddWithValue("@CodigoHorarioClasesDetalle", "").Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@NroCupo", System.Data.SqlDbType.Int)).Value = item.NroCupo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraReserva", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraReserva;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioReservacion", System.Data.SqlDbType.VarChar,50)).Value = item.UsuarioReservacion;
                    cmd.Parameters.Add(new SqlParameter("@EstadoTm", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar,50)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                    CodigoHorarioClasesDetalle = cmd.Parameters["@CodigoHorarioClasesDetalle"].Value.ToString();

                }
            }
            return CodigoHorarioClasesDetalle;
        }

        public int RegistrarMasivoNuevosHorarioClasesGrupales(HorarioClasesDetalleDTO item)
        {
            string CodigoHorarioClasesDetalle = string.Empty;
            int Resultado = 0;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("UspRegistraMasivoNuevoHorarioClasesGrupalesDiario", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int, 10)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int, 10)).Value = item.CodigoSede;
                    if (item.FechaHoraReserva.HasValue)
                    {
                        if (item.FechaHoraReserva.Value.Year > (DateTime.Now.Year - 1))
                        {
                            cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaHoraReserva.Value;
                        }
                    }
                    if (item.NroSala > 0)
                    {
                        cmd.Parameters.Add(new SqlParameter("@NroSala", System.Data.SqlDbType.Int, 10)).Value = item.NroSala;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;
                    Resultado = cmd.ExecuteNonQuery();

                }
            }
            return Resultado;
        }

        public int ValidarExisteReservaHorarioClasesPorSocio(HorarioClasesDetalleDTO item)
        {
            string CodigoHorarioClasesDetalle = string.Empty;
            int? Resultado = 0;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarHorarioClasesDetalleCalendarioPorSocio", conn))
                {                                                                                                          
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClases", System.Data.SqlDbType.VarChar,50)).Value = item.CodigoHorarioClases;
                    cmd.Parameters.Add(new SqlParameter("@NroCupo", System.Data.SqlDbType.Int)).Value = item.NroCupo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Resultado = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistro")]);                                                                  
                            }
                        }
                    }
                }
            }

            return Resultado.Value;
        }
        
        public void Eliminar(HorarioClasesDetalleDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarHorarioClasesDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHorarioClasesDetalle", System.Data.SqlDbType.VarChar,50)).Value = item.CodigoHorarioClasesDetalle;
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
