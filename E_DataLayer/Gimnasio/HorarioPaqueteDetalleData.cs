
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class HorarioPaqueteDetalleData
	{
		
        public List<HorarioPaqueteDetalleDTO> uspListarHoraPaquete(HorarioPaqueteDetalleDTO oitem)
		{
			List<HorarioPaqueteDetalleDTO> lista = new List<HorarioPaqueteDetalleDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHoraPaquete", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHP", System.Data.SqlDbType.Int)).Value = oitem.CodigoHP;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HorarioPaqueteDetalleDTO()
                                {
                                    CodigoHPD = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoHPD")]),
                                    CodigoHP = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoHP")]),
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    horaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("horaFin")]),
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

        public List<HorarioPaqueteDetalleDTO> uspListarHorasCurso(HorarioPaqueteDetalleDTO oitem)
		{
		    List<HorarioPaqueteDetalleDTO> lista = new List<HorarioPaqueteDetalleDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHorasCurso", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPaquete", System.Data.SqlDbType.Int)).Value = oitem.CodigoPaquete;
                    cmd.Parameters.Add(new SqlParameter("@Dia", System.Data.SqlDbType.Int)).Value = oitem.Dia;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HorarioPaqueteDetalleDTO()
                                {
                                    desHora = oIDataReader[oIDataReader.GetOrdinal("desHora")].ToString()                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}
			
		public void Registrar(HorarioPaqueteDetalleDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarHorarioPaqueteDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHPD", System.Data.SqlDbType.Int)).Value = item.CodigoHPD;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHP", System.Data.SqlDbType.Int)).Value = item.CodigoHP;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@horaFin", System.Data.SqlDbType.DateTime)).Value = item.horaFin;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }
		}
        
		public void Eliminar(HorarioPaqueteDetalleDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarHorarioPaqueteDetalle", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoHPD", System.Data.SqlDbType.Int)).Value = item.CodigoHPD;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                   
                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
