using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class SeriesData
	{
	
        public List<SeriesDTO> Listar(SeriesDTO oitem)
		{
			List<SeriesDTO> lista = new List<SeriesDTO>();
          
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSeries", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SeriesDTO()
                                {
                                    CodigoSerie = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSerie")]),
                                    TipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoDocumento")]),
                                    DesTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesTipoDocumento")].ToString(),
                                    SubTipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("SubTipoDocumento")]),
                                    DesSubTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DesSubTipoDocumento")].ToString(),
                                    NroSerie = oIDataReader[oIDataReader.GetOrdinal("NroSerie")].ToString(),
                                    NroCorrelativoActual = oIDataReader[oIDataReader.GetOrdinal("NroCorrelativoActual")].ToString(),
                                    NroCorrelativoFinal = oIDataReader[oIDataReader.GetOrdinal("NroCorrelativoFinal")].ToString(),
                                    DesEstado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]) ? "Vigente" : "Terminado"                                 
                                });
                            }
                        }

                    }
                }
            }
            return lista;
		}

		public SeriesDTO BuscarPorCodigoSeries(SeriesDTO oSeries)
		{
			SeriesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSeriesPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSeries.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSeries.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSerie", System.Data.SqlDbType.Int)).Value = oSeries.CodigoSerie;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new SeriesDTO()
                                {
                                    CodigoSerie = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSerie")]),
                                    TipoDocumento = Convert.ToInt32(reader[reader.GetOrdinal("TipoDocumento")]),
                                    NroSerie = reader[reader.GetOrdinal("NroSerie")].ToString(),
                                    NroCorrelativoActual = reader[reader.GetOrdinal("NroCorrelativoActual")].ToString(),
                                    NroCorrelativoFinal = reader[reader.GetOrdinal("NroCorrelativoFinal")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
           
		}

        public SeriesDTO BuscarGenerarCorrelativo(SeriesDTO oSeries)
        {           
            SeriesDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspGenerarSerie", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSeries.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@flag", System.Data.SqlDbType.Int)).Value = oSeries.flag;
                    cmd.Parameters.Add(new SqlParameter("@subFlag", System.Data.SqlDbType.Int)).Value = oSeries.subFlag;
                    cmd.Parameters.Add(new SqlParameter("@longitudSerie", System.Data.SqlDbType.Int)).Value = oSeries.longitudSerie;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSeries.CodigoSede;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new SeriesDTO()
                                {
                                    NroCorrelativoActual = reader[reader.GetOrdinal("NroSerie")].ToString() 
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;           
        }
		
		public int Registrar(SeriesDTO item)
		{
		   int ? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarSeries", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSerie", System.Data.SqlDbType.Int)).Value = item.CodigoSerie;
                    //cmd.Parameters.AddWithValue("@CodigoSerie", campoRetorno).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@SubTipoDocumento", System.Data.SqlDbType.Int)).Value = item.SubTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@NroSerie", System.Data.SqlDbType.VarChar, 10)).Value = item.NroSerie;
                    cmd.Parameters.Add(new SqlParameter("@NroCorrelativoActual", System.Data.SqlDbType.VarChar, 200)).Value = item.NroCorrelativoActual;                   
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@chkGenerarSerie", System.Data.SqlDbType.Bit)).Value = item.chkGenerarSerie;
                    cmd.Parameters.Add(new SqlParameter("@chkGenerarComprobante", System.Data.SqlDbType.Bit)).Value = item.chkGenerarComprobante;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                  
                    //campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoSerie"].Value);
                    cmd.ExecuteNonQuery();
                }
            }
         
		  return Convert.ToInt32(campoRetorno);
		}

		public void Actualizar(SeriesDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSeries", conn))
                {                     
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSerie", System.Data.SqlDbType.Int)).Value = item.CodigoSerie;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@SubTipoDocumento", System.Data.SqlDbType.Int)).Value = item.SubTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@NroSerie", System.Data.SqlDbType.VarChar, 10)).Value = item.NroSerie;
                    cmd.Parameters.Add(new SqlParameter("@NroCorrelativoActual", System.Data.SqlDbType.VarChar, 200)).Value = item.NroCorrelativoActual;

                    cmd.Parameters.Add(new SqlParameter("@NroCorrelativoFinal", System.Data.SqlDbType.VarChar, 200)).Value = item.NroCorrelativoFinal;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
		}

        public void ActualizarSerieAumentar(SeriesDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSeriesAumentar", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = item.TipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@SubTipoDocumento", System.Data.SqlDbType.Int)).Value = item.SubTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;

                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.CodigoInicioSesion;
                    
                    cmd.ExecuteNonQuery();
                }
            }           
        }

		public void Eliminar(SeriesDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarSeries ", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;                  
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSerie", System.Data.SqlDbType.Int)).Value = item.CodigoSerie;

                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
