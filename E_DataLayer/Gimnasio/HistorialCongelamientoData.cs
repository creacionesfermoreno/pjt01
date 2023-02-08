using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class HistorialCongelamientoData
	{   
        public List<HistorialCongelamientoDTO> ListarHitorialFreezingPorMenbresia(HistorialCongelamientoDTO oHistorialCongelamientoDTO)
        {
            List<HistorialCongelamientoDTO> lista = new List<HistorialCongelamientoDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarHitorialFreezingPorMenbresia", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oHistorialCongelamientoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oHistorialCongelamientoDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = oHistorialCongelamientoDTO.CodigoMembresia;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new HistorialCongelamientoDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoMembresia = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoMembresia")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    NombreSocio = oIDataReader[oIDataReader.GetOrdinal("NombreSocio")].ToString(),
                                    FechaInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaInicio")]),
                                    FechaFin = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaFin")]),
                                    NroDias = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("NroDias")]),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    NroSolicitud = oIDataReader[oIDataReader.GetOrdinal("NroSolicitud")].ToString(),
                                    Motivo = oIDataReader[oIDataReader.GetOrdinal("Motivo")].ToString()                                   
                                });
                            }
                        }
                    }
                }

            }
            return lista;          
        }
        
		public int Registrar(HistorialCongelamientoDTO item)
		{
            int? campoRetorno = 0;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarHistorialCongelamiento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.AddWithValue("@Codigo", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMembresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = item.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = item.FechaFin;
                    cmd.Parameters.Add(new SqlParameter("@NroDias", System.Data.SqlDbType.Int)).Value = item.NroDias;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaCreacion", System.Data.SqlDbType.DateTime)).Value = item.FechaCreacion;
                    cmd.Parameters.Add(new SqlParameter("@NroSolicitud", System.Data.SqlDbType.VarChar,50)).Value = item.NroSolicitud;
                    cmd.Parameters.Add(new SqlParameter("@Motivo", System.Data.SqlDbType.VarChar,500)).Value = item.Motivo;

                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@Codigo"].Value);
                }
            }

		  return Convert.ToInt32(campoRetorno);
		}
        
		public void Eliminar(HistorialCongelamientoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarHistorialCongelamiento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoMenbresia", System.Data.SqlDbType.Int)).Value = item.CodigoMembresia;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                   
                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
