
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class AsistenciaInvitadosData
	{		
        public List<AsistenciaInvitadosDTO> uspListarDetalleAsistenciaInvitados_Paginacion(AsistenciaInvitadosDTO oAsistenciaInvitadosDTO, Paging paging)
		{
            List<AsistenciaInvitadosDTO> lista = new List<AsistenciaInvitadosDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleAsistenciaInvitados_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaInvitadosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = oAsistenciaInvitadosDTO.CodigoInvitado;
                   
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AsistenciaInvitadosDTO()
                                {
                                    fila = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Fila")]),
                                    CodigoAsistenciaI = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoAsistenciaI")]),
                                    CodigoInvitado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]),
                                    FechaIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaIngreso")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;
		}

        public List<AsistenciaInvitadosDTO> uspListarAsistenciaTodosFiltroInvitados_Paginacion(AsistenciaInvitadosDTO oAsistenciaInvitadosDTO, Paging paging)
		{
            List<AsistenciaInvitadosDTO> lista = new List<AsistenciaInvitadosDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAsistenciaTodosFiltroInvitados_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaInvitadosDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAsistenciaInvitadosDTO.FechaIngreso;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAsistenciaInvitadosDTO.FechaFinalizo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaInvitadosDTO.CodigoSede;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AsistenciaInvitadosDTO()
                                {
                                    CodigoInvitado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoInvitado")]),
                                    FechaIngreso = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaIngreso")]),
                                    NombreCompleto = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}
        
        public AsistenciaInvitadosDTO uspListarDetalleAsistenciaInvitados_NumeroRegistros(AsistenciaInvitadosDTO oAsistenciaInvitados)
		{
            AsistenciaInvitadosDTO itemDTO = new AsistenciaInvitadosDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarDetalleAsistenciaInvitados_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaInvitados.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = oAsistenciaInvitados.CodigoInvitado;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AsistenciaInvitadosDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
		}

        public AsistenciaInvitadosDTO uspListarAsistenciaTodosFiltroInvitados_NumeroRegistros(AsistenciaInvitadosDTO oAsistenciaInvitados)
		{
            AsistenciaInvitadosDTO itemDTO = new AsistenciaInvitadosDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAsistenciaTodosFiltroInvitados_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAsistenciaInvitados.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAsistenciaInvitados.FechaIngreso;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAsistenciaInvitados.FechaFinalizo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAsistenciaInvitados.CodigoSede;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new AsistenciaInvitadosDTO()
                                {
                                    CantTotal = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
            
		}
        
		public void Registrar(AsistenciaInvitadosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarAsistenciaInvitados", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAsistenciaI", System.Data.SqlDbType.Int)).Value = item.CodigoAsistenciaI;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                  
                    cmd.ExecuteNonQuery();
                }
            }            
		}

        public void uspActualizarAsistenciaInvitadoPorCodigoInvitado(AsistenciaInvitadosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarAsistenciaInvitadoPorCodigoInvitado", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                   
                    cmd.ExecuteNonQuery();
                }
            }
		}
        
		public void Eliminar(AsistenciaInvitadosDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarAsistenciaInvitados", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoAsistenciaI", System.Data.SqlDbType.Int)).Value = item.CodigoAsistenciaI;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInvitado", System.Data.SqlDbType.Int)).Value = item.CodigoInvitado;

                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
