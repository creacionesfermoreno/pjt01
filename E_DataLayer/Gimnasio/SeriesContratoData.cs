using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class SeriesContratoData
	{
		
        public List<SeriesContratoDTO> Listar(SeriesContratoDTO oitem)
		{
			List<SeriesContratoDTO> lista = new List<SeriesContratoDTO>();
          
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSeriesContrato", conn))
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
                                lista.Add(new SeriesContratoDTO()
                                {
                                    CodigoSerie = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSerie")]),
                                    TipoContrato = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoContrato")]),
                                    DesTipoContrato = oIDataReader[oIDataReader.GetOrdinal("DesTipoContrato")].ToString(),
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
		
		public SeriesContratoDTO BuscarPorCodigoSeriesContrato(SeriesContratoDTO oSeriesContrato)
		{
			SeriesContratoDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSeriesContratoPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSeriesContrato.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSeriesContrato.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSerie", System.Data.SqlDbType.Int)).Value = oSeriesContrato.CodigoSerie;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new SeriesContratoDTO()
                                {
                                    CodigoSerie = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSerie")]),
                                    TipoContrato = Convert.ToInt32(reader[reader.GetOrdinal("TipoContrato")]),
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

        public SeriesContratoDTO BuscarGenerarCorrelativo(SeriesContratoDTO oSeriesContratoDTO)
        {
            SeriesContratoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspGenerarSerieContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oSeriesContratoDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@TipoContrato", System.Data.SqlDbType.Int)).Value = oSeriesContratoDTO.flag;
                    cmd.Parameters.Add(new SqlParameter("@longitudSerie", System.Data.SqlDbType.Int)).Value = oSeriesContratoDTO.longitudSerie;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oSeriesContratoDTO.CodigoSede;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new SeriesContratoDTO()
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

		public int Registrar(SeriesContratoDTO item)
		{
		    int ? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarSeriesContrato", conn))
                {              
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoSerie", campoRetorno).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@TipoContrato", System.Data.SqlDbType.Int)).Value = item.TipoContrato;
                    cmd.Parameters.Add(new SqlParameter("@NroSerie", System.Data.SqlDbType.VarChar,10)).Value = item.NroSerie;
                    cmd.Parameters.Add(new SqlParameter("@NroCorrelativoActual", System.Data.SqlDbType.VarChar, 200)).Value = item.NroCorrelativoActual;
                    cmd.Parameters.Add(new SqlParameter("@NroCorrelativoFinal", System.Data.SqlDbType.VarChar, 200)).Value = item.NroCorrelativoFinal;

                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                   
                    cmd.ExecuteNonQuery();
                }
            }
            
		  return Convert.ToInt32(campoRetorno);
		}

		public void Actualizar(SeriesContratoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSeriesContrato", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSerie", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@TipoContrato", System.Data.SqlDbType.Int)).Value = item.TipoContrato;
                    cmd.Parameters.Add(new SqlParameter("@NroSerie", System.Data.SqlDbType.VarChar, 10)).Value = item.NroSerie;
                    cmd.Parameters.Add(new SqlParameter("@NroCorrelativoActual", System.Data.SqlDbType.VarChar, 200)).Value = item.NroCorrelativoActual;
                    cmd.Parameters.Add(new SqlParameter("@NroCorrelativoFinal", System.Data.SqlDbType.VarChar, 200)).Value = item.NroCorrelativoFinal;

                    
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
            }
		}

		public void Eliminar(SeriesContratoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarSeriesContrato", conn))
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
